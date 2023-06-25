using System.Collections.Generic;
using _Main._main.Scripts.Classes;
using _Main._main.Scripts.Classes.SteeringBhb;
using _Main._main.Scripts.Datas;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Managers;
using _Main._main.Scripts.zzz;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace _Main._main.Scripts.Entities.Enemies
{
    public class EnemyModel : BaseModel
    {
        [SerializeField] private EnemyData data;
        [SerializeField] private LayerMask obsLayer;
        [SerializeField] private float ObsAvoidanceMult;
        public EnemyView View{ get; private set; }
        public WaypointClass PatrolPoints{ get; private set; }
        public Vector3 LastKnownTargetLocation { get; private set; }

        public SbController SbController;
        
        private Rigidbody m_rb;
        private Transform m_targetTransform;
        private RouletteWheel<State> m_CombatRoulette;
        private State m_combatState;

        private float m_timeToEndAlert;


        private ObstacleAvoidance m_obstacleAvoidance;
        private void Start()
        {
            View = GetComponent<EnemyView>();
            PatrolPoints = GetComponent<WaypointClass>();
            m_rb = GetComponent<Rigidbody>();
            m_targetTransform = GameManager.Instance.GetLocalPlayer().transform;

            m_CombatRoulette = new RouletteWheel<State>(data.CombatStates, data.CombatStatesChances);
            SbController = new SbController(this, data.PursuitTime);
            m_obstacleAvoidance = new ObstacleAvoidance(this);
        }


        public void Move(Vector3 p_direction, float p_speed)
        {
            //TODO: Que todo movimiento sea gradual y por rigidbody, que no pase del 0% al 100% del movimiento
            var l_dir = m_obstacleAvoidance.GetDir(p_direction) + SbController.CurrSb.GetDir();
            l_dir.y = 0;
            m_rb.velocity = l_dir * p_speed;
            View.PlayMovementAnimation(m_rb.velocity.magnitude);
            transform.forward = Vector3.Lerp(transform.forward, l_dir, 0.2f);
        }


        #region Pathfinding

        private List<Vector3> m_pathPoints = new List<Vector3>();
        public void SetWayPoints(List<Node> p_newPoints)
        {
            foreach (var l_node in p_newPoints)
            {
                m_pathPoints.Add(l_node.transform.position);
            }
        }

        #endregion
        

        #region Combat

        private float m_timeToFinishAction;

        public void Shoot()
        {
            
        }
        public void ActivateCombatMode()
        {
            View.SetCombatAnimation(true);
        }
        
        public void DeactivateCombatMode()
        {
            View.SetCombatAnimation(false);
            
        }
        
        

        public void SetTimeToEndAction(float p_f)
        {
            m_timeToFinishAction = p_f + Time.time;
        }
        public float GetTimeToEndAction() => m_timeToFinishAction;

    #endregion
    
    #region Setters

        public void SetLastTargetLocation(Vector3 p_lastKnownLocation) => LastKnownTargetLocation = p_lastKnownLocation;
        public void SetTimeToEndSearch(float p_time) => m_timeToEndAlert = p_time + Time.time;
    #endregion
    
    #region Getters
        public EnemyData GetData() => data;
        public State GetCombatStateByRoulette() => m_CombatRoulette.RunWithCached();
        
        public float GetDistanceToTarget() => (m_targetTransform.transform.position - transform.position).magnitude;
        public Transform GetTargetTransform() => m_targetTransform;
        public float GetTimeToEndAlert() => m_timeToEndAlert;
        
        #endregion

    #region HealthController

        public override void Die()
        {
            View.PlayDieAnimation();

            Destroy(gameObject);
        }

        public override void HitToModel<T>(T p_attacker, float p_damage)
        {
            
            DoDamage(p_damage);
        }

        #endregion
        

        #if UNITY_EDITOR

        
#endif
        
    }
}