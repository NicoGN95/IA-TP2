using _Main._main.Scripts.Datas;
using _Main._main.Scripts.Guns;
using UnityEngine;

namespace _Main._main.Scripts.Entities.Player
{
    public class PlayerModel : BaseModel
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform cameraManagerTransform;
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform checkGroundPosition;
        [SerializeField] private Rifle gun;
        private PlayerVisual m_view;

        
        private void Start()
        {
            m_view = GetComponent<PlayerVisual>();
            gun.Equip(handTransform);
        }

        public PlayerData GetData() => data;
        public PlayerInputData GetPlayerInputData() => data.InputData;

        public bool CheckGround()
        {
            return Physics.CheckSphere(checkGroundPosition.position, 0.1f, data.CheckGroundMask);;
        }

        public void Shoot() => gun.Shoot();
        public void ReloadGun() => gun.Reload();
        public float GetCameraRotationY() => cameraManagerTransform.eulerAngles.y;
        public PlayerVisual GetView() => m_view;

        public override void HitToModel<T>(T p_attacker, float p_damage)
        {
            
            DoDamage(p_damage);
        }
    }
}