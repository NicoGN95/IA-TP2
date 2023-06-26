using _Main._main.Scripts.Datas;
using UnityEngine;

namespace _Main._main.Scripts.Guns
{
    public interface IGun
    {
        void PlayerShoot();
        void Reload();
    }
}