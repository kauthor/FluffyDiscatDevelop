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
                BattleManager.GetInstance().currentView.ReceiveLog(
                    $"{owner.CharacterClassPublic}가 {enemy.CharacterClassPublic}에게 스킬 공격! {damage} 데미지");
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