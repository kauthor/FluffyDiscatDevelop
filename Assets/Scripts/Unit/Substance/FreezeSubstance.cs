namespace FluffyDisket.Substance
{
    public class FreezeSubstance:BaseSubstance, ISubstateStackable
    {
        public override SubstanceType tpye => SubstanceType.Freeze;

        public override float duration => 3;

        private int currentDecreaseAmount;

        public override void Start()
        {
            base.Start();
            //duration = 3;
            Owner.AbilityDatas.SetIncreasedAbility(StatType.MOVESPEED, -20);
            currentDecreaseAmount = -20;
        }

        protected override void OnExecute()
        {
            
        }

        public int MaxStack => 3;
        public int CurrentStack { get; private set; }
        public void StackThis()
        {
            //throw new System.NotImplementedException();
            if (MaxStack <= CurrentStack)
                return;
            CurrentStack++;
            current = 0;
            Owner.AbilityDatas.SetIncreasedAbility(StatType.MOVESPEED, -currentDecreaseAmount);
            if (CurrentStack == 1)
                currentDecreaseAmount = -20;
            else if (CurrentStack == 2)
                currentDecreaseAmount = -50;
            else
            {
                currentDecreaseAmount = -100;
            }
            Owner.AbilityDatas.SetIncreasedAbility(StatType.MOVESPEED, currentDecreaseAmount);
            
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            Owner.AbilityDatas.SetIncreasedAbility(StatType.MOVESPEED, -currentDecreaseAmount);
        }
    }
}