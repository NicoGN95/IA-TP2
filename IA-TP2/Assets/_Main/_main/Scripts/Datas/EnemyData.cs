using System.Collections.Generic;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Guns;
using UnityEngine;

namespace _Main._main.Scripts.Datas
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "_main/Datas/EnemyData", order = 0)]
    public class EnemyData : BaseData
    {
        
        
        [field: Header("Movement")]
        [field : SerializeField] public float WalkSpeed { get; private set; }
        [field : SerializeField] public float RunSpeed { get; private set; }
        [field : SerializeField] public float CombatMovementSpeed { get; private set; }
        
        
        [field: Header("FSM")]
        [field : SerializeField] public StateData[] AllStatesData { get; private set; }
        
        [field: Header("Combat")]
        
        [field : SerializeField] public Rifle GunPrefab { get; private set; }
        [field : SerializeField] public float CombatRange { get; private set; }
        [field : SerializeField] public float Spread { get; private set; }
        [field : SerializeField] public int ShotsPerRound { get; private set; }
        [field : SerializeField] public float TimeBtwRounds { get; private set; }
        [field : SerializeField] public List<State> CombatStates { get; private set; }
        [field : SerializeField] public List<float> CombatStatesChances { get; private set; }
        [field : SerializeField] public float[] CombatActionTimerRange { get; private set; }
        [field : SerializeField] public float[] MoveInCombatTimeRange { get; private set; }
        
        [field: Header("Line of Sight")]
        [field : SerializeField] public float ViewDepthRange { get; private set; }
        [field : SerializeField] public float ViewDegrees { get; private set; }
        
        
        [field: Header("Timers")]
        [field : SerializeField] public float SearchTime { get; private set; }
        [field : SerializeField] public float WaitPatrolTime { get; private set; }
        [field : SerializeField] public float PursuitTime { get; private set; }
        [field : SerializeField] public float ShootTime { get; private set; }
        [field : SerializeField] public float TakeCoverTime { get; private set; }
        
        
        [field: Header("Masks")]
        [field : SerializeField] public LayerMask OwnerMask { get; private set; }
        [field : SerializeField] public LayerMask TargetMask { get; private set; }
        [field: Header("Obs Avoidance")]
        
        [field : SerializeField] public LayerMask ObsMask { get; private set; }
        [field : SerializeField] public int MaxObjectsDetection { get; private set; }
        [field : SerializeField] public float CheckRadiusNearObstacles { get; private set; }
        [field : SerializeField] public float BehaviourIntensity { get; private set; }
        
        
        
    }
}