using System;
using UnityEngine;

namespace Tables.Player
{
    [Serializable]
    public struct CharacterData
    {
        public int id;
        public int gameGroup;
        public int gender;
        public int tribe;
        public int maxHp;
        public int atk;
        public int armor;
        public int magicArmor;
    }
    
    [CreateAssetMenu]
    public class CharacterTable:ScriptableObject
    {
        [SerializeField] private CharacterData[] charDatas;
        
        public void SetBaseData(CharacterData[] arr) => charDatas = arr;
    }
}