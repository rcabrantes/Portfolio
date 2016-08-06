using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWarsMultiplayerActors.Actors;
using ColorWarsMultiplayerActors.Models;

namespace Tests.ActorTests
{

    [TestFixture]
    public class LobbyActorTests:TestKit
    {
        private IActorRef _actor;
        private TestActorRef<LobbyActor> _testActor;
        private TestProbe _proxyActorProbe;
        private TestProbe _queueActorProbe;

        private const string CONNECTION_ID= "valid connection id";
        private const string USER_NAME = "valid user name";

        [SetUp]
        public void Setup()
        {
            _proxyActorProbe = CreateTestProbe();
            _queueActorProbe = CreateTestProbe();

            var lobbyActorProps = Props.Create(() => new LobbyActor(_proxyActorProbe, _queueActorProbe));


            _actor = Sys.ActorOf(lobbyActorProps);
            _testActor = ActorOfAsTestActorRef<LobbyActor>(lobbyActorProps);
        }



        [Test]
        public void WhenUserConnects_SavesToList()
        {
            _testActor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID,USER_NAME));

            var result=_testActor.UnderlyingActor.ClientList[CONNECTION_ID];

            Assert.AreEqual(USER_NAME, result.UserName);
        }

        [Test]
        public void WhenUserConnects_SendsMessage()
        {
            _testActor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID, USER_NAME));

            _proxyActorProbe.ExpectMsgFrom<ProxyActor.LobbyActorStatusMessage>(_testActor);
        }

        [Test]
        public void WhenNewUserConnects_CreatesUserActor()
        {
            _testActor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID, USER_NAME));

            var result = _testActor.UnderlyingActor.ClientList[CONNECTION_ID].UserActor;

            Assert.IsNotNull(result);

        }

        [Test]
        public void WhenExistingUserConnects_SendsMessage()
        {
            _actor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID, USER_NAME));

            _proxyActorProbe.ExpectMsgFrom<ProxyActor.LobbyActorStatusMessage>(_actor);

            _actor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID, USER_NAME));

            AwaitAssert(()=>_proxyActorProbe.ExpectMsgFrom<ProxyActor.UserAlreadyConnectedStatus>(_actor));
            

        }


        [Test]
        public void WhenUserConnects_SendsUserToQueue()
        {
            _actor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID, USER_NAME));

            _queueActorProbe.ExpectMsgFrom<QueueActor.NewUserInQueue>(_actor);
        }

    }
}
