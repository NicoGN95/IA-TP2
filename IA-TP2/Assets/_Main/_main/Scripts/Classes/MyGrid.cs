using System;
using UnityEngine;

namespace _Main._main.Scripts.Classes
{
    public class MyGrid : MonoBehaviour
    {
        [SerializeField] private MyNode myNodePrefab;
        [SerializeField] private LayerMask walkableMask;
        [SerializeField] private Vector3 gridworldSize;
        [SerializeField] private float nodeRadius;
        private MyNode [,,] m_grid;
        private float m_nodeDiameter;
        private int m_gridSizeX;
        private int m_gridSizeY;
        private int m_gridSizeZ;

        void Start()
        {
            Initialize();
        }

        [ContextMenu("DeleteGrid")]
        private void DeleteGrid()
        {
            if (m_grid != null)
            {
                foreach (var node in m_grid)
                {
                    DestroyImmediate(node.gameObject);
                }
            }
        }
        
        
        [ContextMenu("ReloadGrid")]
        private void Initialize()
        {
            m_nodeDiameter = nodeRadius*2;
            m_gridSizeX = Mathf.RoundToInt(gridworldSize.x/m_nodeDiameter);
            m_gridSizeY = Mathf.RoundToInt(gridworldSize.y/m_nodeDiameter);
            m_gridSizeZ = Mathf.RoundToInt(gridworldSize.z/m_nodeDiameter);
            CreateGrid();
        }
        
        void CreateGrid()
        {
            m_grid = new MyNode[m_gridSizeX, m_gridSizeY, m_gridSizeZ];
            Vector3 l_worldBottomLeft = transform.position - Vector3.right * gridworldSize.x / 2 -
                                      Vector3.up * gridworldSize.y / 2 - Vector3.forward * gridworldSize.z /2;

            var l_halfExtents = new Vector3(m_nodeDiameter,m_nodeDiameter,m_nodeDiameter);
            for (int l_x = 0; l_x < m_gridSizeX; l_x++)
            {
                for (int l_y = 0; l_y < m_gridSizeY; l_y++)
                {
                    for (int l_z = 0; l_z < m_gridSizeZ; l_z++)
                    {
                        //Fijamos la verdadera posicion en el mundo del nodo
                        Vector3 l_worldPoint = l_worldBottomLeft + Vector3.right * (l_x * m_nodeDiameter + nodeRadius) + Vector3.up * (l_y * m_nodeDiameter + nodeRadius) + Vector3.forward *(l_z*m_nodeDiameter+ nodeRadius);
                        bool l_walkable = Physics.CheckBox(l_worldPoint, l_halfExtents, Quaternion.identity, walkableMask);
                        
                        var l_node = Instantiate(myNodePrefab);
                        l_node.Initialize(l_walkable, l_worldPoint, m_nodeDiameter/2);

                        
                        m_grid[l_x, l_y, l_z] = l_node;
                    }
                }
            }
            
            Debug.Log(m_grid.Length);
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridworldSize.x, gridworldSize.y, gridworldSize.z));

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