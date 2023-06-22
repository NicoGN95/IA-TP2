using System.Collections.Generic;
using _Main._main.Scripts.Guns;
using UnityEngine;

namespace _Main._main.Scripts.Classes
{
    public class PoolEntry<T>
    {
        public string Id;
        public T Value;
    }

    public class PoolBullet
    {
        private Queue<Bullet> m_available = new Queue<Bullet>();
        private List<Bullet> m_inUse = new List<Bullet>();

        private readonly Bullet m_prefab;

        public PoolBullet(Bullet p_bulletPrefab)
        {
            m_prefab = p_bulletPrefab;
        }

        public Bullet GetOrCreate()
        {
            if (m_available.Count > 0)
            {
                var l_data = m_available.Dequeue();
                if (l_data != null)
                    return l_data;
            }

            var l_newObj = Object.Instantiate(m_prefab);
            m_inUse.Add(l_newObj);
            
            return l_newObj;
        }

        public void InUseToAvailable(Bullet p_poolEntry)
        {
            m_inUse.Remove(p_poolEntry);
            m_available.Enqueue(p_poolEntry);
        }

        public void Clear()
        {
            m_available = new Queue<Bullet>();
            m_inUse = new List<Bullet>();
        }
    }
}
