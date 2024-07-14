using System;
using FluffyDisket.Trait;
using FluffyDisket.UI;
using UnityEngine;

namespace FluffyDisket
{
    public class AttackState:BattleState
    {
        private float coolRegain = 0;
        [SerializeField] private float damage;
        public override State State => State.BaseAttack;

        protected override void OnState(State prevState = State.Idle, StateParam param = null)
        {
            base.OnState(prevState, param);
            coolRegain = 0;
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            if (receivedParam == null || receivedParam.target == null)
            {
                TryEndState(State.Idle);
            }

            float dam = owner.AbilityDatas.Atk > 0 ? owner.AbilityDatas.Atk : damage;
            dam -=receivedParam.target.AbilityDatas?.phyDef?? 0;
            dam = dam <= 0 ? 0 : dam;
            
            
            coolRegain += Time.deltaTime;
            if (coolRegain >= owner.AbilityDatas.atkSpeed)
            {
                owner.BattleEventSyetem.FireEvent(OptionCaseType.Attack, new AttackParam()
                {
                    target = receivedParam.target,
                    eventMaker = owner,
                    damage = (int)dam
                });
                receivedParam.target.BattleEventSyetem.FireEvent(OptionCaseType.UnderAttacked, new UnderAttackParam()
                {
                    target = receivedParam.target,
                    eventMaker = owner
                });
                receivedParam.target.SetHp(-dam);
                BattleManager.GetInstance().currentView.ReceiveLog(
                    $"{owner.CharacterClassPublic}가 {receivedParam.target.CharacterClassPublic}에게 공격! {dam} 데미지");
                coolRegain = 0;
                
                var tr = owner.transform;
                var targetTr = receivedParam.target.transform;

                var dist = tr.position - targetTr.position;
                if (Vector3.SqrMagnitude(dist) >= owner.AbilityDatas.Range * owner.AbilityDatas.Range)
                {
                    owner.ChangeState(State.Find, receivedParam);
                }
                else if(receivedParam.target.IsDead)
                {
                    TryEndState(State.Idle);
                }
            }
        }

        protected override void OnEnd(State nextState = State.Idle, Action onEnd = null)
        {
            base.OnEnd(nextState, onEnd);
            coolRegain = 0;
            if (nextState == State.Idle)
            {
                owner.FindEnemy();
            }
        }
    }
}