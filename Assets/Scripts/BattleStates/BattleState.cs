using System;
using UnityEngine;

namespace FluffyDisket
{
    public enum State
    {
        Idle=0,
        Find=1,
        BaseAttack=2,
        Skill=3,
    }

    public class StateParam
    {
        public BattleUnit target;
        public float time;
    }
    public class BattleState : MonoBehaviour
    {
        public virtual State DeclaredState => State.Idle;
        [HideInInspector] public BattleUnit owner;
        public StateParam receivedParam;

        public virtual State State => DeclaredState;

        protected virtual void OnState(State prevState = State.Idle, StateParam param=null)
        {
            receivedParam = param;
        }

        public virtual void Execute()
        {
            OnExecute();
        }
        
        protected virtual void OnExecute()
        {
            
        }

        protected virtual void OnEnd(State nextState = State.Idle, Action onEnd=null)
        {
            receivedParam = null;
            onEnd?.Invoke();
        }

        public void TryEndState(State next, Action onEnd = null)
        {
            OnEnd(next, onEnd);
        }

        public void TryStart(State prev = State.Idle, StateParam param=null)
        {
            OnState(prev, param);
        }
    }
}