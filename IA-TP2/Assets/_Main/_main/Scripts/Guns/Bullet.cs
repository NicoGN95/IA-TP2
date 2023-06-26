using System;
using _Main._main.Scripts.Classes;
using _Main._main.Scripts.Datas;
using _Main._main.Scripts.Managers;
using UnityEngine;

namespace _Main._main.Scripts.Guns
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] private BulletData data;
        private Vector3 m_dir;
        private float m_lifeTime;
        private LayerMask m_ownerLayer;
        public void Initialize(Vector3 p_initPos, Vector3 p_initDir, LayerMask p_ownerLayer)
        {
            gameObject.SetActive(true);
            transform.position = p_initPos;
            m_dir = p_initDir;

            transform.forward = m_dir;

            m_lifeTime = Time.time + data.lifeTime;
            m_ownerLayer = p_ownerLayer;
        }


        private void Update()
        {
            transform.position += m_dir * (data.speed * Time.deltaTime);

            if (m_lifeTime < Time.time)
            {
                gameObject.SetActive(false);
                GameManager.Instance.ReturnBulletToPool(this);
            }
        }


        private void OnTriggerEnter(Collider p_other)
        {
            if (m_ownerLayer == p_other.gameObject.layer)
                return;
            
            if (p_other.TryGetComponent(out IHealthController l_controller))
            {
                l_controller.DoDamage(data.damage);
            }
            gameObject.SetActive(false);
            GameManager.Instance.ReturnBulletToPool(this);
        }
    }
}