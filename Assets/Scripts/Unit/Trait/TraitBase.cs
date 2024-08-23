using System;
using Tables;

namespace FluffyDisket.Trait
{
    public enum OptionCaseType
    {
        NONE=-1,
        UPDATE=0,
        UnderAttacked=1,
        Time,
        BattleStart, //조건 값이 있다면 시작 후 몇초 뒤 발동
        Attack,
        Critical,
        Equiped,
        EquipedVariant,
        Extra, //특수 케이스. 구현이 어려운 케이스이다.
        HpPercent,
        ArmorEquiped,
        Kill,
        AttackPer,
        AttackToSubstance,
        Stuck,
        EnemyHpPercent,
        OnDamageCalculate,
        DamageEnd,
    }

    public enum TraitCondition
    {
        NONE=0,
        EverySecond,
        AfterSecond,
        CriticalAttack,
        EquipedDetail,
        EquipedCategory,
        Extra,
        HpLower,
        EquipedExceptWeapon,
        Kill,
        EveryAttackCount,
        MyAttackMissed,
        EnemyHpPercentLower,
        PartyEquiped,
        PartyEquipedCategory,
        CriticalUnderAttacked,
        EnemyAttackMissed,
        UnderAttacked,
        Substate,
        EnemyHpPercentUpper,
        SkillUse,
        OnDead
    }

    [Serializable]
    public enum OptionType
    {
        NONE=0,
        InstantKill,
        AdditionalDamage,
        DamageIncrease,
        AbilityIncrease,
        AreaAttack,
        Attack,
        AreaSubstance,
        Substance,
        SelfSubstance,
        AttackTypeChange,
        TargetForce,
        SkillCooltimeReduce
    }

    public class UnderAttackParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.UnderAttacked;
        public DamageWrapper wrapper;
    }

    public class TimeEventParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Time;
    }
    
    public class BattleStartParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.BattleStart;
    }
    
    public class AttackParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Attack;
        public int damage;
    }
    
    public class CriticalParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Critical;
    }
    
    public class Equiped : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Equiped;
    }
    
    public class EquipedVariant : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.EquipedVariant;
    }
    
    public class ExtraParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Extra;
    }
    
    public class HpPercentParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.HpPercent;
    }
    
    public class ArmorEquiped : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.ArmorEquiped;
    }
    
    public class KillParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Kill;
    }
    
    public class AttackPerParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.AttackPer;
    }
    
    public class AttackToSubstanceParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.AttackToSubstance;
    }
    
    public class StuckParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.Stuck;
    }
    
    public class EnemyHpPercentParam : BattleEventParam
    {
        public override OptionCaseType optType => OptionCaseType.EnemyHpPercent;
    }
    
    public class TraitBase
    {
        public TraitCondition caseType => (TraitCondition)tData.traitCondition;
        public int ID;
        //public OptionType optionType;
        private BattleUnit owner;
        public BattleUnit Owner => owner;
        
        public virtual OptionType optionType
        {
            get => OptionType.NONE;
        }
        private TraitData tData;

        public virtual void Init(BattleUnit o, TraitData data)
        {
            owner = o;
            tData = data;
            //caseType = (OptionCaseType)tData.conditionType;
            MakeCommandByData();
        }

        private void MakeCommandByData()
        {
            owner.BattleEventSyetem?.AddEvent(TraitConditionToOpCase(caseType), OnOptionInvoked);
        }

        protected virtual void OnOptionInvoked(BattleEventParam param)
        {
            foreach (var d in tData.optionDatas)
            {
                SkillOptionExecuter executer = null;
                switch ((OptionType)d.battleOptionType)
                {
                    case OptionType.Attack:
                        executer = new AttackOptionExecuter()
                        {
                            param = param
                        };
                        break;
                    case OptionType.AbilityIncrease:
                        executer = new AbilityIncreaseExecuter()
                        {
                            param = param
                        };
                        break;
                    case OptionType.AdditionalDamage:
                        executer = new AdditionalDamage()
                        {
                            param = param
                        };
                        break;
                    case OptionType.DamageIncrease:
                        executer = new DamageIncreaseExecuter()
                        {
                            param = param
                        };
                        break;
                    case OptionType.InstantKill:
                        executer = new InstantKillExecuter()
                        {
                            param = param
                        };
                        break;
                    default:
                        executer = new SkillOptionExecuter()
                        {
                            param = param
                        };
                        break;
                }
                
                executer?.Execute();
            }
        }


        public static OptionCaseType TraitConditionToOpCase(TraitCondition con)
        {
            return OptionCaseType.NONE;
        }
        
#if UNITY_EDITOR
        public static void OptionInvokeEditor(OptionType op, BattleUnit caster, BattleUnit target)
        {
            SkillOptionExecuter executer = null;
            var param = new BattleEventParam()
            {
                eventMaker = caster,
                target = target
            };
            switch ((OptionType)op)
            {
                
                case OptionType.Attack:
                    executer = new AttackOptionExecuter()
                    {
                        param = param
                    };
                    break;
                case OptionType.AbilityIncrease:
                    executer = new AbilityIncreaseExecuter()
                    {
                        param = param
                    };
                    break;
                case OptionType.AdditionalDamage:
                    executer = new AdditionalDamage()
                    {
                        param = param
                    };
                    break;
                case OptionType.DamageIncrease:
                    executer = new DamageIncreaseExecuter()
                    {
                        param = param
                    };
                    break;
                case OptionType.InstantKill:
                    executer = new InstantKillExecuter()
                    {
                        param = param
                    };
                    break;
                default:
                    executer = new SkillOptionExecuter()
                    {
                        param = param
                    };
                    break;
            }
                
            executer?.Execute();
        }
#endif
    }
}