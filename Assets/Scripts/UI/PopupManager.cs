using System.Collections.Generic;
using UnityEngine;

namespace FluffyDisket.UI
{
    public enum PopupType
    {
        NONE=-1,
        PlayerInfo=0,
        BattleLevelUp=1,
        Inventory=2,
        ItemDescription=3,
    }

    public class PopupManager:CustomSingleton<PopupManager>
    {
        private Stack<PopupMonoBehavior> popupStack;
        [SerializeField] private PopupMonoBehavior[] popups;
        private Dictionary<PopupType, PopupMonoBehavior> popupDic;

        protected override void Awake()
        {
            base.Awake();
            popupStack = new Stack<PopupMonoBehavior>();
            popupDic = new Dictionary<PopupType, PopupMonoBehavior>();

            for (int i = 0; i < popups.Length; i++)
            {
                popupDic.Add(popups[i].type, popups[i]);
            }
        }


        public PopupMonoBehavior GetPopup(PopupType type) 
        {
            if (popupDic.TryGetValue(type, out var pop))
            {
                var newPop = Instantiate(pop);
                popupStack.Push(newPop);
                pop.gameObject.SetActive(false);
                return newPop;
            }

            return null;
        }

        public void ClosePopup(PopupType type)
        {
            if (popupStack.Peek().type == type)
            {
                var pop = popupStack.Pop();
                Destroy(pop.gameObject);
            }
        }
    }
}