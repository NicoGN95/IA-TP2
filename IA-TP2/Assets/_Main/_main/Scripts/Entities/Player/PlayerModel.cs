using System.Collections.Generic;
using _Main._main.Scripts.Datas;
using _Main._main.Scripts.Guns;
using _Main._main.Scripts.Managers;
using _Main._main.Scripts.PickUps;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Main._main.Scripts.Entities.Player
{
    public class PlayerModel : BaseModel
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform cameraManagerTransform;
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform checkGroundPosition;
        [SerializeField] private Rifle gun;

        private readonly Collider[] m_colliders = new Collider[2];
        private PlayerVisual m_view;

        protected override void Initialized()
        {
            base.Initialized();
            GameManager.Instance.SetLocalPlayer(this);
            m_view = GetComponent<PlayerVisual>();
            gun.Equip(handTransform, data.OwnerMask);
        }


        public PlayerData GetData() => data;
        public PlayerInputData GetPlayerInputData() => data.InputData;

        public bool CheckGround()
        {
            return Physics.CheckSphere(checkGroundPosition.position, 0.1f, data.CheckGroundMask);
        }

        public void Shoot() => gun.PlayerShoot();
        public void ReloadGun() => gun.Reload();

        public float GetCameraRotationY() => cameraManagerTransform.eulerAngles.y;
        public PlayerVisual GetView() => m_view;
        public Rifle GetGun() => gun;

        private readonly List<string> m_keys = new();

        public void AddKey(string p_keyId)
        {
            if (m_keys.Contains(p_keyId))
                return;
            
            m_keys.Add(p_keyId);
        }

        public void RemoveKey(string p_keyId)
        {
            if (!m_keys.Contains(p_keyId))
                return;
            
            m_keys.Remove(p_keyId);
        }

        public bool HasKey(string p_keyId) => m_keys.Contains(p_keyId);


        public void Interact()
        {
            var l_count = Physics.OverlapSphereNonAlloc(transform.position, data.InteractRadius, m_colliders, data.InteractLayerMask);
            for (var i = 0; i < l_count; i++)
            {
                var l_collider = m_colliders[i];
                if (!l_collider.TryGetComponent(out IInteractable l_interactable)) 
                    continue;
                
                l_interactable.Interact(this);
            }
        }

        public override void HitToModel<T>(T p_attacker, float p_damage)
        {
            DoDamage(p_damage);
        }
    }
}