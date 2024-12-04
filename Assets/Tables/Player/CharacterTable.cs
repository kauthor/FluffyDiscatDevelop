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
        public int chrType;
        public int job;
        public int maxHp;
        public int atk;
        public int armor;
        public int magicArmor;
        public int hpRegen;
        public int hpAbsolve;
        public int critical;
        public int critDamage;
        public int attackCoolTime;
        public int dodge;
        public int range;
        public int moveSpeed;
        public int damIncrease;
        public int damDecrease;
        public int aeo;
        public int accuracy;
        public int startItemGroup;
        public int startTraitGroup;
        public int Price => 10;
        

        public CharacterStat GetCharacterDataAsStat()
        {
            var def = ExcelManager.GetInstance().BaseT;
            return new CharacterStat()
            {
                HpMax = maxHp,
                MoveSpeed = this.moveSpeed,
                Range = this.range,
                //SkillCoolTIme = 0,
                AttackCoolTime = this.attackCoolTime,
                Atk = atk,
                phyDef = armor,
                magDef = magicArmor,

                hpRegen = this.hpRegen,
                hpAbsolve = this.hpAbsolve,
                crit = this.critical,
                critDam = this.critDamage,
                //atkSpeed = ,
                dodge = this.dodge,
                //moveSpeedNew = 0,
                atkIncrease = this.damIncrease,
                damageDecrease = this.damDecrease,
                AOEArea = this.aeo,
                accuracy = this.accuracy
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