using _Main._main.Scripts.Entities.Player;
using _Main._main.Scripts.Managers;
using _Main._main.Scripts.PickUps;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using _Main._main.Scripts.StaticClass;
using UnityEngine;

namespace _Main._main.Scripts.Objects
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private string keyIdRequired;
        [SerializeField] private string messageFailedHasKey;
        [SerializeField] private Animator animator;
        [SerializeField] private BoxCollider doorCollider;

        private bool m_isOpen;
        private static readonly int IsOpenAnimId = Animator.StringToHash("IsOpen");

        private static IEventService EventService => ServiceLocator.Get<IEventService>();

        public void Interact(PlayerModel p_playerModel)
        {
            if (!p_playerModel.HasKey(keyIdRequired))
            {
                EventService.DispatchEvent(new MessageHUDEventData(messageFailedHasKey, 1f));
                return;
            }

            m_isOpen = !m_isOpen;
            doorCollider.enabled = !m_isOpen;
            animator.SetBool(IsOpenAnimId, m_isOpen);
        }
        
        private void OnTriggerEnter(Collider p_other)
        {
            if (!p_other.CompareTag("Player")) 
                return;
        
            EventService.DispatchEvent(EventsDefinitions.ACTIVATE_HUD_INTERACT);
        }
    
        private void OnTriggerExit(Collider p_other)
        {
            if (!p_other.CompareTag("Player")) 
                return;
        
            EventService.DispatchEvent(EventsDefinitions.DEACTIVATE_HUD_INTERACT);
        }
    }
}