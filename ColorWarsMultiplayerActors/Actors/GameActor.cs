﻿using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWarsMultiplayerActors.Models;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class GameActor : ReceiveActor
    {
        public IActorRef ProxyActor;
        public List<ClientData> Players;

        public GameActor(IActorRef proxyActor)
        {
            ProxyActor = proxyActor;

            Receive<NewGameMessage>(m=> {
                Players = m.Players;
                                
            });
        }

        
        

    }
}