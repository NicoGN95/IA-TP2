using _Main._main.Scripts.Datas;
using UnityEngine;

namespace _Main._main.Scripts.Guns
{
    public interface IGun
    {
        void Shoot(Vector3 p_endPoint);
        void Reload();
    }
}