using System;
using FluffyDisket.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UITeamFormationIcon:UIMonoBehaviour
    {

        private int charId;
        private UIPlayerSlot parent;

        public void Init(int Id, UIPlayerSlot startParent)
        {
            charId = Id;
            SetParent(startParent);
        }
        

        public void SetParent(UIPlayerSlot p)
        {
            if (p.child != null)
            {
                p.child.parent = parent;
                parent.SetChild(p.child);
                p.child.ResetPosition();
                
                parent = p;
                parent.SetChild(this);
        
                ResetPosition();
            }
            else
            {
                if(parent!=null)
                   parent.ReleaseChild();
                parent = p;
                parent.SetChild(this);
        
                ResetPosition();
            }
        }

        public void ResetPosition()
        {
            transform.SetParent(parent.transform);
            transform.localPosition = Vector3.zero;
        }

        public Tuple<int, int> GetFinalData()
        {
            var ret = 0;
            if (parent != null)
                ret = parent.ID;
            return new Tuple<int, int>(charId, ret);
        }
    }
}