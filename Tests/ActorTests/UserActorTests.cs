using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.NUnit;
using ColorWarsMultiplayerActors.Actors;
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
        private TestActorRef<GameActor> _testActor;

        private TestProbe _proxyActorProbe;
        private TestProbe _gameActorProbe;

        [SetUp]
        public void Setup()
        {
            _proxyActorProbe = CreateTestProbe();
            _gameActorProbe = CreateTestProbe();

            var props = Props.Create(() => new GameActor(_proxyActorProbe));
            _actor = Sys.ActorOf(props);
            _testActor = ActorOfAsTestActorRef<GameActor>(props);

        }


        [Test]
        public void WhenGameIsCreated_SendsStatus()
        {
            _actor.Tell(new UserActor.WelcomeToGameMessage(_gameActorProbe));

            _proxyActorProbe.ExpectMsg<ProxyActor.EnteredGameStatus>();
        }
    }
}
