using _Main._main.Scripts.Classes;
using _Main._main.Scripts.Classes.Pathfinding;
using UnityEditor;
using UnityEngine;

namespace _Main._main.Scripts.Editor
{
    [CustomEditor(typeof(WaypointClass))]
    public class WaypointsEditor : UnityEditor.Editor
    {
        private float m_dashSize = 4f;
        private int[] m_segmentIndices;
        private WaypointClass m_wayPoints;
        private GUISkin m_sceneSkin;

        private void OnEnable()
        {
            m_sceneSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);
            Tools.hidden = true;
            
            m_wayPoints = target as WaypointClass;
            CreateSegments();
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }

        private void OnSceneGUI()
        {
            //
            if (Event.current.type == EventType.ValidateCommand &&
                Event.current.commandName.Equals("UndoRedoPerformed"))
            {
                CreateSegments();
            }
            //Escritura de la posicion de los puntos
            
            for (int l_i = 0; l_i < m_wayPoints.Lenght; l_i++)
            {
                Handles.Label(m_wayPoints[l_i] + (Vector3.zero), l_i.ToString(), m_sceneSkin.textField);
                
                EditorGUI.BeginChangeCheck();
                var l_newPos = Handles.PositionHandle(m_wayPoints[l_i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(m_wayPoints, "Moved waypoint");
                    m_wayPoints[l_i] = l_newPos;
                }
            }
            
            DrawLines();
        }

        private void DrawLines()
        {
            Handles.DrawDottedLines(m_wayPoints.points, m_segmentIndices, m_dashSize);
        }

        private void CreateSegments()
        {
            
            m_segmentIndices = new int[m_wayPoints.Lenght * 2];
            if (m_wayPoints.Lenght < 2)
                return;
            
            m_segmentIndices = new int[m_wayPoints.Lenght * 2];
            int l_index = 0;
            for (int l_start = 0; l_start < m_segmentIndices.Length; l_start+=2)
            {
                m_segmentIndices[l_start] = l_index;
                l_index++;
                m_segmentIndices[l_start + 1] = l_index;
                
                
                
            }

            m_segmentIndices[m_segmentIndices.Length - 2] = m_wayPoints.Lenght - 1;
            m_segmentIndices[m_segmentIndices.Length - 1] = 0;
            
        }
    }
}