using System;
using System.Linq;
using FluffyDisket;
using UnityEngine;

namespace Tables
{
    [Serializable]
    public class ItemData
    {
        public int id;
        public ItemType type;
        public bool stackable;
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