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

    [CreateAssetMenu]
    public class ItemTable:ScriptableObject
    {
        [SerializeField] private ItemData[] itemDatas;
        
        public void SetItemData(ItemData[] arr) => itemDatas = arr;
        
        public ItemData GetItemDataById(int id)
        {
            return itemDatas.FirstOrDefault(_ => _.id == id);
        }
    }
}