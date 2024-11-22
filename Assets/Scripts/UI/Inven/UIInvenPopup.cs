using System.Collections.Generic;
using UnityEngine;

namespace FluffyDisket.UI.Inven
{
    public class UIInvenPopup:PopupMonoBehavior
    {
        public static void OpenPopup()
        {
            var pop = PopupManager.GetInstance().GetPopup(PopupType.Inventory);
            if (pop is UIInvenPopup inven)
            {
                inven.Init();
                inven.gameObject.SetActive(true);
            }
        }
        public override PopupType type => PopupType.Inventory;

        private Dictionary<ItemType, List<ItemInventoryData>> accountData;
        private List<ItemInventoryData> allItems;

        private UIItemComponent[] _itemComponents;
        [SerializeField] private Transform itemsParent;

        private UIItemComponent touchedComp = null;

        protected override void Awake()
        {
            base.Awake();
            
        }

        private void Init()
        {
            _itemComponents = itemsParent.GetComponentsInChildren<UIItemComponent>();
            SyncItems();
            AccountManager.GetInstance().OnAccountSync -= SyncItems;
            AccountManager.GetInstance().OnAccountSync += SyncItems;
            
            
        }

        private void SyncItems()
        {
            var invenList = AccountManager.GetInstance().GetInventory;

            if (invenList != null)
            {
                accountData = invenList;
                allItems = new List<ItemInventoryData>();
                foreach (var pair in accountData)
                {
                    foreach (var e in pair.Value)
                    {
                        allItems.Add(e);
                    }
                }
            }
            
            // 이 이하로 Ui 컴포넌트들 초기화
            if(_itemComponents!=null && _itemComponents.Length>0)
            {
                //소비 아이템은 아마 등록하는 방식인데.. 일단 있는거대로 표기한다
                for (int i = 0; i < 6; i++)
                {
                    _itemComponents[i].ClearThenShow();
                }
                
                for (int i = 6; i < _itemComponents.Length; i++)
                {
                    if(i >= allItems.Count)
                        _itemComponents[i].Clear();
                    else
                    {
                        _itemComponents[i].Init(allItems[i], OnTouchItemComponent);
                    }
                }
            }
        }

        private void OnTouchItemComponent(UIItemComponent comp)
        {
            touchedComp?.ClosePopup();
            touchedComp = comp;
        }
    }
}