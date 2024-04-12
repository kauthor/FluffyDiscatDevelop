

using System;
using FluffyDisket.UI;
using Tables;
using UnityEngine;

namespace FluffyDisket
{
    public enum Contents
    {
        Lobby=0,
        Field=1
    }
    public class GameManager : CustomSingleton<GameManager>
    {
        [SerializeField] private AccountManager accountManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private PopupManager _popupManager;
        [SerializeField] private ExcelManager _excelManager;
        private Contents currentContents;
        public Contents CurrentContents => currentContents;

        private bool isAuto = false;

        protected override void Awake()
        {
            base.Awake();
            Instantiate(_excelManager);
            Instantiate(accountManager);
            Instantiate(_uiManager);
            Instantiate(_popupManager);
        }

        private void Start()
        {
            UIManager.GetInstance().ChangeView(UIType.Lobby);
        }

        public bool SetAuto(bool isauto) => isAuto = isauto;
        public bool IsAuto => isAuto;
    }
}