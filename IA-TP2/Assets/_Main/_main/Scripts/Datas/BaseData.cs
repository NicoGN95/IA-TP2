using UnityEngine;

namespace _Main._main.Scripts.Datas
{
    public class BaseData : ScriptableObject
    {
        [field: Header("Stats")]
        [field : SerializeField] public float MaxHealth { get; private set; }
        [field : SerializeField] public string Tag { get; private set; }
        
        
    }
}