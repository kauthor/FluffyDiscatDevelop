namespace FluffyDisket.Trait
{
    public class TargetForceExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.TargetForce;

        public override void Execute()
        {
            base.Execute();
            var owner = param.eventMaker as BattleUnit;
            owner.TargetTo(optionData.value1==0);
        }
    }
}