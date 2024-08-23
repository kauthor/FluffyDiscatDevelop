using FluffyDisket.Substance;
using Tables;

namespace FluffyDisket.Trait
{
    public class SelfSubstateExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.SelfSubstance;

        public override void Execute()
        {
            base.Execute();
            var tar = param.eventMaker as BattleUnit;
            var subData = ExcelManager.GetInstance().SubstanceT.GetSubstanceOption(optionData.value1);
            BaseSubstance.MakeAndRunSubstance(optionData, tar);
        }
    }
}