using System;
using _Main._main.Scripts.Datas;
using _Main._main.Scripts.Managers;
using UnityEngine;

namespace _Main._main.Scripts.Guns
{
    public class Rifle : MonoBehaviour, IGun
    {
        [SerializeField] private GunData data;
        [SerializeField] private Transform shootPoint;
        
        private GameManager m_gameManager;
        private int m_bullCount;
        private float m_shotCooldown;
        private void Start()
        {
            m_gameManager = GameManager.Instance;

            m_bullCount = data.maxBullCount;
        }

        public void Equip(Transform p_parent)
        {
            transform.position = p_parent.position;
            transform.parent = p_parent;
        }
        public void Shoot()
        {
            if(m_bullCount <= 0)
                return;

            if (m_shotCooldown > Time.time)
                return;
            
            var l_bull= m_gameManager.GetBulletFromPool();
            var l_transform = shootPoint.transform;
            
            var camera = Camera.main;
            var l_cameraCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            if (!Physics.Raycast(camera.ScreenPointToRay(l_cameraCenter), out var l_hit))
            {
                l_bull.Initialize(l_transform.position, camera.transform.forward);
                m_shotCooldown = data.fireRate + Time.time;
                return;
            }

            var l_position = l_transform.position;
            var l_dir = (l_hit.point - l_position).normalized;

            l_bull.Initialize(l_position, l_dir);
            m_shotCooldown = data.fireRate + Time.time;
        }

        public void Reload()
        {
            m_bullCount = data.maxBullCount;
        }
    }
}