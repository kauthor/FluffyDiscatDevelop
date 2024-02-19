using System;
using UnityEngine;

namespace FluffyDisket
{
    public class SkillState:BattleState
    {
        [SerializeField] private float castTime=1;
        [SerializeField] protected float damage = 0;
        private float timeTemp = 0;
        public override State State => State.Skill;


        protected virtual void SkillUse()
        {
            
        }
        
        protected override void OnState(State prevState = State.Idle, StateParam param = null)
        {
            base.OnState(prevState, param);
            SkillUse();
            timeTemp = 0;
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            timeTemp += Time.deltaTime;
            if (timeTemp >= castTime)
            {
                TryEndState(State.Idle);
            }
        }

        protected override void OnEnd(State nextState = State.Idle, Action onEnd = null)
        {
            base.OnEnd(nextState, onEnd);
            
            timeTemp = 0;
            owner.FindEnemy();
        }
    }
}