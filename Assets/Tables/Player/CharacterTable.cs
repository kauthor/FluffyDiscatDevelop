using System;
using FluffyDisket;
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

        public CharacterStat GetCharacterDataAsStat()
        {
            var def = ExcelManager.GetInstance().BaseT;
            return new CharacterStat()
            {
                HpMax = maxHp,
                MoveSpeed = def.GetBaseDataByIndex(15).data,
                Range = def.GetBaseDataByIndex(16).data,
                //SkillCoolTIme = 0,
                AttackCoolTime = def.GetBaseDataByIndex(13).data*100,
                Atk = atk,
                phyDef = armor,
                magDef = magicArmor,

                hpRegen = def.GetBaseDataByIndex(9).data,
                hpAbsolve = def.GetBaseDataByIndex(10).data,
                crit = def.GetBaseDataByIndex(11).data,
                critDam = def.GetBaseDataByIndex(12).data,
                //atkSpeed = ,
                dodge = def.GetBaseDataByIndex(14).data,
                //moveSpeedNew = 0,
                atkIncrease = def.GetBaseDataByIndex(17).data,
                damageDecrease = def.GetBaseDataByIndex(18).data,
                AOEArea = def.GetBaseDataByIndex(19).data,
                accuracy = def.GetBaseDataByIndex(20).data
            };
        }
    }
    
    [CreateAssetMenu]
    public class CharacterTable:ScriptableObject
    {
        [SerializeField] private CharacterData[] charDatas;
        public int characterAmounts => charDatas.Length;
        
        public void SetBaseData(CharacterData[] arr) => charDatas = arr;

        public CharacterData GetCharData(int index)
        {
            if (index >= charDatas.Length)
                return new CharacterData();

            return charDatas[index];
        }
    }
}