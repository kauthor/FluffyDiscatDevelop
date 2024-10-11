using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI.Inven
{
    public class UIItemComponent:MonoBehaviour
    {
        [SerializeField] private Image imgItem;
        [SerializeField] private Text txtAmount;
        [SerializeField] private Button btnOnClicked;

        public void Init(ItemInventoryData data)
        {
            
        }

        public void Clear()
        {
            
        }
    }
}