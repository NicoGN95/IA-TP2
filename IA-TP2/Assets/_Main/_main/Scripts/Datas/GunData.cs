using UnityEngine;

namespace _Main._main.Scripts.Datas
{
    [CreateAssetMenu(fileName = "GunData", menuName = "_main/Datas/GunData", order = 0)]
    public class GunData : ScriptableObject
    {
        [field: SerializeField] public int maxBullCount;
        [field: SerializeField] public float fireRate;
    }
}