using System;
using System.Collections.Generic;
using Tables;
using Tables.Player;
using UnityEngine;

namespace FluffyDisket
{
    public class AccountManager:CustomSingleton<AccountManager>
    {
        private Dictionary<int, bool> characterOwned;
        private Dictionary<int, int> characterLevels;
        private Dictionary<int, int> characterCurrentExp;

        //[SerializeField] private PlayerSubTable[] playerTables;
        //이것은 추후 엑셀 매니저로 분기하자.

        public Dictionary<int, bool> CharacterOwned => characterOwned;
        public Dictionary<int, int> CharacterLevels => characterLevels;
        public Dictionary<int, int> CharacterCurrentExp => characterCurrentExp;

        private List<int> currentBattlePlayers;

        public List<int> CurrentBattleMember => currentBattlePlayers;

        private int currentStageClearedStacked = 0;

        private int currentMonsterLevel = 1;

        public int CurrentMonsterLevel
        {
            get
            {
                if (currentMonsterLevel<=0)
                {
                    currentMonsterLevel = 1;
                }

                return currentMonsterLevel;
            }
        }

        private MonsterLevelData? currentMonsterLevelData;

        public MonsterLevelData CurrentMonsterLevelData
        {
            get
            {
                if (currentMonsterLevelData == null)
                {
                    currentMonsterLevelData =
                        ExcelManager.GetInstance().MonsterLevelT.GetMonsterLevelData(CurrentMonsterLevel - 1);
                }

                return currentMonsterLevelData.Value;
            }
        }

        private bool monsterLevelInit = false;
        
        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Start()
        {
            characterOwned = new Dictionary<int, bool>();
            characterLevels = new Dictionary<int, int>();
            characterCurrentExp = new Dictionary<int, int>();
            currentBattlePlayers = new List<int>();
            var chars = ExcelManager.GetInstance().CharT.characterAmounts;
            
            //차후 계정 정보를 본격적으로 받아오면, 그때  제대로 초기화한다.

            for(int i=0; i<chars; i++)
            {
                characterOwned.Add(i,true);
                characterLevels.Add(i,1);
                characterCurrentExp.Add(i,0);
            }
        }

        public void SetCurrentBattlePlayer(List<int> playerList)
        {
            if (currentBattlePlayers == null)
                currentBattlePlayers = new List<int>();
            
            currentBattlePlayers.Clear();
            foreach (var pl in playerList)
            {
                currentBattlePlayers.Add(pl);
            }
        }


        public void AddOrLoseCharacter(Job type, bool get = true)
        {
            if (characterOwned.ContainsKey((int) type))
                characterOwned[(int) type] = get;
            else characterOwned.Add((int)type, get);
        }

        public void CallStageClearToAccount(Action<Dictionary<int,int>> onLevelDeltaCB=null)
        {
            if (currentMonsterLevel <= 0)
                currentMonsterLevel = 1;

            //추후 싱글톤 및 매니저 초기화 구조를 정비하면서 위치를 변경하자.
            
            if (!monsterLevelInit || currentMonsterLevelData==null)
            {
                monsterLevelInit = true;
                currentMonsterLevelData = 
                    ExcelManager.GetInstance().MonsterLevelT.GetMonsterLevelData(currentMonsterLevel-1);
            }
            currentStageClearedStacked++;
            if (currentStageClearedStacked >= currentMonsterLevelData.Value.nextStageLvCount)
            {
                currentStageClearedStacked = 0;
                currentMonsterLevel++;
                currentMonsterLevelData = 
                    ExcelManager.GetInstance().MonsterLevelT.GetMonsterLevelData(currentMonsterLevel-1);
            }
            
            TryAddExpToCurrentBattlePlayers(onLevelDeltaCB);
        }

        private void TryAddExpToCurrentBattlePlayers(Action<Dictionary<int,int>> onLevelDeltaCB=null)
        {
            if (currentBattlePlayers == null || currentBattlePlayers.Count <= 0)
                return;

            var levelDelta = new Dictionary<int, int>();
            
            foreach (var playerId in currentBattlePlayers)
            {
                if (!characterOwned.ContainsKey(playerId) || !characterOwned[playerId])
                {
                    continue;
                }

                var currentLevel = characterLevels[playerId];
                var curLevDiff2Mon = currentLevel - currentMonsterLevel;
                int addedExp = 0;
                if (curLevDiff2Mon >= 2 && curLevDiff2Mon <=4)
                {
                    addedExp = 1;
                }
                else if (curLevDiff2Mon >= -1 && curLevDiff2Mon <= 1)
                {
                    addedExp = 2;
                }
                else if (curLevDiff2Mon >= -4 && curLevDiff2Mon <= -2)
                {
                    addedExp = 3;
                }
                else if (curLevDiff2Mon <= -5)
                    addedExp = 4;

                var nextExp = characterCurrentExp[playerId] + addedExp;
                //var expD = ExcelManager.GetInstance().ExpT.GetExpData(currentLevel - 1);
                var destiLevel = currentLevel;

                if (addedExp > 0)
                {
                    for (; nextExp >= ExcelManager.GetInstance().ExpT.GetExpData(destiLevel - 1).reqExp; destiLevel++)
                    {
                        nextExp -= ExcelManager.GetInstance().ExpT.GetExpData(destiLevel - 1).reqExp;
                    }
                }


                if (destiLevel != currentLevel)
                {
                    int currentLevelDelta = destiLevel - currentLevel;
                    levelDelta.Add(playerId, currentLevelDelta);
                    characterLevels[playerId] = destiLevel;
                }

                if (addedExp != 0)
                    characterCurrentExp[playerId] = nextExp;
            }
            
            onLevelDeltaCB?.Invoke(levelDelta);
        }
    }
}