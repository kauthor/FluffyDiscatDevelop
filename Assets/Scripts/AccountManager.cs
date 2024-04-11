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

        [SerializeField] private PlayerSubTable[] playerTables;
        //이것은 추후 엑셀 매니저로 분기하자.

        public Dictionary<int, bool> CharacterOwned => characterOwned;

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
            characterOwned = new Dictionary<int, bool>();
            currentBattlePlayers = new List<int>();
            
            //차후 계정 정보를 본격적으로 받아오면, 그때  제대로 초기화한다.

            for(int i=0; i<3; i++)
            {
                characterOwned.Add(i,true);
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

        public void CallStageClearToAccount()
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
        }
    }
}