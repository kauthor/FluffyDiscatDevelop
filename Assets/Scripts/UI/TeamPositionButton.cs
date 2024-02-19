using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class TeamPositionButton:Button
    {
        private event Action<TeamPositionButton> OnClicked;
        private Image child;
        private int playerNum;

        public int GetData()
        {
            if (child == null)
                return -1;

            return playerNum;
        }

        public void HandleListener(Action<TeamPositionButton> oc)
        {
            OnClicked = null;
            OnClicked += oc;
            playerNum = -1;
        }

        public void Dispose()
        {
            //OnClicked = null;
            if(child)
                Destroy(child);
            child = null;
            playerNum = -1;
        }

        protected override void Awake()
        {
            onClick.AddListener( ()=> OnClicked?.Invoke(this));
        }

        public void SetImage(Image img, int num)
        {
            if (img != null)
            {
                Transform transform1;
                (transform1 = img.transform).SetParent(this.transform);
                transform1.localPosition = Vector3.zero;
                
            }
            child = img;

            playerNum = num;
        }

        public void SwapImage(TeamPositionButton btn)
        {
            var ch = child;
            var num = playerNum;
            SetImage(btn.child, btn.playerNum);
            btn.SetImage(ch, num);
        }
    }
}