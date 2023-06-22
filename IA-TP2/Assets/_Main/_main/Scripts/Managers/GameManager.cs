using _Main._main.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main._main.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private string loadSceneName;
        
        private PlayerModel m_localPlayer;
       
        private string m_nextSceneToLoad;

        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetLocalPlayer(changePlayer);
        }

        #region Gets
        public PlayerModel GetLocalPlayer() => m_localPlayer;
        public string GetNextSceneToLoad() => m_nextSceneToLoad;
        public string GetLoadSceneName() => loadSceneName;

        #endregion

        #region Sets
        public void SetLocalPlayer(PlayerModel p_newLocalPlayer) => m_localPlayer = p_newLocalPlayer;
        public void SetNextSceneToLoad(string p_nameScene) => m_nextSceneToLoad = p_nameScene;

        #endregion

        #region Functions

        public void ChangeSceneToLoadingScene()
        {
            SceneManager.LoadScene(loadSceneName);
        }
        
        #endregion

#if UNITY_EDITOR
        [SerializeField] private PlayerModel changePlayer;

        [ContextMenu("ForceChangeLocalPlayer")]
        private void ForceChangeLocalPlayer()
        {
            m_localPlayer = changePlayer;
        }
#endif
    }
}