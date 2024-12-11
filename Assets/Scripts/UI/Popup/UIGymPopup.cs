using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tables;
using Tables.Player;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIGymPopup:PopupMonoBehavior
    {
        public override PopupType type => PopupType.Gym;
        
        [SerializeField] private UIHeroUpgradeExpactResult _informationPart;
        [SerializeField] private GameObject pnlDesc;

        [SerializeField] private Toggle normalToggle;
        [SerializeField] private Toggle specialToggle;

        [SerializeField] private Button btnTrainStart;

        [VerticalGroup("Prefabs")]
        [SerializeField] private UIHeroGymCard portPrefab;
        [VerticalGroup("Prefabs")]
        [SerializeField] private Transform portraitParent;

        [SerializeField] private Text txtTrainningHero;
        
        private List<UIHeroGymCard> _portraitsPool;

        protected override void Awake()
        {
            base.Awake();
            
            /*normalToggle.onValueChanged.RemoveAllListeners();
            normalToggle.onValueChanged.AddListener(_ =>
            {
                if (_)
                {
                    pnlHeroCards.gameObject.SetActive(true);
                    pnlHeroCardInven.gameObject.SetActive(false);
                    pnlInven.gameObject.SetActive(false);
                }
            });*/
            
            /*specialToggle.onValueChanged.RemoveAllListeners();
            specialToggle.onValueChanged.AddListener(_ =>
            {
                if (_)
                {
                    pnlHeroCards.gameObject.SetActive(false);
                    pnlHeroCardInven.gameObject.SetActive(false);
                    pnlInven.gameObject.SetActive(true);
                }
            });*/
            
            btnTrainStart.onClick.RemoveAllListeners();
            btnTrainStart.onClick.AddListener(() =>
            {
                //pnlHeroCards.gameObject.SetActive(true);
                //pnlHeroCardInven.gameObject.SetActive(false);
                OnClickTrain();
            });
            
            
        }

        public static void OpenPopup()
        {
            var pop = PopupManager.GetInstance().GetPopup(PopupType.Gym);
            if (pop is UIGymPopup inven)
            {
                inven.Init();
                inven.gameObject.SetActive(true);
            }
        }

        private void Init()
        {
            if (_portraitsPool == null)
                _portraitsPool = new List<UIHeroGymCard>();

            var acc = AccountManager.GetInstance();
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
                        current.Init(acc.IsTrainning(pair.Key), pair.Key, OnClickCard);
                        
                    }
                    else
                    {
                        var newcard = GameObject.Instantiate(portPrefab, portraitParent);
                        newcard.gameObject.SetActive(true);
                        newcard.Init(acc.IsTrainning(pair.Key), pair.Key, OnClickCard);
                        _portraitsPool.Add(newcard);
                    }

                    idx++;
                }
            }

            //normalToggle.isOn = true;

            pnlDesc.gameObject.SetActive(false);            
            btnTrainStart.enabled = AccountManager.GetInstance().TrainEnable();
            txtTrainningHero.text = $"{AccountManager.GetInstance().TrainningCharacterAmount}/10";
        }


        private int currentSelectedCharacter = 0;
        private void OnClickCard(CharacterData data)
        {
            //pnlHeroCards.gameObject.SetActive(true);
            _informationPart.InitData(data);
            pnlDesc.gameObject.SetActive(true);
            currentSelectedCharacter = data.id;
        }

        private void OnClickTrain()
        {
            pnlDesc.gameObject.SetActive(false);
            AccountManager.GetInstance().TryTrainCharacter(currentSelectedCharacter);
            foreach (var p in _portraitsPool)
            {
                p.gameObject.SetActive(false);
            }
            Init();
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