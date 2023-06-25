using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Main._main.Scripts.Classes
{
    public class MyNode : MonoBehaviour
    {
        public List<MyNode> neighbors;
        
        public bool Walkable;
        public Vector3 WorldPos;

        private float m_radius;
        public MyNode(bool p_walkable, Vector3 p_worldPos)
        {
            Walkable = p_walkable;
            WorldPos = p_worldPos;
        }

        public void Initialize(bool p_walkable, Vector3 p_worldPos, float p_radius)
        {
            Walkable = p_walkable;
            WorldPos = p_worldPos;
            m_radius = p_radius;
        }
        private void Start()
        {
            SetNeighbors();
        }

        private void SetNeighbors()
        {
            RaycastHit[] l_collisions = new RaycastHit[30];
            Physics.BoxCastNonAlloc(transform.position, new Vector3(m_radius, m_radius, m_radius), Vector3.zero,
                l_collisions);


            foreach (var l_col in l_collisions)
            {
                if (l_col.collider.TryGetComponent(out MyNode l_node))
                {
                    neighbors.Add(l_node);
                }
            }
        }
    }
}