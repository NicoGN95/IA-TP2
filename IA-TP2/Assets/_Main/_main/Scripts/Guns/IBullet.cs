using _Main._main.Scripts.Datas;
using UnityEngine;

namespace _Main._main.Scripts.Guns
{
    public interface IBullet
    {
        void Initialize(Vector3 p_initPos, Vector3 p_initDir, string p_ownerTag);
    }
}