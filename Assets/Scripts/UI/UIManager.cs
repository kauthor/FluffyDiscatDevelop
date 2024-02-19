using System.Collections.Generic;
using UnityEngine;

namespace FluffyDisket.UI
{
    public class UIManager: CustomSingleton<UIManager>
    {
        [SerializeField] private UIMonoBehaviour[] Views;

        private Dictionary<UIType, UIMonoBehaviour> uiDic = new Dictionary<UIType, UIMonoBehaviour>();
        private UIMonoBehaviour currentView;

        public UIType CurentViewType
        {
            get
            {
                if(currentView)
                    return currentView.type;
                return UIType.None;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            uiDic = new Dictionary<UIType, UIMonoBehaviour>();

            foreach (var v in Views)
            {
                uiDic.Add(v.type, v);
                v.gameObject.SetActive(false);
            }
        }

        public UIMonoBehaviour ChangeView(UIType type)
        {
            if (currentView)
            {
                currentView.gameObject.SetActive(false);
                currentView.CallEnd();
            }

            if(uiDic.TryGetValue(type, out currentView))
                currentView.gameObject.SetActive(true);

            return currentView;
        }
    }
}