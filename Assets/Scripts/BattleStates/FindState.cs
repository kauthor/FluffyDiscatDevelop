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
            if (Vector3.SqrMagnitude(dist) >= owner.CharacterAbility.Range * owner.CharacterAbility.Range)
            {
                var speedDelta = (-dist).normalized * owner.CharacterAbility.MoveSpeed * Time.deltaTime;
                tr.position += speedDelta;
            }
            else
            {
                owner.ChangeState(State.BaseAttack, receivedParam);
            }
        }
    }
}