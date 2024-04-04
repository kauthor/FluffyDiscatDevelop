using UnityEngine;

namespace Tables
{
    [CreateAssetMenu]
    public class ExpTable:ScriptableObject
    {
        [SerializeField] private ExpData[] expDatas;
        public void SetExpData(ExpData[] arr) => expDatas = arr;
        
        public ExpData GetExpData(int index)
        {
            if (index >= expDatas.Length)
                return new ExpData();

            return expDatas[index];
        }
    }
}