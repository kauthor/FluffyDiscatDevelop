using UnityEngine;

namespace FluffyDisket
{
    public class FindState:BattleState
    {
        
        public override State DeclaredState => State.Find;

        protected override void OnState(State prevState = State.Idle, StateParam param = null)
        {
            base.OnState(prevState, param);
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            if (owner == null || receivedParam == null || receivedParam.target == null)
                return;
            var tr = owner.transform;
            var targetTr = receivedParam.target.transform;

            var dist = tr.position - targetTr.position;
            if (Vector3.SqrMagnitude(dist) >= owner.AbilityDatas.Range * owner.AbilityDatas.Range*0.03f*0.03f)
            {
                var speedDelta = (-dist).normalized * owner.AbilityDatas.MoveSpeed * Time.deltaTime*0.01f;
                tr.position += speedDelta;
            }
            else
            {
                owner.ChangeState(State.BaseAttack, receivedParam);
            }
        }
    }
}