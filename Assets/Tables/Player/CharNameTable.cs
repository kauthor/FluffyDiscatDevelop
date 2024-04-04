using System;
using UnityEngine;

namespace Tables.Player
{

    [Serializable]
    public struct CharNameData
    {
        public int nameGroup;
        public int nameId;
    }
    
    [CreateAssetMenu]
    public class CharNameTable:ScriptableObject
    {
        [SerializeField] private CharNameData[] charDatas;

        public void SetCharData(CharNameData[] arr) => charDatas = arr;
        public CharNameData GetCharNameData(int index)
        {
            if (index >= charDatas.Length)
                return new CharNameData();

            return charDatas[index];
        }
    }
}