using UnityEngine;

namespace _Main._main.Scripts.Classes
{
    public class Node
    {
        public bool Walkable;
        public Vector3 WorldPos;

        public Node(bool p_walkable, Vector3 p_worldPos)
        {
            Walkable = p_walkable;
            WorldPos = p_worldPos;
        }
    }
}