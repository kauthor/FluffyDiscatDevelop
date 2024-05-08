using System;
using System.Collections.Generic;
using FluffyDisket.Trait;
using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket
{
    public enum Job
    {
        Warrior=0,
        Archor =1,
        Thief =2,
        Monster=100
    }

    public enum StatType
    {
        NONE=0,
        
        ATTACK,
        PHYSICALDEF,
        MAGICALDEF,
        HPMAX,
        HPREGEN,
        HPABSOLVE,
        CRITICAL,
        CRITICALDAMAGE,
        ATTACKSPEED,
        DODGE,
        RANGE,
        MOVESPEED,
        ATTACKINCREASE,
        DAMAGEDECREASE,
        AOEAREA,
        ACCURACY
    }

    public class BattleEventParam
    {
        public IUnit eventMaker;
        public IUnit target;
        public virtual OptionCaseType optType => OptionCaseType.NONE;
    }

    public class EventSystem
    {
        private Dictionary<OptionCaseType, Action<BattleEventParam>> events;

        public virtual void Init()
        {
            events = new Dictionary<OptionCaseType, Action<BattleEventParam>>();
            for (int i = 0; i < 5; i++)
            {
                events.Add((OptionCaseType)i, null);
            }
        }

        public void AddEvent(OptionCaseType type, Action<BattleEventParam> ev)
        {
            if (events.TryGetValue(type, out Action<BattleEventParam> act))
            {
                act += ev;
            }
        }

        public void RemoveEvent(OptionCaseType type, Action<BattleEventParam> ev)
        {
            if (events.TryGetValue(type, out Action<BattleEventParam> act))
            {
                act -= ev;
            }
        }

        public void FireEvent(OptionCaseType type, BattleEventParam param)
        {
            if (events.TryGetValue(type, out Action<BattleEventParam> act))
            {
                act?.Invoke(param);
            }
        }
    }
    

    [Serializable]
    public class CharacterStat
    {
        public float HpMax;
        public float MoveSpeed;
        public float Range;
        //public float SkillCoolTIme;
        public float AttackCoolTime;
        public float Atk;
        public float phyDef;
        public float magDef;

        public int hpRegen;
        public float hpAbsolve;
        public float crit;
        public float critDam;
        //public float atkSpeed;
        public int dodge;
        //public int moveSpeedNew;
        public float atkIncrease;
        public float damageDecrease;
        public float AOEArea;
        public float accuracy;

        public float levelAtk;
        public float levelHp;
        public float levelpd;
        public float levelmd;

    }

    public class LevelAdditionalStat
    {
        private int level;
        public int Level => level;
        private float hpMax;
        public float HpMax => hpMax;
        private float Atk;
        public float Attack => Atk;
        private float phyDef;
        public float PhyDef => phyDef;
        private float magDef;
        public float MagicDef => magDef;

        /// <summary>
        /// Initialize Player Level Data
        /// </summary>
        public LevelAdditionalStat(float hp,float atk, float pD, float mD, int lv = 1)
        {
            level = lv;
            hpMax = hp;
            Atk = atk;
            phyDef = pD;
            magDef = mD;
        }

        /// <summary>
        /// Initialize Monster Level Data
        /// </summary>
        public LevelAdditionalStat(int monsterID, float hpMax, float atk, float pd, float md)
        {
            var monData = ExcelManager.GetInstance().MonsterT.GetMonsterData(monsterID);
            level = AccountManager.GetInstance().CurrentMonsterLevel;
            var levelData = AccountManager.GetInstance().CurrentMonsterLevelData;
            this.hpMax = hpMax * (float)levelData.monsterBaseStat / 10000.0f;
            Atk = atk * (float)levelData.monsterBaseStat / 10000.0f;
            phyDef = pd * (float)levelData.monsterBaseStat / 10000.0f;
            magDef = md * (float)levelData.monsterBaseStat / 10000.0f;
        }

        public void UpdateRuntimeData(int levelDelta, float atkDelta, float pdDelta, float mdDelta, float hpDelta)
        {
            level += levelDelta;
            hpMax += hpDelta;
            Atk += atkDelta;
            phyDef += pdDelta;
            magDef += mdDelta;
        }
    }

    public class CharacterAbilityDatas
    {
        private LevelAdditionalStat levelStat;
        private CharacterStat baseStat;
        private Dictionary<StatType, int> absStatDelta;
        private Dictionary<StatType, int> ratioStatDelta;
        
        public void UpdateRuntimeData(int levelDelta, float atkDelta, float pdDelta, float mdDelta, float hpDelta)
        {
            levelStat?.UpdateRuntimeData(levelDelta, atkDelta, pdDelta, mdDelta, hpDelta);
        }

        public CharacterAbilityDatas(LevelAdditionalStat levels, CharacterStat bs, List<TraitData> trait)
        {
            levelStat = levels;
            baseStat = bs;
            absStatDelta = new Dictionary<StatType, int>();

            ratioStatDelta = new Dictionary<StatType, int>();

            if(trait!=null)
            {
                foreach (var t in trait)
                {
                    if (t.optionDatas.Length > 0)
                    {
                        foreach (var op in t.optionDatas)
                        {
                            if (op.battleOptionType >= 101 && op.battleOptionType <= 116)
                            {
                                StatType tp = (StatType)(op.battleOptionType-100);
                                if (!absStatDelta.ContainsKey(tp))
                                    absStatDelta.Add(tp, op.value1);
                                else absStatDelta[tp] += op.value1;

                                if (!ratioStatDelta.ContainsKey(tp))
                                    ratioStatDelta.Add(tp, op.value2);
                                else ratioStatDelta[tp] += op.value2;
                            }
                        }
                    }
                    
                }
            }
        }

        public int Level
        {
            get
            {
                if (levelStat == null)
                    return 1;
                return levelStat.Level;
            }
        }
        
        public float HpMax
        {
            get
            {
                if (baseStat == null)
                    return 10;

                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.HPMAX, out val1);
                ratioStatDelta.TryGetValue(StatType.HPMAX, out val2);
                    
                return (baseStat.HpMax + (levelStat?.HpMax ?? 0) + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float MoveSpeed
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.MOVESPEED, out val1);
                ratioStatDelta.TryGetValue(StatType.MOVESPEED, out val2);
                
                return (baseStat.MoveSpeed +val1) * ((float)(100.0f+val2)/100.0f);
            }
        }

        public float Range
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.RANGE, out val1);
                ratioStatDelta.TryGetValue(StatType.RANGE, out val2);
                
                
                return (baseStat.Range+val1)* ((float)(100.0f+val2)/100.0f);
            }
        }
        
        public float Atk
        {
            get
            {
                if (baseStat == null)
                    return 5;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.ATTACK, out val1);
                ratioStatDelta.TryGetValue(StatType.ATTACK, out val2);
                return (baseStat.Atk + (levelStat?.Attack ?? 0) + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }

        public float phyDef
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.PHYSICALDEF, out val1);
                ratioStatDelta.TryGetValue(StatType.PHYSICALDEF, out val2);
                
                return (baseStat.phyDef + (levelStat?.PhyDef ?? 0) + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float magDef
        {
            get
            {
                if (baseStat == null)
                    return 0;
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.MAGICALDEF, out val1);
                ratioStatDelta.TryGetValue(StatType.MAGICALDEF, out val2);
                
                return (baseStat.magDef + (levelStat?.MagicDef ?? 0) + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }

        public float hpRegen
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.HPREGEN, out val1);
                ratioStatDelta.TryGetValue(StatType.HPREGEN, out val2);
                
                return (baseStat.hpRegen + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float hpAbsolve
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.HPABSOLVE, out val1);
                ratioStatDelta.TryGetValue(StatType.HPABSOLVE, out val2);
                
                return (baseStat.hpAbsolve + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float crit
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.CRITICAL, out val1);
                ratioStatDelta.TryGetValue(StatType.CRITICAL, out val2);
                
                return (baseStat.crit + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float critDam
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.CRITICALDAMAGE, out val1);
                ratioStatDelta.TryGetValue(StatType.CRITICALDAMAGE, out val2);
                
                return (baseStat.critDam + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float atkSpeed
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.ATTACKSPEED, out val1);
                ratioStatDelta.TryGetValue(StatType.ATTACKSPEED, out val2);
                
                return (baseStat.AttackCoolTime + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float dodge
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.DODGE, out val1);
                ratioStatDelta.TryGetValue(StatType.DODGE, out val2);
                
                return (baseStat.dodge + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        
        public float atkIncrease
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.ATTACKINCREASE, out val1);
                ratioStatDelta.TryGetValue(StatType.ATTACKINCREASE, out val2);
                
                return (baseStat.atkIncrease + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float damageDecrease
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.DAMAGEDECREASE, out val1);
                ratioStatDelta.TryGetValue(StatType.DAMAGEDECREASE, out val2);
                
                return (baseStat.damageDecrease + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
        public float AOEArea
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.AOEAREA, out val1);
                ratioStatDelta.TryGetValue(StatType.AOEAREA, out val2);
                
                return (baseStat.AOEArea + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }

        public float accuracy
        {
            get
            {
                if (baseStat == null)
                    return 1;
                
                int val1 = 0;
                int val2 = 0;
                absStatDelta.TryGetValue(StatType.ACCURACY, out val1);
                ratioStatDelta.TryGetValue(StatType.ACCURACY, out val2);
                
                return (baseStat.accuracy + val1) * ((float)(100.0f+val2)/100.0f);
            }
        }
    }
    
    public class BattleUnit : IUnit
    {

        [SerializeField] private BattleState[] inspectorStates;
        //[SerializeField] private float HPMax=100;
        [SerializeField] private Job CharacterClass;
        [SerializeField] private Transform hpBar;
        [SerializeField] private bool CanUseSkillFirst;
        //[SerializeField] private PlayerSubTable table;
        //[SerializeField] private CharacterStat characterAbility;

        private float skillCoolTimeRegain;
        public float SkillCoolRegain => skillCoolTimeRegain;
        //public CharacterStat CharacterAbility => table? table.stat: characterAbility;
        private CharacterAbilityDatas abilityDatas;
        public CharacterAbilityDatas AbilityDatas => abilityDatas;

        public EventSystem BattleEventSyetem;

        private List<TraitBase> managedTrait;

        private void SetAbilityData(CharacterStat baseStat, LevelAdditionalStat lev, List<TraitData> trait)
        {
            abilityDatas = new CharacterAbilityDatas(lev, baseStat, trait);
        }
        
        
        private TeamInfo OurTeam;
        public TeamInfo Team => OurTeam;
        public Job CharacterClassPublic => isPlayer? CharacterClass : Job.Monster;
        private Action OnDeadCb;
        public bool isPlayer => OurTeam.IsPlayer;

        //private bool hasSkill = false;

        //public float atkDamage => table !=null ? table.stat.Atk : 0;

        public float MaxHp => abilityDatas?.HpMax??10;

        public event Action<BattleUnit> onOwnerUpdate;
        public event Action<float> onHpUpdate;
        public event Action OnOwnerDead;

        public void SetTeam(TeamInfo team, Action onDead)
        { 
            OurTeam = team;
            OnDeadCb = onDead;
        }

        private float _currentHp;
        public float currentHp
        {
            get => _currentHp;
            private set
            {
                _currentHp = value;
                if (_currentHp <= 0)
                {
                    OnOwnerDead?.Invoke();
                    gameObject.SetActive(false);
                }
                else
                {
                    onHpUpdate?.Invoke(_currentHp/MaxHp);
                }
            }
        }

        public void SetStat(CharacterStat s, LevelAdditionalStat lev=null, List<TraitData> trait=null)
        {
            //characterAbility = s;
            //HPMax = s.HpMax;
            SetAbilityData(s,lev,trait);
            currentHp = MaxHp;
        }

        public void SetHp(float delta)
        {
            if (IsDead)
                return;
            currentHp += delta;
            if(IsDead)
                OnDead();
        }

        public void Kill()
        {
            SetHpPercent(0);
            OnDead();
        }

        public void SetHpPercent(float per)
        {
            currentHp = MaxHp * per;
        }

        protected virtual void OnDead()
        {
            BattleManager.GetInstance().currentView.ReceiveLog(
                $"{CharacterClassPublic}가 쓰러졌다!");
            OnDeadCb?.Invoke();
        }

        public bool CanUseSkill(bool getInput=false)
        {
            /*if (!hasSkill)
                return false;
            if (IsDead)
                return false;
            if (GameManager.GetInstance().IsAuto || !OurTeam.IsPlayer || getInput)
                return skillCoolTimeRegain >= CharacterAbility.SkillCoolTIme;*/

            return false;
        }

        public bool IsDead => currentHp <= 0;
        
        private Dictionary<State, BattleState> FiniteStateMachineDic = new Dictionary<State, BattleState>();

        private State currentState = State.Idle;
        private BattleState currentBattleState = null;

        private void Awake()
        {
            //currentHp = MaxHp;
            onOwnerUpdate = null;
            FiniteStateMachineDic = new Dictionary<State, BattleState>();
            managedTrait = new List<TraitBase>();
            if (inspectorStates.Length > 0)
            {
                foreach (var st in inspectorStates)
                {
                    st.owner = this;
                    FiniteStateMachineDic.Add(st.State, st);
                }
            }
            else
            {
                Debug.LogError("None Serialized Object.");
            }

            currentState = State.Idle;
            ChangeState(State.Idle);
            //hasSkill = FiniteStateMachineDic.ContainsKey(State.Skill);
            skillCoolTimeRegain = 0;
            //if (CanUseSkillFirst)
            //    skillCoolTimeRegain = CharacterAbility.SkillCoolTIme;

            BattleEventSyetem = new EventSystem();
            BattleEventSyetem.Init();
        }

        public void ChangeState(State nextState, StateParam param =null)
        {
            if (!FiniteStateMachineDic.TryGetValue(currentState, out BattleState current))
            {
                Debug.LogError($"{name} doesn't have the {currentState} state");
                return;
            }

            if (!FiniteStateMachineDic.TryGetValue(nextState, out BattleState next))
            {
                Debug.LogError($"{name} doesn't have the {nextState} state");
                return;
            }
            
            current.TryEndState(nextState, () =>
            {
                currentState = nextState;
                next.TryStart(nextState, param);
                currentBattleState = next;
            });
        }


        private void Update()
        {
            if (BattleManager.ExistInstance())
            {
                if (!BattleManager.GetInstance().InBattle)
                    return;

                skillCoolTimeRegain += Time.deltaTime;
                onOwnerUpdate?.Invoke(this);
                if (CanUseSkill())
                {
                    skillCoolTimeRegain = 0;
                    ChangeState(State.Skill);
                }
                else if (currentBattleState != null)
                {
                    currentBattleState.Execute();
                }
            }
        }

        public void SkillUse(bool input=false)
        {
            if (CanUseSkill(input))
            {
                skillCoolTimeRegain = 0;
                ChangeState(State.Skill);
            }
        }

        public virtual void FindEnemy()
        {
            OurTeam.YieldForOrder(this);
        }

        public virtual void ReceiveOrder(StateParam param)
        {
            ChangeState(State.Find, param);
        }

        public virtual void StageTrait(TraitData data)
        {
            var tra = new TraitBase();
            tra.Init(this, data);
            if (managedTrait == null)
                managedTrait = new List<TraitBase>();
            managedTrait.Add(tra);
            
            
        }
        
    }
    
}