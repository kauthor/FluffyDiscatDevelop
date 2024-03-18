using UnityEngine;

namespace Tables
{
    [CreateAssetMenu]
    public class ExpTable:ScriptableObject
    {
        [SerializeField] private ExpData[] expDatas;
        public void SetExpData(ExpData[] arr) => expDatas = arr;
    }
}