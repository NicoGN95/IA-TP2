using System;
using System.Collections;
using _Main._main.Scripts.Services;
using _Main._main.Scripts.Services.MicroServices.EventsServices;
using _Main._main.Scripts.StaticClass;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace _Main._main.Scripts.Managers
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI firstObjectiveText;
        [SerializeField] private TextMeshProUGUI secondObjectiveText;
        [SerializeField] private TextMeshProUGUI thirdObjectiveText;
        [SerializeField] private GameObject objectiveBox;
        [SerializeField] private GameObject weaponIcon;
        [SerializeField] private GameObject interactPrompt;
        [SerializeField] private GameObject messagePanel;
        [SerializeField] private Text messageText;
        [SerializeField] private float displayTime = 5f;

        private bool isUIVisible = true;
        private float timer;

        private bool m_isActivateMessage;

        private static IEventService EventService => ServiceLocator.Get<IEventService>();

        private void Awake()
        {
            interactPrompt.SetActive(false);
            EventService.AddListener(EventsDefinitions.ACTIVATE_HUD_INTERACT, ActivateHudInteractHandler);
            EventService.AddListener(EventsDefinitions.DEACTIVATE_HUD_INTERACT, DeactivateHudInteractHandler);
            EventService.AddListener<MessageHUDEventData>(OnMessageHUDHandler);
        }

        private void OnMessageHUDHandler(MessageHUDEventData p_data)
        {
            if (m_isActivateMessage)
                return;
            StartCoroutine(ActivateMessageCoroutine(p_data));
        }

        private IEnumerator ActivateMessageCoroutine(MessageHUDEventData p_data)
        {
            m_isActivateMessage = true;
            messagePanel.SetActive(true);
            messageText.text = p_data.message;
            yield return new WaitForSeconds(p_data.viewTime);
            messagePanel.SetActive(false);
            m_isActivateMessage = false;
        }

        private void ActivateHudInteractHandler()
        {
            interactPrompt.SetActive(true);
        }
        
        private void DeactivateHudInteractHandler()
        {
            interactPrompt.SetActive(false);
        }

        private void Start()
        {
            ShowUI();
            SetObjectiveText(1, "First Objective");
            
            InputManager.Instance.SubscribeInput("HUD", OnPressKeyHandler);
        }

        private void OnPressKeyHandler(InputAction.CallbackContext p_obj)
        {
            if (!isUIVisible)
            {
                HideUI();
                return;
            }
            
            ShowUI();
        }

        private void Update()
        {
            if (!isUIVisible) 
                return;
            
            if (timer > Time.time)
                return;
            
            HideUI();
        }

        public void SetObjectiveText(int p_objectiveIndex, string p_objectiveText)
        {
            switch (p_objectiveIndex)
            {
                case 1:
                    firstObjectiveText.text = p_objectiveText;
                    break;
                case 2:
                    secondObjectiveText.text = p_objectiveText;
                    break;
                case 3:
                    thirdObjectiveText.text = p_objectiveText;
                    break;
                default:
                    Debug.LogError("Invalid Objective Index: " + p_objectiveIndex);
                    break;
            }
        }

        private void ShowUI()
        {
            isUIVisible = true;
            objectiveBox.SetActive(true);
            weaponIcon.SetActive(true);
            timer = displayTime + Time.time;
        }

        private void HideUI()
        {
            isUIVisible = false;
            objectiveBox.SetActive(false);
            weaponIcon.SetActive(false);
        }
    }

    public struct MessageHUDEventData : ICustomEventData
    {
        public string message { get; }
        public float viewTime { get; }
        
        public MessageHUDEventData(string p_message, float p_viewTime)
        {
            message = p_message;
            viewTime = p_viewTime;
        }
    }
}
