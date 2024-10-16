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
        
        public void Init(ItemInventoryData data)
        {
            txtAmount.text = data.ItemCount.ToString();
            currentData = data;
        }

        public void Clear()
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _popup = UIItemDescPopup.OpenPopup(currentData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_popup!=null)
                _popup.Close();
        }
    }
}