using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main._main.Scripts.Classes
{
    public class PoolGeneric
    {
        private readonly Queue<GameObject> m_available = new Queue<GameObject>();
        
        private readonly List<GameObject> m_inUse = new List<GameObject>();

        public int AvailableCount => m_available.Count;

        

        public GameObject GetOrCreate(GameObject p_bulletPrefab)
        {
            if (m_available.Count > 0)
            {
                var l_obj = m_available.Dequeue();
                m_inUse.Add(l_obj);
                l_obj.SetActive(true);
                return l_obj;
            }
            else
            {
                var l_obj = GameObject.Instantiate(p_bulletPrefab);
                m_inUse.Add(l_obj);
                l_obj.SetActive(true);
                return l_obj;
            }
        }

        public void InUseToAvailable(GameObject p_poolEntry)
        {
            p_poolEntry.SetActive(false);
            m_inUse.Remove(p_poolEntry);
            m_available.Enqueue(p_poolEntry);
        }
    }
}