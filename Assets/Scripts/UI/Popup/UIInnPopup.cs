using System;
using System.Collections.Generic;
using FluffyDisket;
using FluffyDisket.UI;
using Sirenix.OdinInspector;
using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class UIInnPopup:PopupMonoBehavior
    {
        public override PopupType type => PopupType.Inn;

        [SerializeField] private GameObject pnlHeroCards;
        [SerializeField] private GameObject pnlHeroCardInven;
        [SerializeField] private UIHeroInformationPart _informationPart;
        [SerializeField] private GameObject pnlInven;

        [SerializeField] private Toggle innToggle;
        [SerializeField] private Toggle invenToggle;

        [SerializeField] private Button btnCardInvenBack;

        [VerticalGroup("Prefabs")]
        [SerializeField] private UIHeroCardPortrait portPrefab;
        [VerticalGroup("Prefabs")]
        [SerializeField] private Transform portraitParent;

        private List<UIHeroCardPortrait> _portraitsPool;

        protected override void Awake()
        {
            base.Awake();
            
            innToggle.onValueChanged.RemoveAllListeners();
            innToggle.onValueChanged.AddListener(_ =>
            {
                if (_)
                {
                    pnlHeroCards.gameObject.SetActive(true);
                    pnlHeroCardInven.gameObject.SetActive(false);
                    pnlInven.gameObject.SetActive(false);
                }
            });
            
            invenToggle.onValueChanged.RemoveAllListeners();
            invenToggle.onValueChanged.AddListener(_ =>
            {
                if (_)
                {
                    pnlHeroCards.gameObject.SetActive(false);
                    pnlHeroCardInven.gameObject.SetActive(false);
                    pnlInven.gameObject.SetActive(true);
                }
            });
            
            btnCardInvenBack.onClick.RemoveAllListeners();
            btnCardInvenBack.onClick.AddListener(() =>
            {
                pnlHeroCards.gameObject.SetActive(true);
                pnlHeroCardInven.gameObject.SetActive(false);
                pnlInven.gameObject.SetActive(false);
            });
        }

        public static void OpenPopup()
        {
            var pop = PopupManager.GetInstance().GetPopup(PopupType.Inn);
            if (pop is UIInnPopup inven)
            {
                inven.Init();
                inven.gameObject.SetActive(true);
            }
        }

        private void Init()
        {
            if (_portraitsPool == null)
                _portraitsPool = new List<UIHeroCardPortrait>();

            var owned = AccountManager.GetInstance().CharacterOwned;

            int idx = 0;
            foreach (var pair in owned)
            {
                if (pair.Value)
                {
                    
                    if (idx < _portraitsPool.Count)
                    {
                        var current = _portraitsPool[idx];
                        current.gameObject.SetActive(true);
                        current.Init(pair.Key, OnClickCard);
                    }
                    else
                    {
                        var newcard = GameObject.Instantiate(portPrefab, portraitParent);
                        newcard.gameObject.SetActive(true);
                        newcard.Init(pair.Key, OnClickCard);
                    }

                    idx++;
                }
            }

            innToggle.isOn = true;
        }

        private void OnClickCard(CharacterData data)
        {
            pnlHeroCards.gameObject.SetActive(false);
            _informationPart.Init(data);
            pnlHeroCardInven.gameObject.SetActive(true);
        }

        protected override void Dispose()
        {
            base.Dispose();
            foreach (var p in _portraitsPool)
            {
                p.gameObject.SetActive(false);
            }
        }
    }
}