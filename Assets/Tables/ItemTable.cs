using System;
using System.Linq;
using FluffyDisket;
using UnityEngine;

namespace Tables
{
    public enum EquipType : uint
    {
        OneHandSword = 1<<0,
        TwoHandSword = 1<<1,
        ShortSword = 1<<2,
        Bow = 1<<3,
        Stick = 1<<4,
        OneHandHammer = 1<<5,
        TwoHandHammer = 1<<6,
        Spear = 1<<7,
        AkimboSword = 1<<8,
        Helmet = 1<<9,
        Plate = 1<<10,
        Accessory = 1<<11,
    }
    [Serializable]
    public class ItemData
    {
        public int id;
        public ItemType type;
        public bool stackable => type != ItemType.Equip;
        public int weight;
        public int raiting;
        public int trait_id1;
        public int trait_id2;
        public int trait_id3;
        public int weapon_bit_type;
        public EquipType EquipType => (EquipType) weapon_bit_type;
        public int atk_type;
        public int atk_min;
        public int atk_max;
        public int max_hp;
        public int armor;
        public int magic_armor;
        public int atk_speed;
        public int atk_range;
        public int battle_option_type1;
        public int b1_value1;
        public int b1_value2;
        public int battle_option_type2;
        public int b2_value1;
        public int b2_value2;
        public int battle_option_type3;
        public int b3_value1;
        public int b3_value2;
        public int battle_option_type4;
        public int b4_value1;
        public int b4_value2;

        public string item_icon;
        public int item_point;
    }

    [Serializable]
    public class EquipOption
    {
        public StatType type;
        public int value;
    }

    public class EquipItemData
    {
        public int id;
        public EquipOption[] options;
        public ItemType type;
    }

    [CreateAssetMenu]
    public class ItemTable:ScriptableObject
    {
        [SerializeField] private ItemData[] itemDatas;
        [SerializeField] private EquipItemData[] equipDatas;
        
        public void SetItemData(ItemData[] arr) => itemDatas = arr;
        
        public ItemData GetItemDataById(int id)
        {
            return itemDatas.FirstOrDefault(_ => _.id == id);
        }
        
        public EquipItemData GetEquipDataById(int id)
        {
            return equipDatas.FirstOrDefault(_ => _.id == id);
        }
    }
}