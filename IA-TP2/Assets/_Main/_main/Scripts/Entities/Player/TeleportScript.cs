using System;
using UnityEngine;
using System.Collections;

namespace _Main._main.Scripts.Entities.Player
{
    public class TeleportScript : MonoBehaviour
    {
        [SerializeField] private Vector3 placeToTeleport;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var l_rigidbody = other.gameObject.GetComponent<Rigidbody>();
                l_rigidbody.velocity = Vector3.zero;
                l_rigidbody.angularVelocity = Vector3.zero;
                l_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                other.transform.position = placeToTeleport;
                StartCoroutine(UnfreezeAfterDelay(l_rigidbody, 0.1f));
            }
        }
        IEnumerator UnfreezeAfterDelay(Rigidbody rb, float delay)
        {
            yield return new WaitForSeconds(delay);
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}