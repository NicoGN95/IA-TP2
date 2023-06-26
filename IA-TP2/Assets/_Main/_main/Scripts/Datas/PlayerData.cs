using System.Collections.Generic;
using _Main._main.Scripts.FSM.States.PlayerStates;
using UnityEngine;

namespace _Main._main.Scripts.Datas
{
    
    [CreateAssetMenu(menuName = "_main/Player/PlayerData")]
    public class PlayerData : BaseData
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float MouseSens { get; private set; }
        [field: SerializeField] public LayerMask CheckGroundMask { get; private set; }
        [field: SerializeField] public PlayerInputData InputData { get; private set; }

        [field: SerializeField] public List<PlayerState> States { get; private set; }
        [field: SerializeField] public LayerMask InteractLayerMask { get; private set; }

        [field: SerializeField] public float InteractRadius { get; private set; }
        
    }
}