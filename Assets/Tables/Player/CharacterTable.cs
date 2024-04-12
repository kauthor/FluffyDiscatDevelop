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
                MoveSpeed = def.GetBaseDataByIndex(17).data,
                Range = def.GetBaseDataByIndex(16).data,
                //SkillCoolTIme = 0,
                AttackCoolTime = def.GetBaseDataByIndex(14).data,
                Atk = atk,
                phyDef = armor,
                magDef = magicArmor,

                hpRegen = def.GetBaseDataByIndex(10).data,
                hpAbsolve = def.GetBaseDataByIndex(11).data,
                crit = def.GetBaseDataByIndex(12).data,
                critDam = def.GetBaseDataByIndex(13).data,
                //atkSpeed = ,
                dodge = def.GetBaseDataByIndex(15).data,
                //moveSpeedNew = 0,
                atkIncrease = def.GetBaseDataByIndex(18).data,
                damageDecrease = def.GetBaseDataByIndex(19).data,
                AOEArea = def.GetBaseDataByIndex(20).data,
                accuracy = def.GetBaseDataByIndex(21).data
            };
        }
    }
    
    [CreateAssetMenu]
    public class CharacterTable:ScriptableObject
    {
        [SerializeField] private CharacterData[] charDatas;
        
        public void SetBaseData(CharacterData[] arr) => charDatas = arr;

        public CharacterData GetCharData(int index)
        {
            if (index >= charDatas.Length)
                return new CharacterData();

            return charDatas[index];
        }
    }
}