namespace FluffyDisket.Trait
{
    public class DamageWrapper
    {
        public float damage;
        public bool isMissed;
    }
    
    public class AttackOptionExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.Attack;

        public override void Execute()
        {
            base.Execute();
            var attacker = param.eventMaker as BattleUnit;
            var target = param.target as BattleUnit;

            var dam = attacker.AbilityDatas.Atk - target.AbilityDatas.phyDef;
            attacker.BattleEventSyetem.FireEvent(OptionCaseType.Attack, new AttackParam()
            {
                damage = (int)dam,
                target = target,
                eventMaker = attacker
            });

            var wrap = new DamageWrapper()
            {
                damage = dam,
                isMissed = false
            };
            target.BattleEventSyetem.FireEvent(OptionCaseType.UnderAttacked, new UnderAttackParam()
            {
                target = target,
                eventMaker = attacker,
                wrapper = wrap,
            });
            if(!wrap.isMissed)
                target.SetHp(wrap.damage);
        }
    }
}