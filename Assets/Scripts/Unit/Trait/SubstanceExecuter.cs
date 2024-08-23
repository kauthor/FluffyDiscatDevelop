using FluffyDisket.Substance;
using Tables;

namespace FluffyDisket.Trait
{
    public class SubstanceExecuter:SkillOptionExecuter
    {
        public override OptionType option => OptionType.Substance;

        public override void Execute()
        {
            base.Execute();
            
            
            var tar = param.target as BattleUnit;
            var subData = ExcelManager.GetInstance().SubstanceT.GetSubstanceOption(optionData.value1);
            BaseSubstance.MakeAndRunSubstance(optionData, tar);
        }
    }
}