﻿using System.Collections.Generic;
using _Main._main.Scripts.Entities.Enemies;
using _Main._main.Scripts.Extensions;
using _Main._main.Scripts.FSM.Base;
using UnityEngine;

namespace _Main._main.Scripts.FSM.States.EnemyStates.States
{
    [CreateAssetMenu(fileName = "ShootState", menuName = "_main/States/EnemyStates/ShootState", order = 0)]
    public class ShootState : State
    {
        private class ShootData
        {
            public float TimeBtwRounds;
            public float TimeBtwShots;
            public int ShootsPerRound;
            public int CurrShots;
        }

        private Dictionary<EnemyModel, ShootData> m_dictionary = new Dictionary<EnemyModel, ShootData>();

        public override void EnterState(EnemyModel p_model)
        {
            Debug.Log("Shoot");
            p_model.SetTimeToEndAction(p_model.GetData().ShootTime);
            
            var l_data = p_model.GetData();
            m_dictionary[p_model] = new ShootData();
            m_dictionary[p_model].TimeBtwRounds = 0;
            m_dictionary[p_model].TimeBtwShots = 0.1f;
            m_dictionary[p_model].CurrShots = 0;
            m_dictionary[p_model].ShootsPerRound = l_data.ShotsPerRound;
            
            p_model.SbController.SetZeroSb();
        }

        public override void ExecuteState(EnemyModel p_model)
        {
            p_model.LookAt(p_model.GetTargetTransform().position);
            
            var l_data = m_dictionary[p_model];
            l_data.TimeBtwRounds -= Time.deltaTime;
            
            if (l_data.TimeBtwRounds <= 0)
            {
                l_data.TimeBtwShots -= Time.deltaTime;

                if (l_data.TimeBtwShots <= 0 && l_data.CurrShots <= l_data.ShootsPerRound)
                {
                    var l_dirToTarget = (p_model.GetTargetTransform().position - p_model.transform.position).normalized;
                    var l_spreadAngle = p_model.GetData().Spread;
                    
                    l_dirToTarget =
                        Quaternion.Euler(l_spreadAngle * Random.Range(-1f, 1f), l_spreadAngle * Random.Range(-1f, 1f), 0) * l_dirToTarget;

                    p_model.Shoot(l_dirToTarget);
                    l_data.CurrShots++;
                    l_data.TimeBtwShots = 0.1f;
                }
                else if(l_data.CurrShots >= l_data.ShootsPerRound)
                {
                    l_data.TimeBtwRounds = p_model.GetData().TimeBtwRounds;
                    
                    l_data.CurrShots = 0;
                }
            }
        }

        public override void ExitState(EnemyModel p_model)
        {
            m_dictionary.Remove(p_model);
        }
    }
}