using System;
using System.Collections.Generic;
using _Main._main.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace _Main._main.Scripts.zzz.flocking
{
    public class Leader : MonoBehaviour, IFlocking
    {
        public float multiplier;
        [HideInInspector] public Transform target;

        private void Start()
        {
            target = GameManager.Instance.GetLocalPlayer().transform;
        }

        public Vector3 GetDir(List<IBoid> boids, IBoid self)
        {
            return (target.position - self.Position).normalized * multiplier;
        }
    }
}
