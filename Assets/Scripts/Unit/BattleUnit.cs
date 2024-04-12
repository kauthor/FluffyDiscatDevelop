using System;
using System.Collections.Generic;
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
        
        public void UpdateRuntimeData(int levelDelta, float atkDelta, float pdDelta, float mdDelta, float hpDelta)
        {
            levelStat?.UpdateRuntimeData(levelDelta, atkDelta, pdDelta, mdDelta, hpDelta);
        }

        public CharacterAbilityDatas(LevelAdditionalStat levels, CharacterStat bs)
        {
            levelStat = levels;
            baseStat = bs;
        }

        public float HpMax
        {
            get
            {
                if (baseStat == null)
                    return 10;
                return baseStat.HpMax + (levelStat?.HpMax ?? 0);
            }
        }
        public float MoveSpeed
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.MoveSpeed;
            }
        }

        public float Range
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.Range;
            }
        }
        
        public float Atk
        {
            get
            {
                if (baseStat == null)
                    return 5;
                return baseStat.Atk + (levelStat?.Attack ?? 0);
            }
        }

        public float phyDef
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.phyDef + (levelStat?.PhyDef ?? 0);
            }
        }
        public float magDef
        {
            get
            {
                if (baseStat == null)
                    return 0;
                return baseStat.magDef + (levelStat?.MagicDef ?? 0);
            }
        }

        public int hpRegen
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.hpRegen;
            }
        }
        public float hpAbsolve
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.hpAbsolve;
            }
        }
        public float crit
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.crit;
            }
        }
        public float critDam
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.critDam;
            }
        }
        public float atkSpeed
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.AttackCoolTime;
            }
        }
        public int dodge
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.dodge;
            }
        }
        
        public float atkIncrease
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.atkIncrease;
            }
        }
        public float damageDecrease
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.damageDecrease;
            }
        }
        public float AOEArea
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.AOEArea;
            }
        }

        public float accuracy
        {
            get
            {
                if (baseStat == null)
                    return 1;
                return baseStat.accuracy;
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

        private void SetAbilityData(CharacterStat baseStat, LevelAdditionalStat lev)
        {
            abilityDatas = new CharacterAbilityDatas(lev, baseStat);
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

        public void SetStat(CharacterStat s, LevelAdditionalStat lev=null)
        {
            //characterAbility = s;
            //HPMax = s.HpMax;
            SetAbilityData(s,lev);
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

        public void SetHpPercent(float per)
        {
            currentHp = MaxHp * per;
        }

        protected virtual void OnDead()
        {
            BattleManager.GetInstance().currentView.ReceiveLog(
                $"{CharacterClassPublic}가 쓰러졌다! 1972년 11월 21일...");
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

        
        
    }
    
}