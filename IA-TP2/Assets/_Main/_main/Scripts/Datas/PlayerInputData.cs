using UnityEngine;

namespace _Main._main.Scripts.Datas
{
    [CreateAssetMenu(menuName = "_main/Player/PlayerInputData")]
    public class PlayerInputData : ScriptableObject
    {
        [field: SerializeField] public string MovementId { get; private set; }
        [field: SerializeField] public string ShootId { get; private set; }
        [field: SerializeField] public string CameraMovementId { get; private set; }
        [field: SerializeField] public string AimId { get; private set; }
        [field: SerializeField] public string JumpId { get; private set; }
    }
}