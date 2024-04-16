using System;
using UnityEngine;

namespace Tables
{
    [Serializable]
    public struct TraitOptionData
    {
        public int battleOptionType;
        public int value1;
        public int value2;
    }
    
    [Serializable]
    public struct TraitData
    {
        public int id;
        public int traitType;
        public bool traitCondition;
        public int traitVive;
        public int type;
        public TraitOptionData[] optionDatas;
        public int rarity;
        public int sort;
        public int group;

    }
    
    public class TraitTable:ScriptableObject
    {
        [SerializeField] private TraitData[] traitDatas;
    }
}