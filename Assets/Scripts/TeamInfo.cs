using System;
using System.Linq;
using UnityEngine;

namespace FluffyDisket
{
    public class TeamInfo
    {
        public BattleUnit[] members;
        public bool IsPlayer = false;
        private int memberCount;
        private int deadCount;

        public bool IsDefeated => deadCount >= memberCount && memberCount > 0;

        public TeamInfo(BattleUnit[] mem, bool isPlayer, int dead=0)
        {
            members = mem;
            foreach (var m in members)
            {
                m.SetTeam(this, OnMemberDead);
            }

            memberCount = members.Length;
            deadCount = dead;
            IsPlayer = isPlayer;
        }

        public void StartBattle()
        {
            foreach (var mem in members)
            {
                if(!mem.IsDead)
                   OrderToMember(mem);
            }
        }

        private TeamInfo GetEnemy() => BattleManager.GetInstance()?.GetEnemy(IsPlayer) ?? null;

        public void YieldForOrder(BattleUnit unit)
        {
            OrderToMember(unit);
        }

        private void OrderToMember(BattleUnit unit)
        {
            var weakest = GetWeakestEnemy();
            
            if(weakest==null)
                unit.ChangeState(State.Idle);
            else
                unit.ReceiveOrder(new StateParam()
                {
                    target = weakest
                });
        }

        public BattleUnit GetWeakestEnemy()
        {
            var enemy = GetEnemy();
            if (enemy == null)
            {
                return null;
            }

            BattleUnit weakest = null;

            foreach (var enemyMember in enemy.members)
            {
                if(enemyMember.IsDead)
                    continue;

                if (weakest == null || weakest.currentHp > enemyMember.currentHp)
                    weakest = enemyMember;
            }

            return weakest;
        }

        public BattleUnit GetFarEnemy(BattleUnit unit)
        {
            var enemy = GetEnemy();
            if (enemy == null)
            {
                return null;
            }

            BattleUnit far = null;
            float dist = 0;

            foreach (var enemyMember in enemy.members)
            {
                if(enemyMember.IsDead)
                    continue;

                if (far == null)
                    far = enemyMember;
                else
                {
                    var curdist = Vector3.SqrMagnitude(unit.transform.position - enemyMember.transform.position);
                    if (curdist > dist)
                    {
                        dist = curdist;
                        far = enemyMember;
                    }
                }
            }

            return far;
        }

        private void OnMemberDead()
        {
            deadCount++;
            if (deadCount >= memberCount)
            {
                BattleManager.GetInstance().EndBattle();
            }
        }
    }
}