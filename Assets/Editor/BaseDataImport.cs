using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ExcelDataReader;
using Tables;

namespace Editor
{
    public class BaseDataImport : EditorWindow
    {
        private BaseTable scrip=null;
        
        [MenuItem("Window/Excel Importer/Base Data")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(BaseDataImport));
        }

        private void OnGUI()
        {
            var pathProj = Application.dataPath.Replace("FluffyDiscatDevelop/Assets", "FluffyDiscatDevelop");
            string path =  "/Plan/Table/Live_Data/01.Base_Table.xlsx";
            string path2 = "/Plan/Table/Live_Data/02.Exp_Table.xlsx";
            
            //scrip = EditorGUILayout.ObjectField("Scriptable Object", scrip, typeof(ScriptableObject), true)
             //   as BaseTable;
            
            if (GUILayout.Button("Base Table & Exp Table Export"))
            {
                BaseTable baseT = ScriptableObject.CreateInstance<BaseTable>();
                //var fields = scrip.GetType().GetField("baseDatas");
                using (var stream = File.Open(pathProj+ path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            BaseData[] baseArr = new BaseData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                BaseData newB = new BaseData()
                                {
                                    id= data1Parse, 
                                    unit = data2Parse, 
                                    data = data3Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            baseT.SetBaseData(baseArr);
                        }
                    }
                }
                
                using (var stream = File.Open(pathProj+ path2, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            ExpData[] baseArr = new ExpData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                ExpData newB = new ExpData()
                                {
                                    level= data1Parse, 
                                    heroCount = data2Parse, 
                                    reqExp = data3Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            baseT.SetExpData(baseArr);
                        }
                    }
                }
                AssetDatabase.CreateAsset(baseT, "Assets/Tables/BaseTable.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }
}