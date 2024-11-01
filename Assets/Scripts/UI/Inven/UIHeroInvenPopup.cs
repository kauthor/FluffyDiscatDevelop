
using System.Linq;
using FluffyDisket;
using FluffyDisket.UI;
using FluffyDisket.UI.Inven;
using Tables;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroInvenPopup:PopupMonoBehavior
{
    public override PopupType type => PopupType.HeroInven;


    [SerializeField] private UIItemComponent[] equipSlots;
    [SerializeField] private Image imgHeroPort;

    private int heroID;
    
    public static void OpenPopup(int heroId)
    {
        var pop = PopupManager.GetInstance().GetPopup(PopupType.HeroInven);
        if (pop is UIHeroInvenPopup inven)
        {
            inven.Init(heroId);
            inven.gameObject.SetActive(true);
        }
    }

    private void Init(int heroId)
    {
        heroID = heroId;

        var equiped = AccountManager.GetInstance().CharacterEquiped[heroId];
        var invenData = AccountManager.GetInstance().HeroInventory[heroId];
        for(int i=0; i< equiped.Length; i++)
        {
            var id = equiped[i];
            if (id > 0)
            {
                var data = ExcelManager.GetInstance().ItemT.GetEquipDataById(id);
                var exist = invenData.FirstOrDefault(_ => _.ItemId == id);
                equipSlots[i].Init(exist);
            }
            else
            {
                equipSlots[i].Clear();
            }
            
        }
    }
}