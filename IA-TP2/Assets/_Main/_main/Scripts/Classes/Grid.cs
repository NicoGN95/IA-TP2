using System;
using UnityEngine;

namespace _Main._main.Scripts.Classes
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private LayerMask unwalkableMask;
        [SerializeField] private Vector2 gridworldSize;
        [SerializeField] private float nodeRadius;
        private Node [,] m_grid;
        private float m_nodeDiameter;
        private int m_gridSizeX;
        private int m_gridSizeY;

        void Start()
        {
            m_nodeDiameter = nodeRadius*2;
            m_gridSizeX = Mathf.RoundToInt(gridworldSize.x/m_nodeDiameter);
            m_gridSizeY = Mathf.RoundToInt(gridworldSize.y/m_nodeDiameter);
            CreateGrid();
        }
        
        void CreateGrid()
        {
            m_grid = new Node[m_gridSizeX, m_gridSizeY];
            Vector3 l_worldBottomLeft = transform.position - Vector3.right * gridworldSize.x / 2 -
                                      Vector3.forward * gridworldSize.y / 2;

            for (int l_x = 0; l_x < m_gridSizeX; l_x++)
            {
                for (int l_y = 0; l_y < m_gridSizeY; l_y++)
                {
                    Vector3 l_worldPoint = l_worldBottomLeft + Vector3.right * (l_x * m_nodeDiameter + nodeRadius) + Vector3.forward * (l_y * m_nodeDiameter + nodeRadius);
                    bool l_walkable = ! (Physics. CheckSphere(l_worldPoint, nodeRadius, unwalkableMask));
                    m_grid[l_x, l_y] = new Node(l_walkable, l_worldPoint);
                }
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridworldSize.x, 1 , gridworldSize.y));

            if (m_grid != null)
            {
                foreach (var l_node in m_grid)
                {
                    Gizmos.color = (l_node.Walkable) ? Color.green : Color.red;
                    Gizmos.DrawCube(l_node.WorldPos, Vector3.one * (m_nodeDiameter- 0.1f));
                }
            }
        }
#endif
        
    }
}