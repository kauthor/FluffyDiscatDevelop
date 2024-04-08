using System;
using UnityEngine;

namespace Tables
{
    [Serializable]
    public struct BaseData
    {
        public int id;
        public int unit;
        public int data;
    }

    [Serializable]
    public struct ExpData
    {
        public int level;
        //public int heroCount;
        public int reqExp;
    }
    
    
    [CreateAssetMenu]
    public class BaseTable:ScriptableObject
    {
        [SerializeField] private BaseData[] baseDatas;
        

        public void SetBaseData(BaseData[] arr) => baseDatas = arr;

        public BaseData GetBaseDataByIndex(int i)
        {
            if (i >= baseDatas.Length)
                return new BaseData();

            return baseDatas[i];
        }
        
    }
}