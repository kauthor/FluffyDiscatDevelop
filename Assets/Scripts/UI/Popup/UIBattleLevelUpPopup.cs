using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private Transform divideArea;
        [SerializeField] private HeroStatDivideComponent _divideComponent;
        [SerializeField] private Button btnComplete;

        private List<HeroStatDivideComponent> managed;

        public override PopupType type => PopupType.BattleLevelUp;

        private void Init(Dictionary<int,int> levelDelta)
        {
            managed = new List<HeroStatDivideComponent>();
            foreach (var pair in levelDelta)
            {
                var div = Instantiate(_divideComponent, divideArea);
                div.Init(pair.Key, pair.Value*4);
                managed.Add(div);
            }
            
            btnComplete.onClick.AddListener(OnClickComplete);
        }

        private void OnClickComplete()
        {
            bool enableClose = true;

            foreach (var d in managed)
            {
                enableClose &= d.DivideCompleted();
            }

            if (enableClose)
            {
                foreach (var d in managed)
                {
                    d.TryAddStatToPlayer();
                }
                OnCloseClick();
            }
        }
    }
}