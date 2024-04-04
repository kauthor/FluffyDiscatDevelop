using System;
using UnityEngine;

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