using System;
using _Main._main.Scripts.zzz.flocking;
using UnityEngine;

namespace _Main._main.Scripts.Entities.Hostages
{
    public class Hostage : MonoBehaviour, IBoid
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotSpeed;
        [SerializeField] private float radius;
        [SerializeField] private float stoppingDistance;
        [SerializeField] private LayerMask obsMask;
        [SerializeField] private float obsRadius;
        [SerializeField] private float behaviorIntensity;


        private HostageObsAvoidance m_obstacleAvoidance;

        private Leader m_leader;
        private bool isFollowing = false;
        Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            m_leader = GetComponent<Leader>();

            m_obstacleAvoidance = new HostageObsAvoidance(5, obsMask, transform, obsRadius, behaviorIntensity);
        }
        public Vector3 Position => transform.position;

        public Vector3 Front => transform.forward;

        public float Radius => radius;

        public void Move(Vector3 dir)
        {
            if (!isFollowing)
                return;

            var l_dir = m_obstacleAvoidance.GetDir(transform.position + dir);
            
            float distanceToTarget = Vector3.Distance(transform.position, m_leader.target.position);

            if (distanceToTarget <= stoppingDistance)
            {
                l_dir = Vector3.zero;
            }
            else
            {
                l_dir *= speed;
            }
            
            l_dir.y = _rb.velocity.y;
            _rb.velocity = l_dir;
        }
        public void LookDir(Vector3 dir)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isFollowing = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Position, radius);
        }
    }
}