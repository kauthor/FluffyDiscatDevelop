using System;
using System.Collections;
using System.Collections.Generic;
using FluffyDisket.UI;
using Tables;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FluffyDisket
{
    [Serializable]
    public struct BattlePosition
    {
        public Vector3[] position;
    }

    public class PlayerBattleCurrentData
    {
        public PlayerCurrent[] currentHpDatas;
        public Dictionary<int, int> playerFormationDic;

        public int deadCount = 0;
        
        public PlayerBattleCurrentData(int length)
        {
            var newPlayer = new PlayerCurrent();
            playerFormationDic = new Dictionary<int, int>();
            newPlayer.retired = false;
            newPlayer.remainHpPer = 1;
            //프로토에서는 3인팀 및 중간에 수정불가로 정하자.
            currentHpDatas = new PlayerCurrent[length];
            for (int i = 0; i < length; i++)
            {
                currentHpDatas[i] = newPlayer;
                playerFormationDic.Add(i,i);
            }

            deadCount = 0;
        }
    }

    public struct PlayerCurrent
    {
        public float remainHpPer;
        public bool retired;
    }
    
    public class BattleManager : CustomSingleton<BattleManager>
    {
        private TeamInfo PlayerTeam=null;
        private TeamInfo EnemyTeam=null;

        private PlayerBattleCurrentData currentPlayerCondition;
        public PlayerBattleCurrentData CurrentPlayerCondition => currentPlayerCondition;

        [SerializeField] private BattleUnit[] playerTestPrefab;
        [SerializeField] private BattleUnit[] enemyTestPrefab;

        [SerializeField] private BattlePosition playerPosition;
        [SerializeField] private BattlePosition enemyPosition;

        public static event Action<bool> OnBattleEnd;

        public UIBattleVIew currentView;

        public bool InBattle
        {
            get;
            private set;
        } = false;

        protected void Start()
        {
            
        }

        public void TryStartBattle()
        {
            StageManager.GetInstance().TryEnterFirstStage(() =>
            {
                StartCoroutine(TestImpl());
            });
        }

        public PlayerBattleCurrentData TryGetBattleData()
        {
            if (currentPlayerCondition == null)
                currentPlayerCondition = new PlayerBattleCurrentData(PlayerTeam.members.Length);

            return currentPlayerCondition;
        }

        private IEnumerator TestImpl()
        {
            var playerList = new List<BattleUnit>();
            foreach (var mem in AccountManager.GetInstance().CurrentBattleMember)
            {
                //이것은 문제가 있다. 추후 리소스 관리용 Addressable 등을 사용하게 되면 픽스해야한다.
                if (mem<playerTestPrefab.Length)
                {
                    var pl = Instantiate(playerTestPrefab[mem]);
                    playerList.Add(pl);
                    pl.gameObject.SetActive(false);
                    var baseStat = ExcelManager.GetInstance().CharT.GetCharData(0).GetCharacterDataAsStat();
                    var levelData = new LevelAdditionalStat(0.0f, 0, 0, 0, 1);
                    pl.SetStat(baseStat, levelData);
                }
                
            }

            var enemyList = new List<BattleUnit>();
            var stageData = StageManager.GetInstance().CurrentStage;
            if (stageData != null && stageData.monsterDatas != null)
            {
                foreach (var m in stageData.monsterDatas)
                {
                    var mon = Instantiate(enemyTestPrefab[0]);
                    enemyList.Add(mon);
                    enemyTestPrefab[0].gameObject.SetActive(false);
                    mon.gameObject.SetActive(false);
                    var levData = new LevelAdditionalStat(m.id, m.statData.levelHp, m.statData.levelAtk,
                        m.statData.levelpd, m.statData.levelmd);
                    mon.SetStat(m.statData, levData);
                }
            }
            else
                foreach (var en in enemyTestPrefab)
                {
                    enemyList.Add(Instantiate(en));
                    en.gameObject.SetActive(false);
                }

            // yield return new WaitForSeconds(3.0f);
            //GameManager.GetInstance().SetAuto(true);
            
            Initialize(playerList.ToArray(), enemyList.ToArray());
            yield return null;
        }

        private Coroutine startCor;
        
        
        public void Initialize(BattleUnit[] players, BattleUnit[] enemies, Action onEnd=null)
        {
            PlayerTeam = new TeamInfo(players, true, currentPlayerCondition?.deadCount ?? 0);
            EnemyTeam = new TeamInfo(enemies, false);
            OnBattleEnd = null;
            onEnd?.Invoke();

            

            startCor = StartCoroutine(BattleStartProc());
        }

        private IEnumerator BattleStartProc()
        {
            float time = 2;
            while (time >=0)
            {
                time -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            
            var plp = playerPosition.position;
            var enp = enemyPosition.position;
            
            if (currentPlayerCondition == null)
            {
                currentPlayerCondition = new PlayerBattleCurrentData(PlayerTeam.members.Length);
            }
            
            for (int i=0; i< currentPlayerCondition.playerFormationDic.Count; i++)
            {
                var pos = plp[currentPlayerCondition.playerFormationDic[i]];
                

                if(currentPlayerCondition.currentHpDatas[i].retired)
                    continue;
                PlayerTeam.members[i].transform.position = pos;
                PlayerTeam.members[i].gameObject.SetActive(true);
            }

            int j = 0;
            foreach (var mem in PlayerTeam.members)
            {
                mem.SetHpPercent( currentPlayerCondition.currentHpDatas[j].retired? 0:
                    currentPlayerCondition.currentHpDatas[j].remainHpPer);
                j++;
            }
            
            for (int i=0; i< enp.Length; i++)
            {
                if (i >= EnemyTeam.members.Length)
                    break;

                EnemyTeam.members[i].transform.position = enp[i];
                EnemyTeam.members[i].gameObject.SetActive(true);
            }
            
            InBattle = true;
            PlayerTeam.StartBattle();
            EnemyTeam.StartBattle();
            var view = UIManager.GetInstance().ChangeView(UIType.Battle);
            currentView = view as UIBattleVIew;
            view.Init(new BattleViewParam()
            {
                players = PlayerTeam.members,
                enemies = EnemyTeam.members
            });
            
            if(view is UIBattleVIew b)
                b.StartGameAni();
            startCor = null;
        }

        public TeamInfo GetEnemy(bool player)
        {
            return player ? EnemyTeam : PlayerTeam;
        }

        public void EndBattle()
        {
            InBattle = false;
            OnBattleEnd?.Invoke(EnemyTeam.IsDefeated);
            int i = 0;
            if(EnemyTeam.IsDefeated)
                StageManager.GetInstance().CallStageCleared();
            foreach (var mem in PlayerTeam.members)
            {
                currentPlayerCondition.currentHpDatas[i].retired = mem.IsDead;
                AccountManager.GetInstance().AddOrLoseCharacter(mem.CharacterClassPublic, !mem.IsDead);
                currentPlayerCondition.deadCount += mem.IsDead ? 1 : 0;
                currentPlayerCondition.currentHpDatas[i].remainHpPer = mem.IsDead ? 0: mem.currentHp / mem.MaxHp;
                i++;
            }
            
           
            /*if (EnemyTeam.IsDefeated)
            {
                var stageM = StageManager.GetInstance();
                if (stageM.CurrentStage != null && stageM.CurrentStage.TryGetNextNode() != null)
                {
                    stageM.TryEnterNode(stageM.CurrentStage.TryGetNextNode(), () =>
                    {
                        UIManager.GetInstance().ChangeView(UIType.Formation).Init(new UIViewParam());
                    });
                }
            }*/
            
        }

        public void EndBattleScene()
        {
            currentPlayerCondition = null;

            EnemyTeam = null;
            PlayerTeam = null;
        }

        public void StartBattle()
        {
            StartCoroutine(TestImpl());
        }
    }
    
}