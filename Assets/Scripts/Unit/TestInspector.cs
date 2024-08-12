using UnityEditor;
using UnityEngine;

namespace FluffyDisket
{
    [CustomEditor(typeof(UnitCommandTester))]
    public class TestInspector:Editor
    {

        [MenuItem("Fluffy Extension/Unit Trait Tester _F9")]
        public static void MakeCommandTester()
        {
            var go = new GameObject();
            go.name = "Battle Unit Tester======================================";
            go.AddComponent<UnitCommandTester>();
            GameObject.DontDestroyOnLoad(go);
            Selection.activeGameObject = go;
        }
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var tester = (UnitCommandTester) target;

            if (GUILayout.Button("Add Trait"))
            {
                tester.AddTestTrait();
            }

            if (GUILayout.Button("Add Option"))
            {
                tester.AddTestOption();
            }
            
            if(GUILayout.Button("Pause Game"))
                tester.PauseGame();
            
            if(GUILayout.Button("Resume Game"))
                tester.ResumeGame();
        }
    }
}