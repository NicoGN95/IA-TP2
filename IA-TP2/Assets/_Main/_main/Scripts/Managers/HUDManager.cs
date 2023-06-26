using System;
using System.Collections;
using _Main._main.Scripts.Entities.Player;
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
        [SerializeField] private Slider healthBar;

        private bool isUIVisible = true;
        private float timer;

        private Objective currentObjective;
        private PlayerModel m_playerModel;

        private bool m_isActivateMessage;

        private static IEventService EventService => ServiceLocator.Get<IEventService>();

        private void Awake()
        {
            interactPrompt.SetActive(false);
            EventService.AddListener(EventsDefinitions.ACTIVATE_HUD_INTERACT, ActivateHudInteractHandler);
            EventService.AddListener(EventsDefinitions.DEACTIVATE_HUD_INTERACT, DeactivateHudInteractHandler);
            EventService.AddListener<MessageHUDEventData>(OnMessageHUDHandler);
            EventService.AddListener(EventsDefinitions.OBJECTIVE_COMPLETED, AdvanceObjective);
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
            currentObjective = Objective.First;
            ShowUI();
            SetObjectiveText(1, firstObjectiveText.text);

            m_playerModel = GameManager.Instance.GetLocalPlayer();
            m_playerModel.m_healthController.OnChangeHealth += UpdateHealthBar;
            
            InputManager.Instance.SubscribeInput("HUD", OnPressKeyHandler);
        }

        private void OnPressKeyHandler(InputAction.CallbackContext p_obj)
        {
            if (!isUIVisible)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }

            timer = displayTime + Time.time;
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
                    firstObjectiveText.gameObject.SetActive(true);
                    secondObjectiveText.gameObject.SetActive(false);
                    thirdObjectiveText.gameObject.SetActive(false);
                    break;
                case 2:
                    secondObjectiveText.text = p_objectiveText;
                    firstObjectiveText.gameObject.SetActive(false);
                    secondObjectiveText.gameObject.SetActive(true);
                    thirdObjectiveText.gameObject.SetActive(false);
                    break;
                case 3:
                    thirdObjectiveText.text = p_objectiveText;
                    firstObjectiveText.gameObject.SetActive(false);
                    secondObjectiveText.gameObject.SetActive(false);
                    thirdObjectiveText.gameObject.SetActive(true);
                    break;
                default:
                    Debug.LogError("Invalid Objective Index: " + p_objectiveIndex);
                    break;
            }
        }

        public void AdvanceObjective()
        {
            switch (currentObjective)
            {
                case Objective.First:
                    currentObjective = Objective.Second;
                    SetObjectiveText(2, secondObjectiveText.text);
                    ShowUI();
                    break;
                case Objective.Second:
                    currentObjective = Objective.Third;
                    SetObjectiveText(3, thirdObjectiveText.text);
                    ShowUI();
                    break;
                case Objective.Third:
                    HideUI();
                    break;
            }
        }

        private void UpdateHealthBar(float maxHealth, float currentHealth)
        {
            healthBar.value = currentHealth / maxHealth;
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
    public enum Objective { First, Second, Third }
    
}
