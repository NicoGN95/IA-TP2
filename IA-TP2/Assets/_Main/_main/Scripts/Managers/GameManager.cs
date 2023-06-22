using _Main._main.Scripts.Classes;
using _Main._main.Scripts.Entities.Player;
using _Main._main.Scripts.Guns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main._main.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private string loadSceneName;
        [SerializeField] private Bullet bulletPrefab;
        private PlayerModel m_localPlayer;
       
        private string m_nextSceneToLoad;
        private PoolBullet m_poolBullet;

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


            m_poolBullet = new PoolBullet(bulletPrefab);
        }

        #region Gets
        public PlayerModel GetLocalPlayer() => m_localPlayer;
        public string GetNextSceneToLoad() => m_nextSceneToLoad;
        public string GetLoadSceneName() => loadSceneName;

        public Bullet GetBulletFromPool() => m_poolBullet.GetOrCreate();
        public void ReturnBulletToPool(Bullet p_bullet) => m_poolBullet.InUseToAvailable(p_bullet);
        public void ClearPool() => m_poolBullet.Clear();
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