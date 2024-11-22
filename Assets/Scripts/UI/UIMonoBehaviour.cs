using System;
using UnityEngine;

namespace FluffyDisket.UI
{
    public enum UIType
    {
        None=-1,
        Battle=0,
        Formation=1,
        MapSelect=2,
        LobbyTeamSelect=3,
        Lobby=4,
        Event=5,
        StageReward
    }

    public class UIViewParam
    {
        
    }
    public class UIMonoBehaviour : MonoBehaviour
    {
        public virtual UIType type { get; }

        public virtual void Init(UIViewParam param)
        {
            
        }

        protected virtual void Dispose()
        {
            
        }

        public virtual void CallEnd()
        {
            Dispose();
        }
    }
}