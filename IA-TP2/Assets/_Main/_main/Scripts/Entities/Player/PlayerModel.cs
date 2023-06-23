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

        public void Shoot() => gun.Shoot(GetFrontPoint());
        public void ReloadGun() => gun.Reload();
        public float GetCameraRotationY() => cameraManagerTransform.eulerAngles.y;
        public PlayerVisual GetView() => m_view;
        public Rifle GetGun() => gun;
        
        
        private Vector3 GetFrontPoint()
        {
            var l_cameraTransform = Camera.main.transform;
            Physics.Raycast(l_cameraTransform.position, l_cameraTransform.forward, out var l_hit);
            
            return l_hit.point;
        }
        public override void HitToModel<T>(T p_attacker, float p_damage)
        {
            DoDamage(p_damage);
        }
    }
}