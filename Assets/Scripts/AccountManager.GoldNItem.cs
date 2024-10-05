using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Tables;

namespace FluffyDisket
{
    public enum ItemType
    {
        NONE=0,
        Equip,
        Consume,
        Etc
    }

    public class ItemInventoryData
    {
        private ItemData rowData;
        
        public int ItemCount { get; private set; }

        public void DeltaItemCount(int delta)
            => ItemCount += delta;
        public int ItemId => rowData?.id ?? 0;
        public ItemType IType => rowData?.type ?? ItemType.Etc;
        public bool Stackable => rowData?.stackable ?? false;

        public int UniqueId { get; private set; }
    }
    
    public partial class AccountManager : CustomSingleton<AccountManager>
    {
        private int gold;

        private Dictionary<ItemType, List<ItemInventoryData>> invenTypeDic;

        private int Gold
        {
            get => gold;
            set
            {
                gold = value;
            }
        }

        /// <summary>
        /// 언젠가 서버가 들어가면 동기화 해줄 때 필요할거다
        /// </summary>
        public void Sync()
        {
            
        }

        private void AccountSyncRequest(Action onEnd)
        {
            
        }

        private void ItemDataInit()
        {
            invenTypeDic = new Dictionary<ItemType, List<ItemInventoryData>>();
            gold = 0;
            
            //본래라면 서버로  Sync를 요청해야한다.
            AccountSyncRequest(Sync);
        }

        public void GetItem(ItemInventoryData data)
        {
            if (invenTypeDic.TryGetValue(data.IType, out var lst))
            {
                var old = lst.Find(_ => _.ItemId == data.ItemId);
                if (old !=null && old.Stackable)
                {
                    old.DeltaItemCount(old.ItemCount);
                }
                else
                {
                    lst.Add(data);
                }
            }
            else
            {
                var newList = new List<ItemInventoryData> {data};
                invenTypeDic.Add(data.IType, newList);
            }
        }

        
        /// <summary>
        /// 이 함수는 파라미터가 뭐가 될지 모르겠다...
        /// </summary>
        public void UseItem()
        {
            
        }

        public void GetGold(int g) => Gold += g;
        public void UseGold(int g) => Gold -= g;
        
        
    }
}