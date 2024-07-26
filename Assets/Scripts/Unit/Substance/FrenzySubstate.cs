namespace FluffyDisket.Substance
{
    public class FrenzySubstate:BaseSubstance
    {
        public override SubstanceType tpye => SubstanceType.Frenzy;

        public override void Start()
        {
            base.Start();
            Owner.AbilityDatas.SetIncreasedAbility(StatType.ATTACK, -30);
            Owner.AbilityDatas.SetIncreasedAbility(StatType.MAGICALDEF, -30);
            Owner.AbilityDatas.SetIncreasedAbility(StatType.PHYSICALDEF, -30);
        }

        protected override void OnExecute()
        {
            
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            Owner.AbilityDatas.SetIncreasedAbility(StatType.ATTACK, 30);
            Owner.AbilityDatas.SetIncreasedAbility(StatType.MAGICALDEF, 30);
            Owner.AbilityDatas.SetIncreasedAbility(StatType.PHYSICALDEF, 30);
        }
    }
}