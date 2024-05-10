namespace FluffyDisket.Trait
{
    public class AdditionalDamage:SkillOptionExecuter
    {
        public override OptionType option => OptionType.AdditionalDamage;

        public override void Execute()
        {
            base.Execute();

            var tar = param?.target as BattleUnit;
            tar.SetHp(optionData.value1);
        }
    }
}