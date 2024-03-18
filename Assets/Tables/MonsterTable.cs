using System;
using FluffyDisket;
using UnityEngine;

namespace Tables
{

    public struct MonsterData
    {
        public int id;
        public int nameId;
        public int traitId1;
        public int traitId2;
        public int traitId3;
        public int traitId4;
        public int traitId5;
        public int itemIdW; //아이디 따브류다냐
        public int itemIdE;
        public int itemDropW;
        public int itemDropE;
        public CharacterStat statData;

    }
    
    [CreateAssetMenu]
    public class MonsterTable:ScriptableObject
    {
        [SerializeField] private MonsterData[] monDatas;

        public void SetMonData(MonsterData[] arr) => monDatas = arr;
    }
}