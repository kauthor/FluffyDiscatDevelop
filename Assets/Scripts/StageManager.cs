using System;
using System.Collections.Generic;

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
            
        private void MakeStage(Action onEndCb)
        {
            if (stageTree != null)
                stageTree.Clear();
            else stageTree = new List<StageNode>();
            
            
            //임시 데이터
            var monsterdat = new MonsterData();
            monsterdat.Stat = new CharacterStat()
            {
                HpMax = 100,
                AttackCoolTime = 0.8f,
                MoveSpeed = 2,
                Range = 5,
                SkillCoolTIme = 0
            };
            var monsterPool = new MonsterData[3]
            {
                monsterdat, monsterdat, monsterdat
            };
            var startStage = new StageNode(StageType.Battle, monsterPool);

            var bossData = new MonsterData();
            int bossDepth = 5;
            bossData.Stat = new CharacterStat()
            {
                HpMax = 400,
                AttackCoolTime = 0.8f,
                MoveSpeed = 2,
                Range = 5,
                SkillCoolTIme = 0
            };
            var bossPool = new MonsterData[1]
            {
                bossData
            };
            
            var bossStage=new StageNode(StageType.Boss, bossPool);
            StageNode prev=startStage;
            StageNode current = null;
            stageTree.Add(startStage);
            for (int i = 0; i < bossDepth; i++)
            {
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