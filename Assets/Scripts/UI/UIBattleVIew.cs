using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class BattleViewParam : UIViewParam
    {
        public BattleUnit[] players;
        public BattleUnit[] enemies;
    }
    public class UIBattleVIew:UIMonoBehaviour
    {
        //[SerializeField] private UISkillButton[] buttons;
        [SerializeField] private Text StartText;
        [SerializeField] private Text VictoryText;
        [SerializeField] private Text DefeatText;
        [SerializeField] private Button AutoButton;
        //[SerializeField] private Image autoOn;
        [SerializeField] private UIHpBar hpbarPrefab;
        [SerializeField] private Transform hpBarArea;
        [SerializeField] private Button btnMap;
        [SerializeField] private Button btnBackToLobby;

        private List<UIHpBar> bars;
        private Queue<UIHpBar> barPool;
        public override UIType type => UIType.Battle;

        private void Awake()
        {
            btnMap.onClick.RemoveAllListeners();
            btnMap.onClick.AddListener(OnClickOpenButton);
            btnBackToLobby.onClick.RemoveAllListeners();
            btnBackToLobby.onClick.AddListener(GoLobby);
        }

        public override void Init(UIViewParam param)
        {
            base.Init(param);
            GameManager.GetInstance().SetAuto(true);
            if(bars==null) bars = new List<UIHpBar>();
            if (barPool == null) barPool = new Queue<UIHpBar>(); 
            StartText.gameObject.SetActive(false);
            VictoryText.gameObject.SetActive(false);
            DefeatText.gameObject.SetActive(false);
            BattleManager.OnBattleEnd -= OnBattleEnd;
            BattleManager.OnBattleEnd += OnBattleEnd;
            //autoOn.gameObject.SetActive(GameManager.GetInstance().IsAuto);
            
            btnMap.gameObject.SetActive(false);
            btnBackToLobby.gameObject.SetActive(false);
            var battleP = param as BattleViewParam;
            if (battleP == null)
            {
                Debug.LogError("Undefined Param");
                return;
            }

            foreach (var pl in battleP.players)
            {
                if(pl.IsDead) continue;
                if (barPool.Count <= 0)
                {
                    var hpbar = Instantiate(hpbarPrefab).GetComponent<UIHpBar>();
                    if (hpbar)
                    {
                        hpbar.Init(pl);
                        hpbar.transform.SetParent(hpBarArea);
                        bars.Add(hpbar);
                    }
                }
                else
                {
                    var hpbar = barPool.Dequeue();
                    hpbar.gameObject.SetActive(true);
                    if (hpbar)
                    {
                        hpbar.Init(pl);
                        hpbar.transform.SetParent(hpBarArea);
                        bars.Add(hpbar);
                    }
                }
            }

            foreach (var en in battleP.enemies)
            {
                if (barPool.Count <= 0)
                {
                    var hpbar = Instantiate(hpbarPrefab).GetComponent<UIHpBar>();
                    if (hpbar)
                    {
                        hpbar.Init(en);
                        hpbar.transform.SetParent(hpBarArea);
                        bars.Add(hpbar);
                    }
                }
                else
                {
                    var hpbar = barPool.Dequeue();
                    hpbar.gameObject.SetActive(true);
                    if (hpbar)
                    {
                        hpbar.Init(en);
                        hpbar.transform.SetParent(hpBarArea);
                        bars.Add(hpbar);
                    }
                }
            }
        }

        private void OnClickOpenButton()
        {
            var isFirst = StageManager.GetInstance()?.IsFirstStage()?? false;
            UIManager.GetInstance().ChangeView(UIType.MapSelect).Init(new UIMapSelectParam()
            {
                isFirstMap = isFirst
            });
        }

        private Coroutine startAni = null;
        public void StartGameAni()
        {
            startAni = StartCoroutine(StartImpl());
        }

        private IEnumerator StartImpl()
        {
            StartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            StartText.gameObject.SetActive(false);
        }

        private void Victory() => VictoryText.gameObject.SetActive(true);
        private void Defeat() => DefeatText.gameObject.SetActive(true);

        private void OnBattleEnd(bool end)
        {
            if (end)
            {
                Victory();
                var curst = StageManager.GetInstance()?.CurrentStage;
                if (curst != null && curst.StageType != StageType.Boss)
                {
                    btnMap.gameObject.SetActive(true);
                }
                else if (curst != null && curst.StageType == StageType.Boss)
                {
                    btnBackToLobby.gameObject.SetActive(true);
                }
            }
            else
            {
                Defeat();
                btnBackToLobby.gameObject.SetActive(true);
            }
        }

        protected override void Dispose()
        {
            base.Dispose();
            BattleManager.OnBattleEnd -= OnBattleEnd;
            foreach (var bar in bars)
            {
                bar.gameObject.SetActive(false);
                barPool.Enqueue(bar);
            }
            //AutoButton.onClick.RemoveAllListeners();
        }

        private void GoLobby()
        {
            SceneManager.LoadSceneAsync("Scenes/SelectScene");
        }
    }
}