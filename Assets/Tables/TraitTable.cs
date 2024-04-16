using System;
using FluffyDisket;
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
        
        public void SetTraitData(TraitData[] arr) => traitDatas = arr;
        
        public TraitData GetTraitDataById(int id)
        {
            var ret = Array.Find(traitDatas, t => t.id == id);
            return ret;
        }
    }
}