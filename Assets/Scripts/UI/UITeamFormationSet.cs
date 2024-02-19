using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UITeamFormationSet: UIMonoBehaviour
    {
        [SerializeField] private TeamPositionButton[] teamButtons;
        [SerializeField] private TeamPositionButton[] enemyButtons;

        [SerializeField] private Button btnNextMap;
//임시 프리팹들
        [SerializeField] private Image playerIcon;
        [SerializeField] private Image enemyIcon;

        public override UIType type => UIType.Formation;
        private TeamPositionButton currentSelected;
        private void Awake()
        {
            foreach (var t in teamButtons)
            {
                t.HandleListener(OnSelected);
            }
            btnNextMap.onClick.RemoveAllListeners();
            btnNextMap.onClick.AddListener(GoNextGame);
        }

        public override void Init(UIViewParam param)
        {
            base.Init(param);
            var playerDat = BattleManager.GetInstance().TryGetBattleData();
            var curNode = StageManager.GetInstance().CurrentStage;
            currentSelected = null;

            foreach (var pair in playerDat.playerFormationDic)
            {
                var img = Instantiate(playerIcon);
                var color = Color.red;
                switch (pair.Key)
                {
                    case 0:
                        color=Color.red;
                        break;
                    case 1:
                        color=Color.blue;
                        break;
                    case 2:
                        color=Color.yellow;
                        break;
                    default:
                        color=Color.white;
                        break;
                }

                img.color = color;
                teamButtons[pair.Value].SetImage(img, pair.Key); 
            }

            int i = 0;
            foreach (var mon in curNode.monsterDatas)
            {
                var img = Instantiate(enemyIcon);
                enemyButtons[i].SetImage(img, i);
                i++;
            }
        }

        private void OnSelected(TeamPositionButton btn)
        {
            if (currentSelected != null)
            {
                if (currentSelected != btn)
                {
                    btn.SwapImage(currentSelected);
                }

                currentSelected = null;
            }
            else
            {
                currentSelected = btn;
            }
        }

        private void GoNextGame()
        {
            var playerDat = BattleManager.GetInstance().TryGetBattleData();
            int i = 0;
            foreach (var tb in teamButtons)
            {
                var n = tb.GetData();
                if(n>=0)
                    playerDat.playerFormationDic[n] = i;
                i++;
            }
            foreach (var t in teamButtons)
            {
                t.Dispose();
            }

            foreach (var e in enemyButtons)
            {
                e.Dispose();
            }
            gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("SampleScene").completed
                += _ =>
                {
                    BattleManager.GetInstance().StartBattle();
                };
        }
    }
}