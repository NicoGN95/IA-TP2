
using _Main._main.Scripts.Entities.Player;
using _Main._main.Scripts.PickUps;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using _Main._main.Scripts.StaticClass;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string keyId;
    private static IEventService EventService => ServiceLocator.Get<IEventService>();
    
    
    public void Interact(PlayerModel p_playerModel)
    {
        p_playerModel.AddKey(keyId);
        Destroy(gameObject);
        EventService.DispatchEvent(EventsDefinitions.DEACTIVATE_HUD_INTERACT);
        EventService.DispatchEvent(EventsDefinitions.OBJECTIVE_COMPLETED);
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
