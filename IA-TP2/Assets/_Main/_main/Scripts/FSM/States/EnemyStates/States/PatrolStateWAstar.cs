using System.Collections.Generic;
using System.Linq;
using _Main._main.Scripts.Classes.Pathfinding;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Managers;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "PatrolStateWaStar", menuName = "_main/States/EnemyStates/PatrolStateWaStar", order = 0)]
    public class PatrolStateWAstar : State
    {
        [SerializeField] private LayerMask maskObs;
        private class PatrolData
        {
            public int WaypointCount;
            public float WaitTime; 
            public List<MyNode> PatrolNodes;
            public List<MyNode> Path;
            public MyNode TargetNode;
        }
        
        
        private Dictionary<EnemyModel, PatrolData> m_patrolDatas = new Dictionary<EnemyModel, PatrolData>();
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SbController.SetZeroSb();


            m_patrolDatas[p_model] = new PatrolData();
            m_patrolDatas[p_model].PatrolNodes = new List<MyNode>();
            m_patrolDatas[p_model].WaypointCount = 0;
            m_patrolDatas[p_model].WaitTime = p_model.GetData().WaitPatrolTime;
            
            
            var l_grid = GameManager.Instance.grid;
            
            for (int i = 0; i < p_model.PatrolPoints.Lenght; i++)
            {
                m_patrolDatas[p_model].PatrolNodes.Add(l_grid.NodeFromWorldPoint(p_model.PatrolPoints.points[i]));
            }

            var l_closestNodeToEnemy = l_grid.NodeFromWorldPoint(p_model.transform.position);
            SetPathToNextWaypoint(p_model, l_closestNodeToEnemy, 0);

            m_patrolDatas[p_model].TargetNode = m_patrolDatas[p_model].Path[0];

        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_diff = (m_patrolDatas[p_model].TargetNode.WorldPos - p_model.transform.position);

            if (l_diff.magnitude < 1)
            {
                m_patrolDatas[p_model].WaitTime -= Time.deltaTime;

                if (m_patrolDatas[p_model].WaitTime >= 0)
                {
                    p_model.Move(Vector3.zero, 0);
                    return;
                }
                
                
                //ya llego al punto, necesita ir al siguiente
                m_patrolDatas[p_model].WaypointCount++;
                
                if (m_patrolDatas[p_model].WaypointCount >= m_patrolDatas[p_model].PatrolNodes.Count)
                {
                    m_patrolDatas[p_model].WaypointCount = 0;
                }

                var l_count = m_patrolDatas[p_model].WaypointCount;
                var l_closestNodeToEnemy = GameManager.Instance.grid.NodeFromWorldPoint(p_model.transform.position);
                SetPathToNextWaypoint(p_model, l_closestNodeToEnemy, l_count);
                m_patrolDatas[p_model].TargetNode = m_patrolDatas[p_model].Path[l_count];
                m_patrolDatas[p_model].WaitTime = p_model.GetData().WaitPatrolTime;
            }

            var l_dir = l_diff.normalized;
            
            p_model.Move(l_dir, p_model.GetData().WalkSpeed);
            
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_patrolDatas.Remove(p_model);
        }

        private void SetPathToNextWaypoint(EnemyModel p_model, MyNode p_closestNodeToEnemy, int p_nextPathCount)
        {
            var l_astar = new AStar<MyNode>();
            
             var l_path = l_astar.Run(p_closestNodeToEnemy ,m_patrolDatas[p_model].PatrolNodes[p_nextPathCount], 
                Satisfies, GetConections, GetCost, Heuristic);

             m_patrolDatas[p_model].Path = l_astar.CleanPath(l_path, InView);
        }



        #region Funks

            bool Satisfies(MyNode p_curr, MyNode p_target)
            {
                return p_curr == p_target;
            }
            
            float Heuristic(MyNode p_curr, MyNode p_target)
            {
                float l_multiplierDistance = 2;
                float l_cost = 0;
                l_cost += Vector3.Distance(p_curr.transform.position, p_target.transform.position) * l_multiplierDistance;
                return l_cost;
            }
            
            float GetCost(MyNode p_parent, MyNode p_son)
            {
                float l_multiplierDistance = 1;
                
                float l_cost = 0;
                
                l_cost += Vector3.Distance(p_parent.transform.position, p_son.transform.position) * l_multiplierDistance;
                
                
                return l_cost;
            }
            
            List<MyNode> GetConections(MyNode p_curr)
            {
                var l_grid = GameManager.Instance.grid;
                
                return l_grid.GetNeighbours(p_curr).ToList();
            }
            
            bool InView(MyNode from, MyNode to)
            {
                Debug.Log("CLEAN");
                if (Physics.Linecast(from.transform.position, to.transform.position, maskObs)) return false;
                //Distance
                //Angle
                return true;
            }
            
        #endregion
        
    }
}