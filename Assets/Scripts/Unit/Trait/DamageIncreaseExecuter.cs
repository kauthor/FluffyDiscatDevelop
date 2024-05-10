namespace FluffyDisket.Trait
{
    public class DamageIncreaseExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.DamageIncrease;

        public override void Execute()
        {
            base.Execute();
            if (param is AttackParam atp)
            {
                atp.damage += (int)(atp.damage * (float)optionData.value2 / 100.0f);
            }
        }
    }
}