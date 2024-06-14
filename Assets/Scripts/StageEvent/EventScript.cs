using System;
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
    public class EventScript:ScriptableObject
    {
        [SerializeField] public EventArticle[] EventArticles;
    }
}