using System.Collections.Generic;
using System.Linq;
using _Main._main.Scripts.Classes.Pathfinding;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.FSM.Base;
using _Main._main.Scripts.Managers;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "SearchWTheta", menuName = "_main/States/EnemyStates/SearchWTheta", order = 0)]
    public class SearchWTheta : State
    {
        [SerializeField] private LayerMask maskObs;
        private class SearchData
        {
            public List<MyNode> PathToTarget;
            public int PathCount;
            public MyNode TargetNode;
            public float WaitTime;
        }
        
        private Dictionary<EnemyModel, SearchData> m_dictionary = new Dictionary<EnemyModel, SearchData>();
        
        public override void EnterState(EnemyModel p_model)
        {
            m_dictionary[p_model] = new SearchData();
            var l_grid = GameManager.Instance.grid;
            
            var l_data = m_dictionary[p_model];
            l_data.PathToTarget = new List<MyNode>();
            l_data.PathCount = 0;
            l_data.TargetNode = l_grid.NodeFromWorldPoint(p_model.LastKnownTargetLocation);
            l_data.WaitTime = 3f;

            var l_closestPointToEnemy = l_grid.NodeFromWorldPoint(p_model.transform.position);
            SetPathToNextWaypoint(p_model, l_closestPointToEnemy);
            
            p_model.SetTimeToEndSearch(p_model.GetData().SearchTime);
            p_model.SbController.SetZeroSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            var l_data = m_dictionary[p_model];
            var l_count = l_data.PathCount;
            var l_diffToTarget = l_data.PathToTarget[l_count].WorldPos - p_model.transform.position;
            
            if (l_diffToTarget.magnitude < 1f)
            {
                l_data.WaitTime -= Time.deltaTime;
                
                if(l_data.WaitTime > 0)
                    return;

                l_data.PathCount++;

                if (l_data.PathCount >= l_data.PathToTarget.Count)
                {
                    var l_grid = GameManager.Instance.grid;
                    var l_closestNode = l_grid.NodeFromWorldPoint(p_model.transform.position);
                    Vector3 l_rndVector = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                    
                    l_data.TargetNode = l_grid.NodeFromWorldPoint(p_model.transform.position + l_rndVector);
                    l_data.WaitTime = 3;
                    l_data.PathCount = 0;
                    
                    SetPathToNextWaypoint(p_model, l_closestNode);
                }
            }
            p_model.Move(l_data.PathToTarget[l_count].WorldPos, p_model.GetData().WalkSpeed);
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_dictionary.Remove(p_model);
        }
        
        private void SetPathToNextWaypoint(EnemyModel p_model, MyNode p_closestNodeToEnemy)
        {
            m_dictionary[p_model].PathToTarget = ThetaStar.Run(p_closestNodeToEnemy, m_dictionary[p_model].TargetNode,
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