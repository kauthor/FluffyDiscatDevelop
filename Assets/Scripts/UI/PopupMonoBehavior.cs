using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class PopupMonoBehavior:MonoBehaviour
    {
        public virtual PopupType type { get; }
        [SerializeField] private Button BtnClose;

        protected virtual void Awake()
        {
            BtnClose?.onClick.AddListener(OnCloseClick);
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