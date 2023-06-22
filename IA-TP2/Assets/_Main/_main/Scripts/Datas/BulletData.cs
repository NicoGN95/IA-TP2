using UnityEngine;

namespace _Main._main.Scripts.Datas
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "_main/Datas/BulletData", order = 0)]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float speed;
        [field: SerializeField] public float damage;
        [field: SerializeField] public float lifeTime;
    }
}