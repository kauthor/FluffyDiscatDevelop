using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket
{
    public enum Job
    {
        Warrior=0,
        Archor =1,
        Thief =2
    }

    [Serializable]
    public class CharacterStat
    {
        public float HpMax;
        public float MoveSpeed;
        public float Range;
        public float SkillCoolTIme;
        public float AttackCoolTime;
    }
    
    public class BattleUnit : IUnit
    {

        [SerializeField] private BattleState[] inspectorStates;
        [SerializeField] private float HPMax=100;
        [SerializeField] private Job CharacterClass;
        [SerializeField] private Transform hpBar;
        [SerializeField] private bool CanUseSkillFirst;

        [SerializeField] private CharacterStat characterAbility;

        private float skillCoolTimeRegain;
        public float SkillCoolRegain => skillCoolTimeRegain;
        public CharacterStat CharacterAbility => characterAbility;
        private TeamInfo OurTeam;
        public TeamInfo Team => OurTeam;
        public Job CharacterClassPublic => CharacterClass;
        private Action OnDeadCb;
        public bool isPlayer => OurTeam.IsPlayer;

        private bool hasSkill = false;

        public float MaxHp => HPMax;

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
                    onHpUpdate?.Invoke(_currentHp/HPMax);
                }
            }
        }

        public void SetStat(CharacterStat s)
        {
            characterAbility = s;
            HPMax = s.HpMax;
            currentHp = HPMax;
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
            currentHp = HPMax * per;
        }

        protected virtual void OnDead()
        {
            OnDeadCb?.Invoke();
        }

        public bool CanUseSkill(bool getInput=false)
        {
            if (!hasSkill)
                return false;
            if (IsDead)
                return false;
            if (GameManager.GetInstance().IsAuto || !OurTeam.IsPlayer || getInput)
                return skillCoolTimeRegain >= characterAbility.SkillCoolTIme;

            return false;
        }

        public bool IsDead => currentHp <= 0;
        
        private Dictionary<State, BattleState> FiniteStateMachineDic = new Dictionary<State, BattleState>();

        private State currentState = State.Idle;
        private BattleState currentBattleState = null;

        private void Awake()
        {
            currentHp = HPMax;
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
            hasSkill = FiniteStateMachineDic.ContainsKey(State.Skill);
            skillCoolTimeRegain = 0;
            if (CanUseSkillFirst)
                skillCoolTimeRegain = characterAbility.SkillCoolTIme;
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