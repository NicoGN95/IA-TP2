﻿using _Main._main.Scripts.Classes;
using _Main._main.Scripts.Datas;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Managers;
using UnityEngine;

namespace _Main._main.Scripts.Entities.Enemies
{
    public class EnemyModel : BaseModel
    {
        [SerializeField] private EnemyData data;
        [SerializeField] private Transform handTransform;
        
        public EnemyView View{ get; private set; }
        public WaypointClass PatrolPoints{ get; private set; }
        public Vector3 LastKnownTargetLocation { get; private set; }

        private Rigidbody m_rb;
        private Transform m_targetTransform;
        private RouletteWheel<State> m_CombatRoulette;
        private State m_combatState;

        private float m_timeToEndAlert;
        private bool m_isParring;
        private bool m_isBlocking;
        private void Start()
        {

            View = GetComponent<EnemyView>();
            PatrolPoints = GetComponent<WaypointClass>();
            m_rb = GetComponent<Rigidbody>();
            m_targetTransform = GameManager.Instance.GetLocalPlayer().transform;

            m_CombatRoulette = new RouletteWheel<State>(data.CombatStates, data.CombatStatesChances);
        }


        public void Move(Vector3 p_direction, float p_speed)
        {
            //TODO: Que todo movimiento sea gradual y por rigidbody, que no pase del 0% al 100% del movimiento
            p_direction = p_direction.normalized;
            
            p_direction.y = 0;
            m_rb.velocity = p_direction * p_speed;
            View.PlayMovementAnimation(m_rb.velocity.magnitude);
            transform.forward = Vector3.Lerp(transform.forward, p_direction, 0.2f);
        }

        public void MoveInCombat(Vector3 p_direction)
        {
            p_direction = p_direction.normalized;
            
            p_direction.y = 0;
            m_rb.velocity = p_direction * data.CombatMovementSpeed;
            View.PlayMovementCombatAnim(m_rb.velocity);

            var l_dirToPlayer = m_targetTransform.position - transform.position;
            transform.forward = Vector3.Lerp(transform.forward, l_dirToPlayer.normalized, 0.2f);
        }
        
        

    #region Combat

        private float m_timeToFinishAction;
        public void ActivateCombatMode()
        {
            View.SetCombatAnimation(true);
        }
        
        public void DeactivateCombatMode()
        {
            View.SetCombatAnimation(false);
            
        }
        public void ExecuteAttack()
        {
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
        public Transform GetHandTransform() => handTransform;
        public float GetTimeToEndAlert() => m_timeToEndAlert;
        
        #endregion

    #region HealthController

        public override void Die()
        {
            View.PlayDieAnimation();
            Debug.Log("se murio");

            var l_enemies = new Collider[10];

            var l_enemiesCount =
                Physics.OverlapSphereNonAlloc(transform.position, data.PanicRadiusEffect, l_enemies, data.OwnerMask);


            Destroy(gameObject);
        }

        public override void HitToModel<T>(T p_attacker, float p_damage)
        {
            if (IsParring)
            {
                p_attacker.Stun();
                return;
            }

            if (IsBlocking)
            {
                return;
            }
            
            DoDamage(p_damage);
        }

        public override void Stun()
        {
            View.PlayStunAnimation();
        }

        #endregion
        

        
        
        #if UNITY_EDITOR

        
#endif
        
    }
}