using System;
using UnityEngine;

namespace Tables
{

    [Serializable]
    public struct MonsterLevelData
    {
        public int stageLv;
        public int monsterBaseStat;
        public int nextStageLvCount;
    }
    
    [CreateAssetMenu]
    public class MonsterLevelTable:ScriptableObject
    {
        [SerializeField] private MonsterLevelData[] monsterLevelDatas;

        public void SetMonLevelData(MonsterLevelData[] arr) => monsterLevelDatas = arr;
    }
}