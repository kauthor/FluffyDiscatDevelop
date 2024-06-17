using System;
using System.Linq;
using UnityEngine;

namespace FluffyDisket.StageEvent
{

    public enum EventArticleType
    {
        NONE=0,
        Dialogue,
        Choice,
        Jump,
        GetGold,
        RandomCheck,
        GetDamage,
        Gacha,
        CheckGold,
        GetItem,
        End
    }
    
    [Serializable]
    public  class EventArticle
    {
        public virtual EventArticleType ArticleType { get; }
        public int Id;
    }

    [Serializable]
    public class DialogueArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.Dialogue;

        public int dialogueId;
        public int talker;
        public int imageKey;
    }


    [Serializable]
    public class ChoiceButtonDesc
    {
        public int dialogueId;
        public int jumpId;
    }
    
    [Serializable]
    public class ChoiceArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.Choice;

        public ChoiceButtonDesc[] buttons;
    }

    [Serializable]
    public class JumpArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.Jump;

        public int jumpId;
    }

    [Serializable]
    public class GetGoldArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.GetGold;

        public int amount;
    }

    [Serializable]
    public class RandomCheckArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.RandomCheck;

        public int[] randomRatios;
        public int[] jumpTo;
    }

    [Serializable]
    public class GetDamageArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.GetDamage;

        public int damage;
    }
    
    [Serializable]
    public class EndArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.End;

        //public int damage;
    }
    
    [Serializable]
    public class GachaArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.Gacha;

        //public int damage;
    }
    [Serializable]
    public class CheckGoldArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.CheckGold;

        public int trueJump;
        public int falseJump;

        //public int damage;
    }
    
    [Serializable]
    public class GetItemArticle : EventArticle
    {
        public override EventArticleType ArticleType => EventArticleType.GetItem;

        //public int damage;
    }
    
    [CreateAssetMenu]
    public class EventScript:ScriptableObject
    {
        //[SerializeField] public EventArticle[] EventArticles;
        [SerializeField] private DialogueArticle[] DialogueArticles;
        [SerializeField] private ChoiceArticle[] ChoiceArticles;
        [SerializeField] private JumpArticle[] JumpArticles;
        [SerializeField] private GetGoldArticle[] GetGoldArticles;
        [SerializeField] private RandomCheckArticle[] RandomCheckArticles;
        [SerializeField] private GetDamageArticle[] GetDamageArticles;
        [SerializeField] private CheckGoldArticle[] checkGoldArticles;
        [SerializeField] private GachaArticle[] gachaArticles;
        [SerializeField] private GetItemArticle[] getItemArticles;
        [SerializeField] private EndArticle[] EndArticles;
     
        
        private EventArticle[] eventArticles;

        public EventArticle[] EventArticles
        {
            get
            {
                if (eventArticles.Length == 0)
                {
                    var uni = eventArticles.Union(DialogueArticles);
                    uni = uni.Union(ChoiceArticles).Union(JumpArticles)
                        .Union(GetGoldArticles).Union(RandomCheckArticles).Union(GetGoldArticles)
                        .Union(GetDamageArticles).Union(EndArticles)
                        .Union(getItemArticles).Union(checkGoldArticles).Union(gachaArticles);

                    var ret = uni.ToArray();
                    ret = ret.OrderBy(_ => _.Id).ToArray();
                    eventArticles = ret;
                }

                return eventArticles;
            }
        }
    }
}