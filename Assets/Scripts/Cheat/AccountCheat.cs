using FluffyDisket.UI;
using UnityEngine;

namespace FluffyDisket.Cheat
{
    public class AccountCheat:MonoBehaviour
    {
        [SerializeField] public int cheatGoldAmount;
        [SerializeField] public int cheatItemId;
        [SerializeField] public int cheatItemAmount;

        //[]
        public void GetGoldCheat()
        {
            AccountManager.GetInstance()?.GetGold(cheatGoldAmount);
        }

        public void GetItemCheat()
        {
            var newItem = new ItemInventoryData();
            newItem.DeltaItemCount(cheatItemAmount);
            AccountManager.GetInstance()?.GetItem(newItem);
        }
    }
}