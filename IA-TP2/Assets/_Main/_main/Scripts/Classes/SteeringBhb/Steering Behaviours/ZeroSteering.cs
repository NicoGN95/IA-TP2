using UnityEngine;

namespace _Main._main.Scripts.Classes.SteeringBhb.Steering_Behaviours
{
    public class ZeroSteering : ISteeringBehaviour
    {
        public Vector3 GetDir() => Vector3.zero;
    }
}