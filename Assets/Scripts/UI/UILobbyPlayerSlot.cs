using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UILobbyPlayerSlot:MonoBehaviour
    {
        [SerializeField] private Image playerIcon;
        [SerializeField] private Button btnArea;
        [SerializeField] private Transform panelArea;
        [SerializeField] private Button btnSelect;
        [SerializeField] private Button btnCancel;
        
        [SerializeField] private Transform panelResArea;
        [SerializeField] private Button btnResSelect;
        [SerializeField] private Button btnResCancel;

        private int playerNumber;
        public int PlayerNumber => playerNumber;

        private bool isInDeck;

        private Action<UILobbyPlayerSlot> onClickSettleEvent;
        private Action<UILobbyPlayerSlot> onClickResetEvent;
        private Action<UILobbyPlayerSlot> onClickSelectEvent;

        private void Awake()
        {
            btnArea.onClick.AddListener(OnClickBtnArea);
            btnSelect.onClick.AddListener(OnClickSettle);
            btnCancel.onClick.AddListener(OnClickCancel);
            btnResSelect.onClick.AddListener(OnClickReset);
            btnResCancel.onClick.AddListener(OnClickCancel);
            onClickSettleEvent = null;
            SetPanelOff();
        }

        private void OnClickSettle()
        {
            panelArea.gameObject.SetActive(false);
            onClickSettleEvent?.Invoke(this);
            isInDeck = false;
        }

        private void OnClickReset()
        {
            panelResArea.gameObject.SetActive(false);
            onClickResetEvent?.Invoke(this);
            isInDeck = true;
        }

        public void Init(int playerNum, Action<UILobbyPlayerSlot> cbSettle, Action<UILobbyPlayerSlot> cbReset
                          ,Action<UILobbyPlayerSlot> cbSelect)
        {
            //일단 데모버전 아이콘 초기화

            isInDeck = true;
            playerNumber = playerNum;
            switch (playerNum)
            {
                case 0:
                    playerIcon.color = Color.red;
                    break;
                case 1:
                    playerIcon.color = Color.blue;
                    break;
                case 2:
                    playerIcon.color=Color.yellow;
                    break;
            }

            onClickResetEvent = cbReset;
            onClickSettleEvent = cbSettle;
            onClickSelectEvent = cbSelect;
        }

        private void OnClickBtnArea()
        {
            onClickSelectEvent?.Invoke(this);
            if(isInDeck)
               panelArea.gameObject.SetActive(true);
            else panelResArea.gameObject.SetActive(true);
        }

        private void OnClickCancel()
        {
            panelArea.gameObject.SetActive(false);
            panelResArea.gameObject.SetActive(false);
        }

        public void SetPanelOff() => OnClickCancel();
    }
}