using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.Entities.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private StateMachine m_stateMachine;
        [SerializeField] private StateData currentState;
        private void Start()
        {
            var l_model = GetComponent<EnemyModel>();
            m_stateMachine = new StateMachine(l_model);
        }

        private void Update()
        {
            m_stateMachine.RunStateMachine();


#if UNITY_EDITOR
            
            currentState = m_stateMachine.GetCurrentState();
#endif
        }
    }
}