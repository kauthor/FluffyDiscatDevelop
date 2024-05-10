namespace FluffyDisket.Trait
{
    public class AbilityIncreaseExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.AbilityIncrease;

        public override void Execute()
        {
            base.Execute();
            var tar = param.target as BattleUnit;
            tar.AbilityDatas.SetIncreasedAbility((StatType)(optionData.value1-100),optionData.value2);
        }
    }
}