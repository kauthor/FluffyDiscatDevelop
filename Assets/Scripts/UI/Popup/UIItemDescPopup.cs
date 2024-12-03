using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI.Inven
{
    public class UIItemDescPopup:PopupMonoBehavior
    {
        public static UIItemDescPopup OpenPopup(ItemInventoryData data)
        {
            var pop = PopupManager.GetInstance().GetPopup(PopupType.ItemDescription);
            if (pop is UIItemDescPopup desc)
            {
                desc.Init(data);
                desc.gameObject.SetActive(true);
                return desc;
            }

            return null;
        }
        public override PopupType type => PopupType.ItemDescription;

        [SerializeField] private Image imgItemIcon;
        [SerializeField] private Text txtItemName;
        [SerializeField] private Text txtItemDesc;

        private ItemInventoryData currentData = null;

        private void Init(ItemInventoryData data)
        {
            currentData = data;
            txtItemName.text = data.ItemId.ToString();
            txtItemDesc.text = data.ItemId.ToString();
        }
    }
}