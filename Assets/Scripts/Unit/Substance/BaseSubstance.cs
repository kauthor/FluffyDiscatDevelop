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
        public float duration;
        public float value1;
        public float value2;
        public int id { get; protected set; }

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
            Start();
        }

        public static BaseSubstance MakeAndRunSubstance(TraitOptionData data, BattleUnit target)
        {
            var type = (SubstanceType)data.value1;
            BaseSubstance ret;
            switch (type)
            {
                case SubstanceType.Poison:
                default:
                    ret = new PoisonSubstance()
                    {
                        duration = 3,
                        id = data.value1,
                        EffectOptionType = EffectOptionType.TickDamageAbs,
                        effectType = EffectType.Private,
                        value1 = 5
                    };
                    ret.Init(data, target);
                    break;
            }
            return ret;
        }
    }
}