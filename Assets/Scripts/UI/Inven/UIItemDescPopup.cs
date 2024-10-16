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

        private void Init(ItemInventoryData data)
        {
            
        }
    }
}