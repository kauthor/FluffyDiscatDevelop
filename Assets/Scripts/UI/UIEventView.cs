using System;
using FluffyDisket.StageEvent;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FluffyDisket.UI
{
    public class EventUiParam : UIViewParam
    {
        public int eventId;
    }
    
    
    public class UIEventView:UIMonoBehaviour
    {
        [SerializeField] private Button btnNext;
        [SerializeField] private Button[] btnSelects;
        [SerializeField] private Text[] txtSelects;
        [SerializeField] private Text txtDialogue;

        [SerializeField] private Image imgBg;
        public override UIType type => UIType.Event;

        //#if UNITY_EDITOR
        
        /// <summary>
        /// 임시 데이터
        /// </summary>
        [SerializeField] private EventScript scriptData;


        public override void Init(UIViewParam param)
        {
            base.Init(param);
            
            //todo: 이벤트 아이디로 스크립트 데이터를 동적으로 불러와야 하는데...
            
            ShowStart();
        }

        private void ShowStart()
        {
            OpenDialogue(0);
        }

        private void OpenDialogue(int id)
        {
            if (id >= scriptData.EventArticles.Length)
            {
                EndView();
                return;
            }
            var currentEvent = scriptData.EventArticles[id];

            switch (currentEvent.ArticleType)
            {
                case EventArticleType.Dialogue:
                    var dia = currentEvent as DialogueArticle;
                    foreach (var btn in btnSelects)
                    {
                        btn.gameObject.SetActive(false);
                    }
                    //todo 배경 이미지 수정 구문 추가해야함
                    txtDialogue.text = $"Dialogue/{currentEvent.Id}"; 
                    btnNext.onClick.RemoveAllListeners();
                    btnNext.gameObject.SetActive(true);
                    btnNext.onClick.AddListener(() =>
                    {
                        OpenDialogue(id+1);
                    });
                    break;
                case EventArticleType.Choice:
                    var cho = currentEvent as ChoiceArticle;
                    for (int i = 0; i < btnSelects.Length; i++)
                    {
                        if (i >= cho.buttons.Length)
                        {
                            btnSelects[i].gameObject.SetActive(false);
                            continue;
                        }
                        
                        btnSelects[i].gameObject.SetActive(true);
                        btnSelects[i].onClick.RemoveAllListeners();
                        btnSelects[i].onClick.AddListener(() =>
                        {
                            OpenDialogue(cho.buttons[i].jumpId-1);
                        });
                        txtSelects[i].text = $"Dialogue/{cho.buttons[i].dialogueId}";
                    }
                    btnNext.gameObject.SetActive(false);
                    break;
                case EventArticleType.GetDamage:
                case EventArticleType.GetGold:
                    btnNext.onClick.AddListener(() =>
                    {
                        OpenDialogue(id+1);
                    });
                    break;
                case EventArticleType.Jump:
                    var jump = currentEvent as JumpArticle;
                    OpenDialogue(jump.jumpId-1);
                    break;
                case EventArticleType.RandomCheck:
                    var rand = currentEvent as RandomCheckArticle;
                    var randINt = Random.Range(0, 100);
                    int next = 0;
                    int eventId = 0;
                    for (int i = 0; i < rand.randomRatios.Length; i++)
                    {
                        next += rand.randomRatios[i];
                        if (randINt <= next)
                        {
                            eventId = i;
                            break;
                        }
                    }
                    OpenDialogue(rand.jumpTo[eventId]-1);
                    break;
                case EventArticleType.End:
                default:
                    EndView();
                    break;
            }
        }

        private void EndView()
        {
            UIManager.GetInstance().ChangeView(UIType.Formation).Init(new UIViewParam());
        }
    }
}