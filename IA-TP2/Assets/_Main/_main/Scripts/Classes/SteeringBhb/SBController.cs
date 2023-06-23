using _Main._main.Scripts.Classes.SteeringBhb.Steering_Behaviours;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.Entities.Player;
using _Main._main.Scripts.Managers;

namespace _Main._main.Scripts.Classes.SteeringBhb
{
    public class SbController
    {
        private EnemyModel m_enemyModel;
        private PlayerModel m_target;
        
        private Seek m_seek;
        private Pursuit m_pursuit;
        private Evade m_evade;
        private Flee m_flee;
        private ZeroSteering m_zero;
        
        float m_pursuitTime;
        ISteeringBehaviour m_currCurrSb;

        public ISteeringBehaviour CurrSb { get => m_currCurrSb; set => m_currCurrSb = value; }

        public SbController(EnemyModel p_enemyModel, float p_pursuitTime)
        {
            this.m_enemyModel = p_enemyModel;
            m_target = GameManager.Instance.GetLocalPlayer();
            m_pursuitTime = p_pursuitTime;
            InitializeSb();
        }

        void InitializeSb()
        {
            var l_enemyTransform = m_enemyModel.transform;
            var l_playerTransform = m_target.transform;
            
            m_seek = new Seek(l_enemyTransform, l_playerTransform);
            m_flee = new Flee(l_enemyTransform, l_playerTransform);
            m_pursuit = new Pursuit(l_enemyTransform, m_target, m_pursuitTime);
            m_evade = new Evade(l_enemyTransform, m_target, m_pursuitTime);
            m_zero = new ZeroSteering();
        }

        public void SetSeekSb() => m_currCurrSb = m_seek;
        public void SetFleeSb() => m_currCurrSb = m_flee;
        public void SetPursuitSb() => m_currCurrSb = m_pursuit;
        public void SetEvadeSb() => m_currCurrSb = m_evade;
        public void SetZeroSb() => m_currCurrSb = m_zero;

    }
}
