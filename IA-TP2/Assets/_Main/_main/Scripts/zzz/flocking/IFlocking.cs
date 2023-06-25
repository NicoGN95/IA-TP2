using System.Collections.Generic;
using UnityEngine;

namespace _Main._main.Scripts.zzz.flocking
{
    public interface IFlocking 
    {
        Vector3 GetDir(List<IBoid> boids, IBoid self);
    }
}
