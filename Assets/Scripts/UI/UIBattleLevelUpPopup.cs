using System.Collections.Generic;

namespace FluffyDisket.UI
{
    public class UIBattleLevelUpPopup:PopupMonoBehavior
    {
        public static void OpenPopup(Dictionary<int,int> levelDelta=null)
        {
            if (levelDelta == null || levelDelta.Count <= 0)
                return;
            var pop = PopupManager.GetInstance().GetPopup(PopupType.BattleLevelUp);
            if (pop == null)
                return;
            if (pop is UIBattleLevelUpPopup levelPop)
            {
                levelPop.Init(levelDelta);
                levelPop.gameObject.SetActive(true);
            }
        }


        private void Init(Dictionary<int,int> levelDelta)
        {
            
        }
    }
}