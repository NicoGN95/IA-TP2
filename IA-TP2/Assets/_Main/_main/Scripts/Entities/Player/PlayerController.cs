using System;
using System.Collections.Generic;
using _Main._main.Scripts.FSM.States.PlayerStates;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using UnityEngine;

namespace _Main._main.Scripts.Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerModel model;
        [SerializeField] private PlayerState currentState;
        
        private readonly Dictionary<Type, PlayerState> m_statesDictionary = new();

        private static IEventService EventService => ServiceLocator.Get<IEventService>();

        private void OnEnable()
        {
            EventService.AddListener<SwitchPlayerStateCustomEventData>(SwitchState);
        }

        private void OnDisable()
        {
            EventService.RemoveListener<SwitchPlayerStateCustomEventData>(SwitchState);
        }

        private void Start()
        {
            var l_list = model.GetData().States;

            foreach (var l_state in l_list)
            {
                var l_type = l_state.GetType();
                if (m_statesDictionary.ContainsKey(l_type))
                {
                    Debug.LogError("Error: This key is already in the dictionary.");
                    continue;
                }
                
                m_statesDictionary.Add(l_type, l_state);
            }

            var l_firstType = l_list[0].GetType();
            SwitchState(l_firstType);
        }

        private void Update()
        {
            currentState.ExecuteState();
        }

        private void SwitchState(Type p_type)
        {
            if (!m_statesDictionary.ContainsKey(p_type))
            {
                Debug.LogError("Error: This key is not found in the dictionary");
                return;
            }
            
            if (currentState != default)
                currentState.ExitState();

            currentState = m_statesDictionary[p_type];
            
            currentState.EnterState(model);
        }
        
        private void SwitchState(SwitchPlayerStateCustomEventData p_data)
        {
            if (!m_statesDictionary.ContainsKey(p_data.NewStateType))
            {
                Debug.LogError("Error: This key is not found in the dictionary");
                return;
            }
            
            if (currentState != default)
                currentState.ExitState();

            currentState = m_statesDictionary[p_data.NewStateType];
            
            currentState.EnterState(model);
        }
    }
}