using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.Conditions
{
    [CreateAssetMenu(fileName = "LineOfSightToPlayer", menuName = "_main/States/Conditions/LineOfSightToPlayer", order = 0)]
    public class LineOfSightToPlayer : StateCondition
    {
        public override bool CompleteCondition(EnemyModel p_model)
        {
            return LineOfSight(p_model);
        }
        
        private bool LineOfSight(EnemyModel p_model)
        {
            var l_targetPos = p_model.GetTargetTransform().position;
            var l_modelTransform = p_model.transform;
            Vector3 l_directionToTarget = l_targetPos - l_modelTransform.position;
            
            float l_distanceToTarget = l_directionToTarget.magnitude;

            if (l_distanceToTarget > p_model.GetData().ViewDepthRange) 
                return false;

            float l_angleToTarget = Vector3.Angle(l_modelTransform.forward, l_directionToTarget.normalized);
            
            if (l_angleToTarget > p_model.GetData().ViewDegrees/2) 
                return false;

            if (Physics.Linecast(l_modelTransform.position, l_targetPos, p_model.GetData().TargetMask))
            {
                p_model.SetLastTargetLocation(l_targetPos);
                return true;
            }

            return false;
        }
    }
}