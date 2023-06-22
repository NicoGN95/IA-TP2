using System;
using _Main._main.Scripts.Entities.Player;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.PlayerStates
{
    public abstract class PlayerState : ScriptableObject
    {
        protected PlayerModel Model;
        protected PlayerVisual View;

        protected static IEventService EventService => ServiceLocator.Get<IEventService>();

        public virtual void EnterState(PlayerModel p_model)
        {
            Model = p_model;
            View = p_model.GetView();
        }
        
        public virtual void ExecuteState(){}
        public virtual void ExitState(){}
    }

    public struct SwitchPlayerStateCustomEventData : ICustomEventData
    {
        public Type NewStateType { get; }

        public SwitchPlayerStateCustomEventData(Type p_newStateType)
        {
            NewStateType = p_newStateType;
        }
    }
}