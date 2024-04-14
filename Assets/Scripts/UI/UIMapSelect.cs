using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace FluffyDisket.UI
{
    public class UIMapSelectParam:UIViewParam
    {
        public bool isFirstMap;
    }
    public class UIMapSelect:UIMonoBehaviour
    {
        [SerializeField] private UIMapButton mapButtonPrefab;
        [SerializeField] private Image mpaLinePrefab;
        [SerializeField] private Transform mapDisplayArea;
        [SerializeField] private Transform lineDisplayArea;
         public override UIType type => UIType.MapSelect;
        private List<StageNode> nodeTree;

        private List<UIMapButton> MadeNode;
        private Queue<UIMapButton> MapNodePool;

        private List<Image> MadeLines;
        private Queue<Image> MadeLinesPool;
        public override void Init(UIViewParam param)
        {
            base.Init(param);
            gameObject.SetActive(false);
            nodeTree = StageManager.GetInstance().StageTree;

            var curStage = StageManager.GetInstance().CurrentStage;
            var up = curStage.Top;
            var down = curStage.Bottom;
            var right = curStage.Right;
            var mp = param as UIMapSelectParam;
            if (mp == null)
            {
                Debug.LogError("Param is Not MapSelectParam!");
                return;
            }
            
            if (mp.isFirstMap)
            {
                Vector2 startPos = new Vector2(-300,0);
                
                int depth = 0;
                
                //UI를 재사용 시 오브젝트 풀링 - 다른 던전 입장 등의 상황.

                if (MadeNode != null && MadeNode.Count > 0)
                {
                    if (MapNodePool == null)
                        MapNodePool = new Queue<UIMapButton>();

                    foreach (var mn in MadeNode)
                    {
                        mn.gameObject.SetActive(false);
                        MapNodePool.Enqueue(mn);
                    }
                }
                else if (MadeNode == null)
                    MadeNode = new List<UIMapButton>();
                
                if (MadeLines != null && MadeLines.Count > 0)
                {
                    if (MadeLinesPool == null)
                        MadeLinesPool = new Queue<Image>();

                    foreach (var mn in MadeLines)
                    {
                        mn.gameObject.SetActive(false);
                        MadeLinesPool.Enqueue(mn);
                    }
                }
                else if (MadeLines == null)
                    MadeLines = new List<Image>();

                foreach (var n in nodeTree)
                {
                    //포로토에선 일단 일직선형 맵이라 가정하고 생성

                    var newNode = MapNodePool != null && MapNodePool.Count > 0
                        ? MapNodePool.Dequeue()
                        : Instantiate(mapButtonPrefab);

                    bool selectable =
                        up == n || down == n || right == n;
                    
                    newNode.Init(n.Cleared,n , selectable, OnSelectNode);
                    newNode.transform.SetParent(mapDisplayArea);
                    newNode.transform.localPosition = new Vector3(-200 + depth * 120, 0, 0);
                    MadeNode.Add(newNode);
                    if (n.StageType != StageType.Boss)
                    {
                        var newImage = MadeLinesPool != null && MadeLinesPool.Count > 0
                            ? MadeLinesPool.Dequeue()
                            : Instantiate(mpaLinePrefab);
                        
                        newImage.transform.SetParent(lineDisplayArea);
                        newImage.transform.localPosition = new Vector3(-140 + depth * 120, 0, 0);
                        MadeLines.Add(newImage);
                    }

                    depth++;
                }
            }
            else
            {
                //두번 째 맵 부터 열람 시 굳이 재생성 할 필요없이 갱신만 한다.
                foreach (var nn in MadeNode)
                {
                    nn.UpdateState(curStage);
                }
            }
            gameObject.SetActive(true);
        }

        private void OnSelectNode(StageNode node)
        {
            var stageM = StageManager.GetInstance();
            if (stageM.CurrentStage != null)
            {
                stageM.TryEnterNode(node, () =>
                {
                    UIManager.GetInstance().ChangeView(UIType.Formation).Init(new UIViewParam());
                });
            }
        }
        
        
    }
}