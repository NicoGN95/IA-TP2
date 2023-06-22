using _Main._main.Scripts.Classes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace _main.Scripts.Editor
{
    [CustomEditor(typeof(WaypointClass))]
    public class WaypointsEditor : UnityEditor.Editor
    {
        private float dashSize = 4f;
        private int[] segmentIndices;
        private WaypointClass wayPoints;
        private GUISkin sceneSkin;

        private void OnEnable()
        {
            sceneSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);
            Tools.hidden = true;
            
            wayPoints = target as WaypointClass;
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
            
            for (int i = 0; i < wayPoints.Lenght; i++)
            {
                Handles.Label(wayPoints[i] + (Vector3.zero), i.ToString(), sceneSkin.textField);
                
                EditorGUI.BeginChangeCheck();
                var newPos = Handles.PositionHandle(wayPoints[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(wayPoints, "Moved waypoint");
                    wayPoints[i] = newPos;
                }
            }
            
            DrawLines();
        }

        private void DrawLines()
        {
            Handles.DrawDottedLines(wayPoints.points, segmentIndices, dashSize);
        }

        private void CreateSegments()
        {
            
            segmentIndices = new int[wayPoints.Lenght * 2];
            if (wayPoints.Lenght < 2)
                return;
            
            segmentIndices = new int[wayPoints.Lenght * 2];
            int index = 0;
            for (int start = 0; start < segmentIndices.Length; start+=2)
            {
                segmentIndices[start] = index;
                index++;
                segmentIndices[start + 1] = index;
                
                
                
            }

            segmentIndices[segmentIndices.Length - 2] = wayPoints.Lenght - 1;
            segmentIndices[segmentIndices.Length - 1] = 0;
            
        }
    }
}