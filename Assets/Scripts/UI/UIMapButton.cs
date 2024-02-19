

using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIMapButton:MonoBehaviour
    {
        [SerializeField] private Text txtStageState;
        [SerializeField] private Button btnArea;
        private StageNode showingNode;
        private bool isCleared;

        private event Action<StageNode> OnStageSelected;

        private void Awake()
        {
            
            btnArea.onClick.RemoveAllListeners();
            btnArea.onClick.AddListener(OnClicked);
        }

        public void Init(bool cleared, StageNode node,bool selectable, Action<StageNode> onClicked=null)
        {
            isCleared = cleared;
            showingNode = node;

            if (cleared)
            {
                txtStageState.text = "클리어!";
                btnArea.interactable = false;
                btnArea.image.color = Color.blue;
                //this.enabled = false;
            }
            else
            {
                txtStageState.text = "";
                btnArea.interactable = selectable;
                btnArea.image.color = showingNode.StageType==StageType.Boss?
                    Color.red :  Color.white;
                //this.enabled = true;
            }

            if (onClicked != null)
            {
                OnStageSelected -= onClicked;
                OnStageSelected += onClicked;
            }
        }

        public void UpdateState(StageNode curNode)
        {
            var up = curNode.Top;
            var down = curNode.Bottom;
            var right = curNode.Right;
            var selectable =
                up == showingNode || down == showingNode || right == showingNode;

            isCleared = showingNode.Cleared;
            if (isCleared)
            {
                txtStageState.text = "클리어!";
                btnArea.interactable = false;
                btnArea.image.color = Color.blue;
                //this.enabled = false;
            }
            else
            {
                txtStageState.text = "";
                btnArea.interactable = selectable;
                btnArea.image.color = showingNode.StageType==StageType.Boss?
                    Color.red :  Color.white;
                //this.enabled = true;
            }
        }

        private void OnClicked()
        {
            OnStageSelected?.Invoke(showingNode);
        }
    }
}