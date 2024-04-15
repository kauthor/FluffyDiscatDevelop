using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using FluffyDisket.UI;
using Tables;
using Random = UnityEngine.Random;

namespace FluffyDisket
{
    public enum StageType
    {
        Battle=0,
        Boss
    }
    
    [Serializable]
    public class StageNode
    {
        private StageNode LeftNode;
        public StageNode Left => LeftNode;
        
        private StageNode RightNode;
        public StageNode Right => RightNode;
        
        private StageNode TopNode;
        public StageNode Top => TopNode;
        
        private StageNode BottomNode;
        public StageNode Bottom => BottomNode;
        
        private StageType type;
        public MonsterData[] monsterDatas;
        public bool isStart;

        public StageNode(StageType type, MonsterData[] monDatas, bool StartPoint=false)
        {
            this.type = type;
            monsterDatas = monDatas;
            isStart = StartPoint;
        }

        public StageNode TryGetNextNode()
        {
            return RightNode;
        }
        
        public void TryAttachPrevNode(StageNode prevNode)
        {
            //임시
            prevNode.RecognizeNextNode(this);
            LeftNode = prevNode;
        }

        private void RecognizeNextNode(StageNode nextNode)
        {
            RightNode = nextNode;
        }

        public StageType StageType => type;

        private bool cleared = false;
        public bool Cleared => cleared;

        public void CallCleared() => cleared = true;
    }
    
    public class StageManager:CustomSingleton<StageManager>
    {
        private List<StageNode> stageTree;
        public List<StageNode> StageTree => stageTree;
        private StageNode currentStage;
        public StageNode CurrentStage => currentStage;
        private bool initialize = false;

        public bool IsFirstStage()
        {
            return stageTree != null && stageTree[0] == currentStage;
        }

        public void CallStageCleared()
        {
            AccountManager.GetInstance().CallStageClearToAccount(UIBattleLevelUpPopup.OpenPopup);
            currentStage.CallCleared();
        }
        
        public void TryEnterNode(StageNode entered, Action onEndCb)
        {
            if(initialize)
            {
                currentStage = entered;
                onEndCb?.Invoke();
                return;
            }
            initialize = true;
            MakeStage(onEndCb);
        }

        public void TryEnterFirstStage(Action onEnd)
        {
            if(initialize)
            {
                currentStage = stageTree[0];
                onEnd?.Invoke();
                return;
            }
            initialize = true;
            MakeStage(onEnd);
        }
            
        private void MakeStage(Action onEndCb, int stageNumber=0)
        {
            if (stageTree != null)
                stageTree.Clear();
            else stageTree = new List<StageNode>();
            
            
            //임시 데이터
            var excel = ExcelManager.GetInstance();
            var stageT = excel.StageT.GetStageData(stageNumber);
            var monsterGT = excel.MonsterGroupT;
            var monsterT = excel.MonsterT;
            
            
            var startMonsters = 
                 monsterGT.GetMonsterGroupData(stageT.GetMonsterGroupByRatio(true));
            
            var monList = new List<MonsterData>();

            foreach (var par in startMonsters.parties)
            {
                for (int i = 0; i < par.monsterCount; i++)
                {
                    var monData = monsterT.GetMonsterData(par.monsterId);
                    monList.Add(monData);
                }
            }
            var startStage = new StageNode(StageType.Battle, monList.ToArray());
            var bossDepth = Random.Range(stageT.minStage, stageT.maxStage);
            bossDepth /= 5;
            bossDepth *= 5;
            bossDepth--;
            var bossList = new List<MonsterData>();

            var bossMonster = monsterGT.GetMonsterGroupData(stageT.bossMapGroup);
            foreach (var par in bossMonster.parties)
            {
                for (int i = 0; i < par.monsterCount; i++)
                {
                    var monData = monsterT.GetMonsterData(par.monsterId);
                    bossList.Add(monData);
                }
            }
            
            var bossStage=new StageNode(StageType.Boss, bossList.ToArray());
            StageNode prev=startStage;
            StageNode current = null;
            stageTree.Add(startStage);
            for (int i = 0; i < bossDepth; i++)
            {
                var currentMonsters = monsterGT.GetMonsterGroupData(stageT.GetMonsterGroupByRatio());
                var CurMonList = new List<MonsterData>();

                foreach (var par in currentMonsters.parties)
                {
                    for (int j = 0; j < par.monsterCount; j++)
                    {
                        var monData = monsterT.GetMonsterData(par.monsterId);
                        CurMonList.Add(monData);
                    }
                }

                var monsterPool = CurMonList.ToArray();
                if (i == 0)
                {
                    prev = startStage;
                    current = new StageNode(StageType.Battle, monsterPool);
                    
                }
                else
                {
                    prev = current;
                    current = i<bossDepth-1 ? new StageNode(StageType.Battle, monsterPool) : bossStage;
                }
                current.TryAttachPrevNode(prev);
                stageTree.Add(current);
            }

            currentStage = startStage;
            
            onEndCb?.Invoke();
        }
    }
}