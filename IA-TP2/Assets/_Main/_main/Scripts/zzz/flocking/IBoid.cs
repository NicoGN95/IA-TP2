using UnityEngine;

namespace _Main._main.Scripts.zzz.flocking
{
    public interface IBoid
    {
        void Move(Vector3 dir);
        void LookDir(Vector3 dir);
        Vector3 Position { get; }
        Vector3 Front { get; }
        float Radius { get; }
    }
}
