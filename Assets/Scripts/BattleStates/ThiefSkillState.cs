using UnityEngine;

namespace FluffyDisket
{
    public class ThiefSkillState:SkillState
    {
        protected override void SkillUse()
        {
            base.SkillUse();
            var enemy= owner.Team.GetWeakestEnemy();

            if (enemy != null)
            {
                owner.transform.position =
                    enemy.transform.position + new Vector3(owner.CharacterAbility.Range / 2.0f, 0, 0);
                enemy.SetHp(-damage);
            }
            
            if(enemy.IsDead)
              owner.FindEnemy();
            else
            {
                owner.ChangeState(State.BaseAttack, new StateParam()
                {
                    target = enemy
                });
            }
        }
    }
}