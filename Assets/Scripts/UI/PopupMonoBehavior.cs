using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class PopupMonoBehavior:MonoBehaviour
    {
        public virtual PopupType type { get; }
        [SerializeField] private Button BtnClose;
        [SerializeField] private Button BtnOutClose;

        protected virtual void Awake()
        {
            BtnClose.onClick.AddListener(OnCloseClick);
            if(BtnOutClose!=null)
               BtnOutClose.onClick.AddListener(OnCloseClick);
        }

        protected virtual void Dispose()
        {
            
        }

        public virtual void CallEnd()
        {
            Dispose();
        }

        protected virtual void OnCloseClick()
        {
            PopupManager.GetInstance().ClosePopup(type);
        }

        public void Close()
        {
            OnCloseClick();
        }
    }
}