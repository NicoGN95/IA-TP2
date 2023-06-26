using System.Collections.Generic;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "TakeCover", menuName = "_main/States/EnemyStates/TakeCover", order = 0)]
    public class TakeCover : State
    {
        [SerializeField] private LayerMask coverMask;
        
        private Dictionary<EnemyModel, Vector3> m_coverDictionary = new Dictionary<EnemyModel, Vector3>();
        private static IEventService EventService => ServiceLocator.Get<IEventService>();
        public override void EnterState(EnemyModel p_model)
        {
            Debug.Log("Take cover");
            p_model.SbController.SetZeroSb();
            
            Collider[] l_hit = new Collider[10];
            var l_enemyPos = p_model.transform.position;
            Physics.OverlapSphereNonAlloc(l_enemyPos, 10f, l_hit, coverMask);

            m_coverDictionary[p_model] = l_hit[0].ClosestPoint(l_enemyPos);
            p_model.SetTimeToEndAction(p_model.GetData().TakeCoverTime);
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_diff = m_coverDictionary[p_model] - p_model.transform.position;

            if (l_diff.magnitude <= 1f)
            {
                p_model.LookAt(p_model.GetTargetTransform().position);
            }
            else
            {
                p_model.Move(l_diff.normalized, p_model.GetData().RunSpeed);
            }
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_coverDictionary.Remove(p_model);
        }
    }
}