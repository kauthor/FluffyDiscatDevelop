using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FluffyDisket.UI.Inven
{
    public class UIItemComponent:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image imgItem;
        [SerializeField] private Text txtAmount;
        [SerializeField] private Button btnOnClicked;

        private ItemInventoryData currentData;
        private UIItemDescPopup _popup;
        private UIInvenPopup parentPopup;

        private Action<UIItemComponent> OnItemShow;
        
        public void Init(ItemInventoryData data, Action<UIItemComponent> onTouch=null)
        {
            txtAmount.text = data.ItemCount.ToString();
            currentData = data;
            OnItemShow = onTouch;
        }

        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnItemShow?.Invoke(this);
            _popup = UIItemDescPopup.OpenPopup(currentData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ClosePopup();
        }

        public void ClosePopup()
        {
            if(_popup!=null)
                _popup.Close();

            _popup = null;
        }
    }
}