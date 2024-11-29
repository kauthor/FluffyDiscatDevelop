using System;
using System.Net.Mime;
using Tables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UILobbyPlayerSlot:MonoBehaviour
    {
        [SerializeField] private Image playerIcon;
        [SerializeField] private Button btnArea;
        [SerializeField] private Transform panelArea;
        [SerializeField] private Transform imgDisposed;
        [SerializeField] private Transform imgSelected;
        [SerializeField] private Button btnSelect;
        [SerializeField] private Button btnCancel;
        
        [SerializeField] private Transform panelResArea;
        [SerializeField] private Button btnResSelect;
        [SerializeField] private Button btnResCancel;


        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private TextMeshProUGUI txtHp;
        [SerializeField] private TextMeshProUGUI txtAtk;
        [SerializeField] private TextMeshProUGUI txtPdef;
        [SerializeField] private TextMeshProUGUI txtMdef;
        
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
            imgDisposed.gameObject.SetActive(false);
            imgSelected.gameObject.SetActive(true);
            isInDeck = false;
            txtHp.color = Color.black;
            txtAtk.color = Color.black;
            txtMdef.color = Color.black;
            txtPdef.color = Color.black;
            txtName.color = Color.black;
        }

        private void OnClickReset()
        {
            panelResArea.gameObject.SetActive(false);
            onClickResetEvent?.Invoke(this);
            imgDisposed.gameObject.SetActive(true);
            imgSelected.gameObject.SetActive(false);
            isInDeck = true;
            txtHp.color = Color.white;
            txtAtk.color = Color.white;
            txtMdef.color = Color.white;
            txtPdef.color = Color.white;
            txtName.color = Color.white;
        }

        public void Init(int playerNum, Action<UILobbyPlayerSlot> cbSettle, Action<UILobbyPlayerSlot> cbReset
                          ,Action<UILobbyPlayerSlot> cbSelect)
        {
            //일단 데모버전 아이콘 초기화

            imgDisposed.gameObject.SetActive(true);
            imgSelected.gameObject.SetActive(false);
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
            
            var baseStat = ExcelManager.GetInstance().CharT.GetCharData(playerNum).GetCharacterDataAsStat();

            txtHp.text = baseStat.HpMax.ToString();
            txtAtk.text = baseStat.Atk.ToString();
            txtMdef.text = baseStat.magDef.ToString();
            txtPdef.text = baseStat.phyDef.ToString();
            
            txtHp.color = Color.white;
            txtAtk.color = Color.white;
            txtMdef.color = Color.white;
            txtPdef.color = Color.white;
            txtName.color = Color.white;
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