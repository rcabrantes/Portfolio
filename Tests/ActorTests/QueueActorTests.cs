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
    public class QueueActorTests:TestKit
    {
        private const string CONNECTION_ID = "valid connection ID";
        private const string USER_NAME = "valid user id";

        private TestProbe _proxyActorProbe;

        private TestActorRef<QueueActor> _testActor;
        private TestProbe _userActor;
        private TestProbe _secondUserActor;
        private IActorRef _actor;
        private ClientData _clientData;

        [SetUp]
        public void Setup()
        {
            _proxyActorProbe = CreateTestProbe();
            var props = Props.Create(() => new QueueActor(_proxyActorProbe));



            _actor = Sys.ActorOf(props);
            _testActor = ActorOfAsTestActorRef<QueueActor>(props);


            _secondUserActor = CreateTestProbe();
            
            _userActor = CreateTestProbe();
            _clientData = new ClientData(CONNECTION_ID, USER_NAME, _userActor);
        }

        [Test]
        public void WhenFirstUserIsAdded_AddsToList()
        {
            _testActor.Tell(new QueueActor.NewUserInQueue(_clientData));

            _testActor.UnderlyingActor.UserQueue[_clientData.ConnectionID].UserName = USER_NAME;

        }

        [Test]
        public void WhenSameUserIsAdded_DoesntAddToList()
        {

            _testActor.Ask(new QueueActor.NewUserInQueue(_clientData));
            _testActor.Ask(new QueueActor.NewUserInQueue(_clientData));

            
            Assert.AreEqual(1, _testActor.UnderlyingActor.UserQueue.Count);

        }




    }



    public class MyActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            
        }

        // if any child of MyActor throws an exception, apply the rules below
        // e.g. Restart the child, if 10 exceptions occur in 30 seconds or
        // less, then stop the actor
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(// or AllForOneStrategy
                maxNrOfRetries: 10,
                withinTimeMilliseconds:30,
                decider:Decider.From( x =>
                {
                return Directive.Stop;
                
                }));
        }

    
}
}
