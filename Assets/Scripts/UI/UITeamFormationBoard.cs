using System;
using FluffyDisket;
using FluffyDisket.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UITeamFormationBoard : UIMonoBehaviour, IEndDragHandler,IDragHandler
    {
        public override UIType type => UIType.Formation;
        [SerializeField] private int[] startPosition = new []{6,8,12,16,18};
        [SerializeField] private UITeamFormationIcon[] playerSets;
        [SerializeField] private UIPlayerSlot[] playerSlots;

        [SerializeField] private Transform trDragging;
        [SerializeField] private Button btnGoGame;
        private UITeamFormationIcon CurrentDrag;

        private void Awake()
        {
            btnGoGame.onClick.RemoveAllListeners();
            btnGoGame.onClick.AddListener(GoNextGame);
        }

        public override void Init(UIViewParam param)
        {
            base.Init(param);
            
            CurrentDrag = null;
            
            var playerDat = BattleManager.GetInstance().TryGetBattleData();
            var curNode = StageManager.GetInstance().CurrentStage;

            foreach (var p in playerSets)
            {
                p.gameObject.SetActive(false);
            }

            int i = 0;
            for(int j=0;j<playerSlots.Length; j ++)
            {
                playerSlots[j].InitHandler(OnDroppedAtSlot,OnDragStart,j);
            }
            foreach (var pair in playerDat.playerFormationDic)
            {
                if(BattleManager.GetInstance().CurrentPlayerCondition.currentHpDatas[pair.Key].retired)
                    continue;
               
                int playerNum = AccountManager.GetInstance().CurrentBattleMember[pair.Key];

                if (i >= playerSets.Length)
                    break;
                
                var currentSet = playerSets[i];
                currentSet.gameObject.SetActive(true);
                currentSet.Init(playerNum, playerSlots[startPosition[i]]);
                playerSlots[startPosition[i]].SetChild(currentSet);
                i++;
            }
            
            
        }

        private void OnDragStart(UITeamFormationIcon started)
        {
            CurrentDrag = started;
            started.transform.SetParent(trDragging);
        }
        
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (CurrentDrag != null)
            {
                CurrentDrag.ResetPosition();
                CurrentDrag = null;
            }
        }

        private void OnDroppedAtSlot(UIPlayerSlot slot)
        {
            if (CurrentDrag != null)
            {
                CurrentDrag.SetParent(slot);
                CurrentDrag = null;
            }
        }
        
        private void GoNextGame()
        {
            var playerDat = BattleManager.GetInstance().TryGetBattleData();
            int i = 0;
            foreach (var tb in playerSets)
            {
                var tuple = tb.GetFinalData();
                playerDat.playerFormationDic[tuple.Item1] = tuple.Item2;
            }
            
            gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("SampleScene").completed
                += _ =>
                {
                    BattleManager.GetInstance().StartBattle();
                };
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (CurrentDrag != null)
            {
                CurrentDrag.transform.position = eventData.position;
            }
        }
    }
}