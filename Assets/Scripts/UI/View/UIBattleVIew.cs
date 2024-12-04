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
        [SerializeField] private Transform logArea;
        [SerializeField] private UILogSentence logPrefab;

        [SerializeField] private UIBattleUnitInfoParts heroInfoPartsPrefab;

        [SerializeField] private Transform heroInfoSlot;

        private List<UIBattleUnitInfoParts> cachedInfoParts;
        
        private List<UIHpBar> bars;
        private Queue<UIHpBar> barPool;
        private List<UILogSentence> managedLog;
        public override UIType type => UIType.Battle;

        private void Awake()
        {
            btnMap.onClick.RemoveAllListeners();
            btnMap.onClick.AddListener(OnClickOpenButton);
            btnBackToLobby.onClick.RemoveAllListeners();
            btnBackToLobby.onClick.AddListener(GoLobby);
            managedLog = new List<UILogSentence>();
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
            if (cachedInfoParts == null)
                cachedInfoParts = new List<UIBattleUnitInfoParts>();
            
            btnMap.gameObject.SetActive(false);
            btnBackToLobby.gameObject.SetActive(false);
            var battleP = param as BattleViewParam;
            if (battleP == null)
            {
                Debug.LogError("Undefined Param");
                return;
            }

            int listNumber = 0;
            foreach (var pl in battleP.players)
            {
                if(pl.IsDead) continue;
                UIBattleUnitInfoParts part;
                if (cachedInfoParts != null && cachedInfoParts.Count > listNumber)
                    part = cachedInfoParts[listNumber];
                else
                {
                    part = GameObject.Instantiate(heroInfoPartsPrefab, heroInfoSlot);
                    cachedInfoParts.Add(part);
                }

                listNumber++;
                
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
                
                part.gameObject.SetActive(true);
                part.Init(pl);
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
            UIManager.GetInstance().ChangeView(UIType.Event).Init(new EventUiParam());
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
            for (int i = 0; i < managedLog.Count; i++)
            {
                Destroy(managedLog[i].gameObject);
            }
            managedLog.Clear();
            foreach (var bar in bars)
            {
                bar.gameObject.SetActive(false);
                barPool.Enqueue(bar);
            }

            foreach (var c in cachedInfoParts)
            {
                c.gameObject.SetActive(false);
            }
            //AutoButton.onClick.RemoveAllListeners();
        }

        private void GoLobby()
        {
            BattleManager.GetInstance().EndBattleScene();
            SceneManager.LoadSceneAsync("Scenes/SelectScene");
        }

        public void ReceiveLog(string txt)
        {
            var log = Instantiate(logPrefab, logArea, true);
            log.SetLog(txt);
        }
    }
}