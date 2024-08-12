using UnityEditor;
using UnityEngine;

namespace FluffyDisket
{
    [CustomEditor(typeof(UnitCommandTester))]
    public class TestInspector:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var tester = (UnitCommandTester) target;

            if (GUILayout.Button("Add Command"))
            {
                tester.AddTestOption();
            }
        }
    }
}