using System.Collections.Generic;
using System.Linq;
using _Main._main.Scripts.Classes.Pathfinding;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Managers;
using _Main._main.Scripts.zzz;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "PatrolStateWaStar", menuName = "_main/States/EnemyStates/PatrolStateWaStar", order = 0)]
    public class PatrolStateWAstar : State
    {
        [SerializeField] private LayerMask maskObs;
        private class PatrolData
        {
            public int PathPointCount;
            public float WaitTime; 
            public List<MyNode> PatrolNodes;
            public int PatrolCount;
            public List<MyNode> Path;
        }
        
        
        private Dictionary<EnemyModel, PatrolData> m_patrolDatas = new Dictionary<EnemyModel, PatrolData>();
        public override void EnterState(EnemyModel p_model)
        {
            p_model.SbController.SetZeroSb();

            m_patrolDatas[p_model] = new PatrolData();
            var l_data = m_patrolDatas[p_model];
            l_data.PatrolNodes = new List<MyNode>();
            l_data.PathPointCount = 0;
            l_data.PatrolCount = 0;
            l_data.WaitTime = p_model.GetData().WaitPatrolTime;
            
            
            var l_grid = GameManager.Instance.grid;
            
            for (int l_i = 0; l_i < p_model.PatrolPoints.Lenght; l_i++)
            {
                l_data.PatrolNodes.Add(l_grid.NodeFromWorldPoint(p_model.PatrolPoints.points[l_i]));
            }

            var l_closestNodeToEnemy = l_grid.NodeFromWorldPoint(p_model.transform.position);
            SetPathToNextWaypoint(p_model, l_closestNodeToEnemy, 0);

        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_data = m_patrolDatas[p_model];
            var l_pathCount = l_data.PathPointCount;
            var l_diff = (l_data.Path[l_pathCount].WorldPos - p_model.transform.position);

            if (l_diff.magnitude < 1)
            {
                l_data.WaitTime -= Time.deltaTime;

                if (l_data.WaitTime >= 0)
                {
                    p_model.Move(Vector3.zero, 0);
                    return;
                }
                
                
                //ya llego al punto, necesita ir al siguiente
                l_data.PathPointCount++;
                
                if (l_data.PathPointCount >= l_data.Path.Count)
                {
                    var l_closestNodeToEnemy = GameManager.Instance.grid.NodeFromWorldPoint(p_model.transform.position);

                    l_data.PatrolCount++;

                    if (l_data.PatrolCount >= l_data.PatrolNodes.Count)
                    {
                        l_data.PatrolCount = 0;
                    }
                    
                    SetPathToNextWaypoint(p_model, l_closestNodeToEnemy, l_data.PatrolCount);
                    l_data.WaitTime = p_model.GetData().WaitPatrolTime;
                    l_data.PathPointCount = 0;
                }
            }

            p_model.Move(l_data.Path[l_data.PathPointCount].WorldPos, p_model.GetData().WalkSpeed);
            p_model.LookAt(l_data.Path[l_data.PathPointCount].WorldPos);
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_patrolDatas.Remove(p_model);
        }

        private void SetPathToNextWaypoint(EnemyModel p_model, MyNode p_closestNodeToEnemy, int p_nextPathCount)
        {
            m_patrolDatas[p_model].Path = ThetaStar.Run(p_closestNodeToEnemy, m_patrolDatas[p_model].PatrolNodes[p_nextPathCount],
                Satisfies, GetConnections, GetCost, Heuristic, InView);
            
        }



        #region Funks

        private bool Satisfies(MyNode p_curr, MyNode p_target)
            {
                return p_curr == p_target;
            }
            
            private float Heuristic(MyNode p_curr, MyNode p_target)
            {
                float l_multiplierDistance = 2;
                float l_cost = 0;
                
                
                l_cost += Vector3.Distance(p_curr.WorldPos, p_target.WorldPos) * l_multiplierDistance;
                return l_cost;
            }
            
            private float GetCost(MyNode p_parent, MyNode p_son)
            {
                float l_multiplierDistance = 1;
                
                float l_cost = 0;
                
                l_cost += Vector3.Distance(p_parent.WorldPos, p_son.WorldPos) * l_multiplierDistance;
                
                
                return l_cost;
            }
            
            private List<MyNode> GetConnections(MyNode p_curr)
            {
                var l_grid = GameManager.Instance.grid;
                
                return l_grid.GetNeighbours(p_curr).ToList();
            }
            
            private bool InView(MyNode p_from, MyNode p_to)
            {
                return !Physics.Linecast(p_from.WorldPos, p_to.WorldPos, maskObs);
            }
            
        #endregion
        
    }
}