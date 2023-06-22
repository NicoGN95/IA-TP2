using _Main._main.Scripts.Entities.Enemies;
using UnityEngine;

namespace _Main._main.Scripts.FSM.Base
{
    public abstract class State : ScriptableObject
    {
        public virtual void EnterState(EnemyModel p_model){}
        public abstract void ExecuteState(EnemyModel p_model);
        public virtual void ExitState(EnemyModel p_model){}
    }
    
}

