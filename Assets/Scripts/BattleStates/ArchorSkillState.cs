using UnityEngine;

namespace FluffyDisket
{
    public class ArchorSkillState:SkillState
    {
        protected override void SkillUse()
        {
            base.SkillUse();
            var enemy= owner.Team.GetFarEnemy(owner);

            if (enemy != null)
            {
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