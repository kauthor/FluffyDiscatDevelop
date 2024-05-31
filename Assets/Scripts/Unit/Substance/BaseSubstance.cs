using FluffyDisket.Trait;
using Tables;
using UnityEngine;

namespace FluffyDisket.Substance
{
    public enum SubstanceType
    {
        NONE=0,
        Stun=1,
        Silence,
        Bleed,
        Poison,
        Burn,
        MAX
    }

    public enum EffectOptionType
    {
        NONE=0,
        Stun,
        SkillDeny,
        TickDamagePer,
        TickDamageAbs,
        Slow,
    }

    public enum EffectType
    {
        NONE=0,
        Private,
        Stack,
        InfStack
    }

    public enum ResetType
    {
        NONE=0,
        RestoreWhenStageEnd,
        Remain
    }
    
    
    public abstract class BaseSubstance
    {
        public abstract SubstanceType tpye { get; }
        public EffectOptionType EffectOptionType;
        public EffectType effectType;
        public ResetType resetType;

        public float duration => subOpData.duration;
        //차후 배열을 전부 적용하게 바꾸자.
        public float value1 => subOpData.options[0].value1;
        public float value2;
        public int id { get; protected set; }

        private TraitOptionData opData;
        private SubstanceOption subOpData;
        private SubstanceEffectOption subEfOp;
        protected float current;
        public BattleUnit Owner { get; private set; }

        protected abstract void OnExecute();

        public virtual void Start()
        {
            Owner?.BattleEventSyetem.AddEvent(OptionCaseType.UPDATE, Execute);
            current = 0;
        }

        public void Execute(BattleEventParam param)
        {
            current += Time.deltaTime;
            if (current >= duration)
            {
                Finish();
                return;
            }
            OnExecute();
        }

        public void Finish()
        {
            Owner?.BattleEventSyetem.RemoveEvent(OptionCaseType.UPDATE,Execute);
        }

        private void Init(TraitOptionData data, BattleUnit target)
        {
            id = data.value1;
            Owner = target;
            opData = data;
            Start();
        }

        public static BaseSubstance MakeAndRunSubstance(TraitOptionData data, BattleUnit target)
        {
            var type = (SubstanceType)data.value1;
            BaseSubstance ret;
            var dat = ExcelManager.GetInstance().SubstanceT.GetSubstanceOption(data.value1);
            
            switch (type)
            {
                case SubstanceType.Poison:
                default:
                    ret = new PoisonSubstance()
                    {
                        id = data.value1,
                        EffectOptionType = EffectOptionType.TickDamageAbs,
                        effectType = EffectType.Private,
                        subOpData = dat
                    };
                    ret.Init(data, target);
                    break;
            }
            return ret;
        }
    }
}