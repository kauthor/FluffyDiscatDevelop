using Tables;

namespace FluffyDisket.Trait
{
    public enum OptionCaseType
    {
        NONE=0,
        UnderAttacked=1,
        Attack=2,
        BattleStart=3,
        CoolTime=4
    }

    public enum OptionType
    {
        NONE=0,
        MultiAttack=1,
        DamageIgnore,
        DamageDecrease,
        AttackIncrease,
        BackStab,
    }
    
    
    public class TraitBase
    {
        public OptionCaseType caseType;
        public int ID;
        public OptionType optionType;
        private BattleUnit owner;
        public BattleUnit Owner => owner;
        private TraitData tData;

        public virtual void Init(BattleUnit o, TraitData data)
        {
            owner = o;
            tData = data;
            MakeCommandByData();
        }

        private void MakeCommandByData()
        {
            owner.BattleEventSyetem?.AddEvent(OptionType.NONE, OnOptionInvoked);
        }

        protected virtual void OnOptionInvoked(BattleEventParam param)
        {
            
        }
    }
}