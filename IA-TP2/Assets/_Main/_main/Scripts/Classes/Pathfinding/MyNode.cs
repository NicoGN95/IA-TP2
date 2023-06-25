using System.Collections.Generic;
using UnityEngine;

namespace _Main._main.Scripts.Classes.Pathfinding
{
    public class MyNode : MonoBehaviour
    {
        public readonly  List<MyNode> Neighbors;
        public int XId;
        public int YId;
        public int ZId;
        public bool Walkable;
        public Vector3 WorldPos;

        private float m_radius;

        public void Initialize(bool p_walkable, Vector3 p_worldPos, float p_radius, Vector3 gridId)
        {
            Walkable = p_walkable;
            WorldPos = p_worldPos;
            m_radius = p_radius;

            XId = (int)gridId.x;
            YId = (int)gridId.y;
            ZId = (int)gridId.z;
        }
        /*
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
                    Neighbors.Add(l_node);
                }
            }
        }
        */
    }
}