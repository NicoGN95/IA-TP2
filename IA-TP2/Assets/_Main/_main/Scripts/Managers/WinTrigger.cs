using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main._main.Scripts.Managers
{
    public class WinTrigger : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        
        private void OnTriggerEnter(Collider p_other)
        {
            if (!p_other.CompareTag("Player")) 
                return;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}