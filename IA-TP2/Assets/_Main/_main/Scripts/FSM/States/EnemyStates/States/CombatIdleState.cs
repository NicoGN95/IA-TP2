using System.Collections.Generic;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "CombatIdleState", menuName = "_main/States/EnemyStates/CombatIdleState", order = 0)]
    public class CombatIdleState : State
    {
        private class CombatData
        {
            public CombatData(float p_combatTimer, State p_state)
            {
                CombatTimer = p_combatTimer;
                CombatState = p_state;
            }
            
            public float CombatTimer;
            public State CombatState;
        }

        private static IEventService EventService => ServiceLocator.Get<IEventService>();
        private readonly Dictionary<EnemyModel, CombatData> m_timerDictionary = new Dictionary<EnemyModel, CombatData>();
        public override void EnterState(EnemyModel p_model)
        {
            
            var l_rnd = Random.Range(p_model.GetData().CombatActionTimerRange[0],
                p_model.GetData().CombatActionTimerRange[1]);
            
            m_timerDictionary[p_model] = new CombatData(Time.time + l_rnd, p_model.GetCombatStateByRoulette());
            p_model.SbController.SetZeroSb();
            p_model.ActivateCombatMode();
        }
        public override void ExecuteState(EnemyModel p_model)
        {
            if (m_timerDictionary[p_model].CombatTimer < Time.time)
            {
                EventService.DispatchEvent(new ChangeEnemyStateCustomEventData(p_model, m_timerDictionary[p_model].CombatState.GetType()));
            }
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_timerDictionary.Remove(p_model);
        }
    }
}