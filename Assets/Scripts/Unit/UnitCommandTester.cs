using FluffyDisket.Trait;
using UnityEngine;
using UnityEditor;

namespace FluffyDisket
{
#if UNITY_EDITOR
    public class UnitCommandTester:MonoBehaviour
    {
        [SerializeField] public int targetNumber;
        [SerializeField] public OptionType type;
        [SerializeField] public int opValue;
        [SerializeField] public int opDuration;

        //[]
        public void AddTestOption()
        {
            
        }
    }
#endif
}