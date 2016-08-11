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
    public class GameActorTests:TestKit
    {

        private TestActorRef<GameActor> _testActor;
        private IActorRef _actor;

        private TestProbe _userActorProbe1;
        private TestProbe _userActorProbe2;
        private TestProbe _proxyActorProbe;

        private PlayerData _userData1;
        private PlayerData _userData2;

        [SetUp]
        public void Setup()
        {
            _userActorProbe1 = CreateTestProbe();
            _userActorProbe2 = CreateTestProbe();
            _proxyActorProbe = CreateTestProbe();

            var props = Props.Create(() => new GameActor(_proxyActorProbe));

            _testActor = ActorOfAsTestActorRef<GameActor>(props);
            _actor = Sys.ActorOf(props);

            _userData1 = new PlayerData("connection ID 1", "user 1", _userActorProbe1,1);
            _userData2 = new PlayerData("connection ID 2", "user 2", _userActorProbe2,2);
        }

        [Test]
        public void WhenGameIsCreated_AddsPlayersToList()
        {
            _testActor.Tell(new GameActor.NewGameMessage(new List<PlayerData>() { _userData1, _userData2 }));

            Assert.AreEqual(2, _testActor.UnderlyingActor.Players.Count);
        }

        


        [Test]
        public void WhenGameIsCreated_WelcomesUserActors()
        {
            _actor.Tell(new GameActor.NewGameMessage(new List<PlayerData>() { _userData1, _userData2 }));

            _userActorProbe1.ExpectMsg<UserActor.WelcomeToGameMessage>(m=>m.GameActor.Path==_actor.Path);
            _userActorProbe2.ExpectMsg<UserActor.WelcomeToGameMessage>(m => m.GameActor.Path == _actor.Path);
        }


        [Test]
        public void WhenGameIsCreated_InitializesGrid()
        {
            _testActor.Tell(new GameActor.NewGameMessage(new List<PlayerData>() { _userData1, _userData2 }));

            Assert.AreEqual(GameActor.GAME_HORIZONTAL_SIZE, _testActor.UnderlyingActor.GameData.HorizontalCount);
        }

        [Test]
        public void WhenGameIsCreated_SendsGridToUserActors()
        {
            _actor.Tell(new GameActor.NewGameMessage(new List<PlayerData>() { _userData1, _userData2 }));

            _userActorProbe1.IgnoreMessages(m=> m is UserActor.WelcomeToGameMessage);
            _userActorProbe2.IgnoreMessages(m => m is UserActor.WelcomeToGameMessage);

            _userActorProbe1.ExpectMsg<UserActor.GameInitializedMessage>();
            _userActorProbe2.ExpectMsg<UserActor.GameInitializedMessage>();
        }

        [Test]
        public void WhenGameIsCreated_InitializesPlayersPositions()
        {
            _testActor.Tell(new GameActor.NewGameMessage(new List<PlayerData>() { _userData1, _userData2 }));

            var query = _testActor.UnderlyingActor.GameData.Grid.Cast<GameCell>();
            Assert.IsTrue(query.Where(c => c.Owner == 1).Count() >= 1);
            Assert.IsTrue(query.Where(c => c.Owner == 2).Count() >= 1);
        }
    }
}
