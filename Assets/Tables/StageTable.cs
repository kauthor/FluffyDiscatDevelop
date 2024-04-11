using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tables
{
    [Serializable]
    public struct StageData
    {
        public int mapType;
        public int stageType;
        public int minStage;
        public int maxStage;
        public int remainder;
        public int firstStageType;
        public int firstStageMonsterType;
        public int[] normalMonsterGroup;
        public int eventMapGroup;
        public int bossMapGroup;
        public int[] normalPer;
        public int eventPer;

        public int GetMonsterGroupByRatio(bool firstStage=false)
        {
            if (firstStageType >= 2 && firstStage)
                return firstStageMonsterType;

            int rand = Random.Range(0, 10000);
            int useGroup = 0;

            for (int i=0; i< normalPer.Length; i++)
            {
                rand -= normalPer[i];
                if (rand <= 0)
                {
                    useGroup = i;
                    break;
                }
            }

            if (useGroup >= normalMonsterGroup.Length)
                return 1;

            return normalMonsterGroup[useGroup];
        }
    }
    
    [CreateAssetMenu]
    public class StageTable : ScriptableObject
    {
        [SerializeField] private StageData[] stageDatas;

        public void SetStageData(StageData[] arr) => stageDatas = arr;

        public StageData GetStageData(int i)
        {
            if (i >= stageDatas.Length)
                return new StageData();
            return stageDatas[i];
        }
    }
}