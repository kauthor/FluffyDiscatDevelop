using System;
using UnityEngine;

namespace Tables
{
    [Serializable]
    public struct MonsterGroupData
    {
        public int groupId;
        public MonsterParty[] parties;
        public int reward;
    }

    [Serializable]
    public struct MonsterParty
    {
        public int monsterId;
        public int monsterCount;
    }
    
    [CreateAssetMenu]
    public class MonsterGroupTable:ScriptableObject
    {
        [SerializeField] private MonsterGroupData[] _monsterGroupData;

        public void SetMonGroupData(MonsterGroupData[] arr) => _monsterGroupData = arr;
    }
}