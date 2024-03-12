using System.Collections.Generic;
using System.Linq;
using Tables.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class ExcelImporter : ScriptableWizard
    {
        string fileName;
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;
        private int tabIndex;
        private string[] tabSubject =
        {
            "Main", "View", "Setting"
        };

        private ScriptableObject scrip=null;

        private Dictionary<string, List<string>> parseSlots;

        // Add menu item named "My Window" to the Window menu
        //[MenuItem("Window/Excel Importer")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(ExcelImporter));
        }

        
        private void OnGUI()
        {
            tabIndex = GUILayout.Toolbar(tabIndex, tabSubject);
            switch(tabIndex)
            {
                case 0:
                    OnGUI_Main();
                    break;
                case 1:
                    OnGUI_View();
                    break;
                case 2:
                    OnGUI_Setting();
                    break;
            }

            

            scrip = EditorGUILayout.ObjectField("Scriptable Object", scrip, typeof(ScriptableObject), true)
                as ScriptableObject;

            if (scrip != null)
            {
                GUILayout.Label("Script Ok");
                parseSlots = new Dictionary<string, List<string>>();
                var fields = scrip.GetType().GetFields();
                if (fields.Length > 0)
                {
                    foreach (var f in fields)
                    {
                        
                        bool isArray = f.GetType().IsArray;
                        var t = f.GetType();
                        bool isStructOrClass = f.GetType().IsClass || (f.GetType().IsValueType && !f.GetType().IsPrimitive);
                        if (isStructOrClass)
                        {
                            var subFields = f.FieldType.GetFields();
                            if (subFields.Length > 0)
                            {
                                var lst = new List<string>();
                                for (int i = 0; i < subFields.Length; i++)
                                {
                                    lst.Add("");
                                }
                                
                                for (int i = 0; i < subFields.Length; i++)
                                {
                                    lst[i] = EditorGUILayout.TextField(subFields[i].Name, lst[i]);
                                }
                                parseSlots.Add(f.Name, lst);
                            }
                        }
                        else if (isArray)
                        {
                            var lst = new List<string>(2){"",""};
                            lst[0] = EditorGUILayout.TextField( f.Name+" Array Start", lst[0]);
                            lst[1] = EditorGUILayout.TextField(f.Name+" Array End", lst[1]);
                            parseSlots.Add(f.Name, lst);
                        }
                        else
                        {
                            var lst = new List<string>(1){""};
                            lst[0] = EditorGUILayout.TextField(f.Name, lst[0]);
                            parseSlots.Add(f.Name, lst);
                        }

                    }
                }
                
            }
        }
        private void OnGUI_Main() { GUILayout.Label("Main"); }
        private void OnGUI_View() { GUILayout.Label("View"); }
        private void OnGUI_Setting() { GUILayout.Label("Setting"); }
    }
}