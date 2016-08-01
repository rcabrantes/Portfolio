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

        private const string CONNECTION_ID= "valid connection id";
        private const string USER_NAME = "valid user name";

        [SetUp]
        public void Setup()
        {

            _actor = Sys.ActorOf<LobbyActor>();
            _testActor = ActorOfAsTestActorRef<LobbyActor>();
        }



        [Test]
        public void WhenUserConnects_SavesToList()
        {
            _testActor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID,USER_NAME));

            var result=_testActor.UnderlyingActor.ClientList[CONNECTION_ID];

            Assert.AreEqual(USER_NAME, result.UserName);
        }

        [Test]
        public void WhenNewUserConnects_CreatesUserActor()
        {
            _testActor.Tell(new LobbyActor.ConnectMessage(CONNECTION_ID, USER_NAME));

            var result = _testActor.UnderlyingActor.ClientList[CONNECTION_ID].UserActor;

            Assert.IsNotNull(result);

        }

    }
}
