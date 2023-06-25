using System.Collections.Generic;
using UnityEngine;

namespace _Main._main.Scripts.zzz.flocking
{
    public class Leader : MonoBehaviour, IFlocking
    {
        public float multiplier;
        public Transform target;
        public Vector3 GetDir(List<IBoid> boids, IBoid self)
        {
            return (target.position - self.Position).normalized * multiplier;
        }
    }
}
