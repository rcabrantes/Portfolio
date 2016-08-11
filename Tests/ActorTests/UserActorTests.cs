using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.NUnit;
using ColorWarsMultiplayerActors.Actors;
using ColorWarsMultiplayerActors.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ActorTests
{
    [TestFixture]
    public class UserActorTests:TestKit
    {
        private IActorRef _actor;
        private TestActorRef<UserActor> _testActor;

        private TestProbe _proxyActorProbe;
        private TestProbe _gameActorProbe;

        [SetUp]
        public void Setup()
        {
            _proxyActorProbe = CreateTestProbe();
            _gameActorProbe = CreateTestProbe();

            var props = Props.Create(() => new UserActor("conn1","user name",_proxyActorProbe));
            _actor = Sys.ActorOf(props);
            _testActor = ActorOfAsTestActorRef<UserActor>(props);

        }


        [Test]
        public void WhenGameIsCreated_SendsStatus()
        {
            _actor.Tell(new UserActor.WelcomeToGameMessage(_gameActorProbe,1));

            _proxyActorProbe.ExpectMsg<ProxyActor.EnteredGameStatus>();
        }

        [Test]
        public void WhenGameIsCreated_SavesGameActor()
        {
            _testActor.Tell(new UserActor.WelcomeToGameMessage(_gameActorProbe,1));

            Assert.IsNotNull(_testActor.UnderlyingActor.GameActor);
        }

        [Test]
        public void WhenGameIsInitialized_SavesGameData()
        {
            _testActor.Tell(new UserActor.GameInitializedMessage(new GameGrid(72,36)));

            Assert.IsNotNull(_testActor.UnderlyingActor.GameData);
        }

        [Test]
        public void WhenGameIsInitialized_SendsGameInit()
        {
            _actor.Tell(new UserActor.GameInitializedMessage(new GameGrid(72, 36)));

            _proxyActorProbe.ExpectMsg<ProxyActor.GameInitCommand>();
        }
    }
}
