using System;
using FluffyDisket;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tables.Player
{
    [Serializable]
    public class JobData
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
    public class JobTable:ScriptableObject
    {
        [SerializeField] private JobData[] jobDatas;
        public int characterAmounts => jobDatas.Length;
        
        public void SetBaseData(JobData[] arr) => jobDatas = arr;

        public JobData GetJobData(int index)
        {
            if (index >= jobDatas.Length)
                return null;

            return jobDatas[index];
        }
    }
}