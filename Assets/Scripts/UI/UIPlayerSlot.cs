using System;
using FluffyDisket.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIPlayerSlot:UIMonoBehaviour, IDropHandler, IPointerDownHandler
    {

        private Action<UIPlayerSlot> OnDropped;
        private Action<UITeamFormationIcon> OnStart;
        public UITeamFormationIcon child;
        
        public int ID { get; private set; }

        public void InitHandler(Action<UIPlayerSlot> handle, Action<UITeamFormationIcon> onstart, int id)
        {
            OnDropped = handle;
            OnStart = onstart;
            ID = id;
            child = null;
        }

        public void SetChild(UITeamFormationIcon icon)
        {
            child = icon;
        }

        public void ReleaseChild() => child = null;

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            OnDropped?.Invoke(this);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnStart?.Invoke(child);
        }
    }
}