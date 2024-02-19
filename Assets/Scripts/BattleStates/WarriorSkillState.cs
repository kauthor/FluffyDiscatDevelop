namespace FluffyDisket
{
    public class WarriorSkillState:SkillState
    {
        protected override void SkillUse()
        {
            base.SkillUse();
            var enemy= BattleManager.GetInstance().GetEnemy(owner.isPlayer);
            foreach (var mem in enemy.members)
            {
                mem.ChangeState(State.Find, new StateParam()
                {
                    target = owner
                });
            }
        }
    }
}