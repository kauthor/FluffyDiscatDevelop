using System;
using System.Collections.Generic;
using System.Linq;
using FluffyDisket.UI.Inven;
using UI.Popup;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UILobbyTeamSelect:UIMonoBehaviour
    {
        private List<int> ownedPlayers;

        private List<int> deckPlayerList;
        private List<int> reservedPlayerList;

        private List<UILobbyPlayerSlot> managedSlots;

        public override UIType type => UIType.LobbyTeamSelect;

        [SerializeField] private Transform deckArea;
        [SerializeField] private Transform reserveArea;

        [SerializeField] private UILobbyPlayerSlot playerSlotPrefab;
        
        [SerializeField] private Button btnGoGame;

        [SerializeField] private Button btnInven;
        [SerializeField] private Text txtGold;

        [SerializeField] private Button btnPub; 
        [SerializeField] private Button btnInn; 
        [SerializeField] private Button btnGym;

        private UILobbyPlayerSlot currentSelected;
        
        private void Awake()
        {
            if (managedSlots == null)
                managedSlots = new List<UILobbyPlayerSlot>();
            
            btnGoGame.onClick.AddListener(GoGame);
            btnInven?.onClick.AddListener(OpenInven);
            btnPub.onClick.RemoveAllListeners();
            btnPub.onClick.AddListener(OpenPub);
            
            btnInn.onClick.RemoveAllListeners();
            btnInn.onClick.AddListener(OpenInn);
            
            btnGym.onClick.RemoveAllListeners();
            btnGym.onClick.AddListener(OpenGym);
        }

        private void OpenInven()
        {
            UIInvenPopup.OpenPopup();
        }

        private void OpenPub()
        {
            UIPubPopup.OpenPopup();
        }

        private void OpenGym()
        {
            UIGymPopup.OpenPopup();
        }

        private void OpenInn()
        {
            UIInnPopup.OpenPopup();
        }

        public override void Init(UIViewParam param)
        {
            base.Init(param);
            AccountManager.GetInstance().SyncLobby();
            btnGoGame.enabled = false;
            currentSelected = null;
            if (ownedPlayers == null)
                ownedPlayers = new List<int>();
            else
            {
                ownedPlayers.Clear();
            }
            
            if (deckPlayerList == null)
                deckPlayerList = new List<int>();
            else
            {
                deckPlayerList.Clear();
            }
            
            if (reservedPlayerList == null)
                reservedPlayerList = new List<int>();
            else
            {
                reservedPlayerList.Clear();
            }

            var acc = AccountManager.GetInstance();

            if (acc != null)
            {
                foreach (var pair in acc.CharacterOwned)
                {
                    if (pair.Value)
                    {
                        ownedPlayers.Add(pair.Key);
                        deckPlayerList.Add(pair.Key);
                    }
                }

                int i = 0;
                foreach (var ow in ownedPlayers)
                {
                    if (managedSlots.Count < i)
                    {
                        managedSlots[i].gameObject.SetActive(true);
                        managedSlots[i].transform.SetParent(deckArea);
                        managedSlots[i].Init(ow, SettleIcon, ResetIcon, OnSelectSlot);
                    }
                    else
                    {
                        var newIcon = Instantiate(playerSlotPrefab, deckArea, true);
                        managedSlots.Add(newIcon);
                    
                        newIcon.Init(ow, SettleIcon, ResetIcon, OnSelectSlot);
                    }
                    
                }
            }

            txtGold.text = AccountManager.GetInstance()?.GetOwnedGold().ToString() ?? "0";
        }

        protected override void Dispose()
        {
            base.Dispose();
            foreach (var man in managedSlots)
            {
                man.gameObject.SetActive(false);
            }
        }

        private void SettleIcon(UILobbyPlayerSlot slot)
        {
            
            deckPlayerList.Remove(slot.PlayerNumber);
            reservedPlayerList.Add(slot.PlayerNumber);

            btnGoGame.enabled = true;
        }

        private void OnSelectSlot(UILobbyPlayerSlot slot)
        {
            if (currentSelected == slot)
                return;
            
            if (currentSelected != null)
            {
                currentSelected.SetPanelOff();
            }

            currentSelected = slot;
        }

        private void ResetIcon(UILobbyPlayerSlot slot)
        {
            
            deckPlayerList.Add(slot.PlayerNumber);
            reservedPlayerList.Remove(slot.PlayerNumber);

            btnGoGame.enabled = reservedPlayerList.Count > 0;
        }

        private void GoGame()
        {
            if (reservedPlayerList != null && reservedPlayerList.Count > 0)
            {
                AccountManager.GetInstance().SetCurrentBattlePlayer(reservedPlayerList);
                SceneManager.LoadSceneAsync("Scenes/SampleScene").completed +=
                    _ =>
                    {
                        BattleManager.GetInstance().TryStartBattle();
                    };
            }
        }
    }
}