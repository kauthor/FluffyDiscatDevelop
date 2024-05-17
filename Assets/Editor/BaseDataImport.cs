using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ExcelDataReader;
using FluffyDisket;
using Tables;
using Tables.Player;
using MonsterData = Tables.MonsterData;

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
            string path3 = "/Plan/Table/Live_Data/03.Character_Table.xlsx";
            string path4 = "/Plan/Table/Live_Data/04.Trait_Table.xlsx";
            string path6 = "/Plan/Table/Live_Data/06.Chr_Name_Table.xlsx";
            string path7 = "/Plan/Table/Live_Data/07.Stage_Table.xlsx";
            string path8 = "/Plan/Table/Live_Data/08.Monster_Level_Table.xlsx";
            string path9 = "/Plan/Table/Live_Data/09.Monster_Table.xlsx";
            string path10 = "/Plan/Table/Live_Data/10.Monster_Group_Table.xlsx";
            
            //scrip = EditorGUILayout.ObjectField("Scriptable Object", scrip, typeof(ScriptableObject), true)
             //   as BaseTable;
            
             //1.base Table
             
            if (GUILayout.Button("Base Table Export"))
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
                
               
                AssetDatabase.CreateAsset(baseT, "Assets/Tables/BaseTable.asset");
                AssetDatabase.SaveAssets();
            }

            //2.Exp Table
            
            if (GUILayout.Button("Exp Table Export"))
            {
                ExpTable expT = ScriptableObject.CreateInstance<ExpTable>();
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
                                //string data3 = result.Tables[i].Rows[j][2].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                //int data3Parse = Int32.Parse(data3);
                                ExpData newB = new ExpData()
                                {
                                    level= data1Parse, 
                                    //heroCount = data2Parse, 
                                    reqExp = data2Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            expT.SetExpData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(expT, "Assets/Tables/ExpTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
                
            }
            
            
            //3.Character Table
            if (GUILayout.Button("Character Table Export"))
            {
                CharacterTable charT = ScriptableObject.CreateInstance<CharacterTable>();
                using (var stream = File.Open(pathProj+ path3, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            CharacterData[] baseArr = new CharacterData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                string data4 = result.Tables[i].Rows[j][3].ToString();
                                string data5 = result.Tables[i].Rows[j][4].ToString();
                                string data6 = result.Tables[i].Rows[j][5].ToString();
                                string data7 = result.Tables[i].Rows[j][6].ToString();
                                string data8 = result.Tables[i].Rows[j][7].ToString();
                                string data9 = result.Tables[i].Rows[j][8].ToString();
                                string data10 = result.Tables[i].Rows[j][9].ToString();
                                string data11 = result.Tables[i].Rows[j][10].ToString();
                                string data12 = result.Tables[i].Rows[j][11].ToString();
                                string data13 = result.Tables[i].Rows[j][12].ToString();
                                string data14 = result.Tables[i].Rows[j][13].ToString();
                                string data15 = result.Tables[i].Rows[j][14].ToString();
                                string data16 = result.Tables[i].Rows[j][15].ToString();
                                string data17 = result.Tables[i].Rows[j][16].ToString();
                                string data18 = result.Tables[i].Rows[j][17].ToString();
                                string data19 = result.Tables[i].Rows[j][18].ToString();
                                string data20 = result.Tables[i].Rows[j][19].ToString();
                                string data21 = result.Tables[i].Rows[j][20].ToString();
                                string data22 = result.Tables[i].Rows[j][21].ToString();
                                string data23 = result.Tables[i].Rows[j][22].ToString();
                                string data24 = result.Tables[i].Rows[j][23].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                int data4Parse = Int32.Parse(data4);
                                int data5Parse = Int32.Parse(data5);
                                int data6Parse = Int32.Parse(data6);
                                int data7Parse = Int32.Parse(data7);
                                int data8Parse = Int32.Parse(data8);
                                int data9Parse = Int32.Parse(data9);
                                int data10Parse = Int32.Parse(data10);
                                int data11Parse = Int32.Parse(data11);
                                int data12Parse = Int32.Parse(data12);
                                int data13Parse = Int32.Parse(data13);
                                int data14Parse = Int32.Parse(data14);
                                int data15Parse = Int32.Parse(data15);
                                int data16Parse = Int32.Parse(data16);
                                int data17Parse = Int32.Parse(data17);
                                int data18Parse = Int32.Parse(data18);
                                int data19Parse = Int32.Parse(data19);
                                int data20Parse = Int32.Parse(data20);
                                int data21Parse = Int32.Parse(data21);
                                int data22Parse = Int32.Parse(data22);
                                int data23Parse = Int32.Parse(data23);
                                int data24Parse = Int32.Parse(data24);
                                
                                CharacterData newB = new CharacterData()
                                {
                                    id=data1Parse,
                                    gameGroup=data2Parse,
                                    gender=data3Parse,
                                    tribe=data4Parse,
                                    chrType = data5Parse,
                                    job=data6Parse,
                                    maxHp=data7Parse,
                                    atk=data8Parse,
                                    armor=data9Parse,
                                    magicArmor=data10Parse,
                                    hpRegen = data11Parse,
                                    hpAbsolve = data12Parse,
                                    critical = data13Parse,
                                    critDamage = data14Parse,
                                    attackCoolTime = data15Parse,
                                    dodge = data16Parse,
                                    range = data17Parse,
                                    moveSpeed = data18Parse,
                                    damIncrease = data19Parse,
                                    damDecrease = data20Parse,
                                    aeo = data21Parse,
                                    accuracy = data22Parse,
                                    startItemGroup = data23Parse,
                                    startTraitGroup = data24Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            charT.SetBaseData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(charT, "Assets/Tables/CharacterTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            
            //6.Char Name Data
            if (GUILayout.Button("Character Name Table Export"))
            {
                CharNameTable charT = ScriptableObject.CreateInstance<CharNameTable>();
                using (var stream = File.Open(pathProj+ path6, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            CharNameData[] baseArr = new CharNameData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                //string data3 = result.Tables[i].Rows[j][2].ToString();
                                //string data4 = result.Tables[i].Rows[j][3].ToString();
                                //string data5 = result.Tables[i].Rows[j][4].ToString();
                                //string data6 = result.Tables[i].Rows[j][5].ToString();
                                //string data7 = result.Tables[i].Rows[j][6].ToString();
                                //string data8 = result.Tables[i].Rows[j][7].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                //int data3Parse = Int32.Parse(data3);
                                //int data4Parse = Int32.Parse(data4);
                                //int data5Parse = Int32.Parse(data5);
                               // int data6Parse = Int32.Parse(data6);
                               // int data7Parse = Int32.Parse(data7);
                               // int data8Parse = Int32.Parse(data8);
                                CharNameData newB = new CharNameData()
                                {
                                    nameGroup= data1Parse,
                                    nameId= data2Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            charT.SetCharData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(charT, "Assets/Tables/CharNameTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            
            //7.Stage Data
            
            if (GUILayout.Button("Stage Table Export"))
            {
                StageTable charT = ScriptableObject.CreateInstance<StageTable>();
                using (var stream = File.Open(pathProj+ path7, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            StageData[] baseArr = new StageData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                string data4 = result.Tables[i].Rows[j][3].ToString();
                                string data5 = result.Tables[i].Rows[j][4].ToString();
                                string data6 = result.Tables[i].Rows[j][5].ToString();
                                string data7 = result.Tables[i].Rows[j][6].ToString();
                                string data8 = result.Tables[i].Rows[j][7].ToString();
                                string data9 = result.Tables[i].Rows[j][8].ToString();
                                string data10 = result.Tables[i].Rows[j][9].ToString();
                                string data11 = result.Tables[i].Rows[j][10].ToString();
                                string data12 = result.Tables[i].Rows[j][11].ToString();
                                string data13 = result.Tables[i].Rows[j][12].ToString();
                                string data14 = result.Tables[i].Rows[j][13].ToString();
                                string data15 = result.Tables[i].Rows[j][14].ToString();
                                string data16 = result.Tables[i].Rows[j][15].ToString();
                                string data17 = result.Tables[i].Rows[j][16].ToString();
                                string data18 = result.Tables[i].Rows[j][17].ToString();
                                string data19 = result.Tables[i].Rows[j][18].ToString();
                                string data20 = result.Tables[i].Rows[j][19].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                int data4Parse = Int32.Parse(data4);
                                int data5Parse = Int32.Parse(data5);
                                int data6Parse = Int32.Parse(data6);
                                int data7Parse = Int32.Parse(data7);
                                int data8Parse = Int32.Parse(data8);
                                int data9Parse = Int32.Parse(data9);
                                int data10Parse = Int32.Parse(data10);
                                int data11Parse = Int32.Parse(data11);
                                int data12Parse = Int32.Parse(data12);
                                int data13Parse = Int32.Parse(data13);
                                int data14Parse = Int32.Parse(data14);
                                int data15Parse = Int32.Parse(data15);
                                int data16Parse = Int32.Parse(data16);
                                int data17Parse = Int32.Parse(data17);
                                int data18Parse = Int32.Parse(data18);
                                int data19Parse = Int32.Parse(data19);
                                int data20Parse = Int32.Parse(data20);
                                
                                StageData newB = new StageData()
                                {
                                    mapType = data1Parse,
                                    stageType = data2Parse,
                                    minStage = data3Parse,
                                    maxStage = data4Parse,
                                    remainder = data5Parse,
                                    firstStageType = data6Parse,
                                    firstStageMonsterType = data7Parse,
                                    normalMonsterGroup = new int[]
                                    {
                                        data8Parse, data9Parse,data10Parse,data11Parse,data12Parse
                                    },
                                    eventMapGroup = data13Parse,
                                    bossMapGroup = data14Parse,
                                    normalPer = new int[]
                                    {
                                        data15Parse,data16Parse,data17Parse,data18Parse,data19Parse
                                    },
                                    eventPer = data20Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            charT.SetStageData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(charT, "Assets/Tables/StageTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            
            //8.Monster Level Table
            
            if (GUILayout.Button("Monster Level Table Export"))
            {
                MonsterLevelTable expT = ScriptableObject.CreateInstance<MonsterLevelTable>();
                using (var stream = File.Open(pathProj+ path8, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            MonsterLevelData[] baseArr = new MonsterLevelData[result.Tables[i].Rows.Count-1];
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
                                MonsterLevelData newB = new MonsterLevelData()
                                {
                                    stageLv= data1Parse, 
                                    monsterBaseStat = data2Parse, 
                                    nextStageLvCount = data3Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            expT.SetMonLevelData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(expT, "Assets/Tables/MonsterLevelTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
                
            }
            
            //9.Monster Table
            
            if (GUILayout.Button("Monster Table Export"))
            {
                MonsterTable monT = ScriptableObject.CreateInstance<MonsterTable>();
                using (var stream = File.Open(pathProj+ path9, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            MonsterData[] baseArr = new MonsterData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                string data4 = result.Tables[i].Rows[j][3].ToString();
                                string data5 = result.Tables[i].Rows[j][4].ToString();
                                string data6 = result.Tables[i].Rows[j][5].ToString();
                                string data7 = result.Tables[i].Rows[j][6].ToString();
                                string data8 = result.Tables[i].Rows[j][7].ToString();
                                string data9 = result.Tables[i].Rows[j][8].ToString();
                                string data10 = result.Tables[i].Rows[j][9].ToString();
                                string data11 = result.Tables[i].Rows[j][10].ToString();
                                string data12 = result.Tables[i].Rows[j][11].ToString();
                                string data13 = result.Tables[i].Rows[j][12].ToString();
                                string data14 = result.Tables[i].Rows[j][13].ToString();
                                string data15 = result.Tables[i].Rows[j][14].ToString();
                                string data16 = result.Tables[i].Rows[j][15].ToString();
                                string data17 = result.Tables[i].Rows[j][16].ToString();
                                string data18 = result.Tables[i].Rows[j][17].ToString();
                                string data19 = result.Tables[i].Rows[j][18].ToString();
                                string data20 = result.Tables[i].Rows[j][19].ToString();
                                string data21 = result.Tables[i].Rows[j][20].ToString();
                                string data22 = result.Tables[i].Rows[j][21].ToString();
                                string data23 = result.Tables[i].Rows[j][22].ToString();
                                string data24 = result.Tables[i].Rows[j][23].ToString();
                                string data25 = result.Tables[i].Rows[j][24].ToString();
                                string data26 = result.Tables[i].Rows[j][25].ToString();
                                string data27 = result.Tables[i].Rows[j][26].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                int data4Parse = Int32.Parse(data4);
                                int data5Parse = Int32.Parse(data5);
                                int data6Parse = Int32.Parse(data6);
                                int data7Parse = Int32.Parse(data7);
                                int data8Parse = Int32.Parse(data8);
                                int data9Parse = Int32.Parse(data9);
                                int data10Parse = Int32.Parse(data10);
                                int data11Parse = Int32.Parse(data11);
                                int data12Parse = Int32.Parse(data12);
                                int data13Parse = Int32.Parse(data13);
                                int data14Parse = Int32.Parse(data14);
                                int data15Parse = Int32.Parse(data15);
                                int data16Parse = Int32.Parse(data16);
                                int data17Parse = Int32.Parse(data17);
                                int data18Parse = Int32.Parse(data18);
                                int data19Parse = Int32.Parse(data19);
                                int data20Parse = Int32.Parse(data20);
                                int data21Parse = Int32.Parse(data21);
                                int data22Parse = Int32.Parse(data22);
                                int data23Parse = Int32.Parse(data23);
                                int data24Parse = Int32.Parse(data24);
                                int data25Parse = Int32.Parse(data25);
                                int data26Parse = Int32.Parse(data26);
                                int data27Parse = Int32.Parse(data27);

                               
                                MonsterData newB = new MonsterData()
                                {
                                    id = data1Parse,
                                    nameId = data2Parse,
                                    traitId1 = data3Parse,
                                    traitId2 = data4Parse,
                                    traitId3 = data5Parse,
                                    traitId4 = data6Parse,
                                    traitId5 = data7Parse,
                                    itemIdW = data8Parse,
                                    itemIdE = data9Parse,
                                    itemDropW = data10Parse,
                                    itemDropE = data11Parse,
                                    statData = new CharacterStat()
                                    {
                                        levelHp = data12Parse,
                                        levelAtk = data13Parse,
                                        levelpd = data14Parse,
                                        levelmd = data15Parse,
                                        hpRegen = data16Parse,
                                        hpAbsolve = data17Parse,
                                        crit = data18Parse,
                                        critDam = data19Parse,
                                        AttackCoolTime = data20Parse,
                                        accuracy = data21Parse,
                                        dodge = data22Parse,
                                        Range = data23Parse,
                                        MoveSpeed = data24Parse,
                                        atkIncrease = data25Parse,
                                        damageDecrease = data26Parse,
                                        AOEArea = data27Parse
                                    }
                                };
                                baseArr[j-1] = newB;
                            }

                            monT.SetMonData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(monT, "Assets/Tables/MonsterTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            
            
            //10.Monster Group Data
            
            if (GUILayout.Button("Trait Table Export"))
            {
                TraitTable traitT = ScriptableObject.CreateInstance<TraitTable>();
                using (var stream = File.Open(pathProj+ path4, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            TraitData[] baseArr = new TraitData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                string data4 = result.Tables[i].Rows[j][3].ToString();
                                string data5 = result.Tables[i].Rows[j][4].ToString();
                                string data6 = result.Tables[i].Rows[j][5].ToString();
                                string data7 = result.Tables[i].Rows[j][6].ToString();
                                string data8 = result.Tables[i].Rows[j][7].ToString();
                                string data9 = result.Tables[i].Rows[j][8].ToString();
                                string data10 = result.Tables[i].Rows[j][9].ToString();
                                string data11 = result.Tables[i].Rows[j][10].ToString();
                                string data12 = result.Tables[i].Rows[j][11].ToString();
                                string data13 = result.Tables[i].Rows[j][12].ToString();
                                string data14 = result.Tables[i].Rows[j][13].ToString();
                                string data15 = result.Tables[i].Rows[j][14].ToString();
                                string data16 = result.Tables[i].Rows[j][15].ToString();
                                string data17 = result.Tables[i].Rows[j][16].ToString();
                                string data18 = result.Tables[i].Rows[j][17].ToString();
                                string data19 = result.Tables[i].Rows[j][18].ToString();
                                string data20 = result.Tables[i].Rows[j][19].ToString();
                                string data21 = result.Tables[i].Rows[j][20].ToString();
                                string data22 = result.Tables[i].Rows[j][21].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                int data4Parse = Int32.Parse(data4);
                                int data5Parse = Int32.Parse(data5);
                                int data6Parse = Int32.Parse(data6);
                                int data7Parse = Int32.Parse(data7);
                                int data8Parse = Int32.Parse(data8);
                                int data9Parse = Int32.Parse(data9);
                                int data10Parse = Int32.Parse(data10);
                                int data11Parse = Int32.Parse(data11);
                                int data12Parse = Int32.Parse(data12);
                                int data13Parse = Int32.Parse(data13);
                                int data14Parse = Int32.Parse(data14);
                                int data15Parse = Int32.Parse(data15);
                                int data16Parse = Int32.Parse(data16);
                                int data17Parse = Int32.Parse(data17);
                                int data18Parse = Int32.Parse(data18);
                                int data19Parse = Int32.Parse(data19);
                                int data20Parse = Int32.Parse(data20);
                                int data21Parse = Int32.Parse(data21);
                                int data22Parse = Int32.Parse(data22);

                                List<TraitOptionData> traitOps = new List<TraitOptionData>();

                                if (data6Parse != 0)
                                {
                                    traitOps.Add(new TraitOptionData(){battleOptionType = data6Parse, value1 = data7Parse, value2 = data8Parse});
                                }
                                if (data9Parse != 0)
                                {
                                    traitOps.Add(new TraitOptionData(){battleOptionType = data9Parse, value1 = data10Parse, value2 = data11Parse});
                                }
                                if (data12Parse != 0)
                                {
                                    traitOps.Add(new TraitOptionData(){battleOptionType = data12Parse, value1 = data13Parse, value2 = data14Parse});
                                }
                                if (data15Parse != 0)
                                {
                                    traitOps.Add(new TraitOptionData(){battleOptionType = data15Parse, value1 = data16Parse, value2 = data17Parse});
                                }
                                

                                var arr = traitOps.ToArray();

                                TraitData data = new TraitData()
                                {
                                    id = data1Parse,
                                    traitType = data2Parse,
                                    traitCondition = data3Parse,
                                    traitVive = data4Parse == 1,
                                    type = data5Parse,
                                    optionDatas = arr,
                                    conditionType = data18Parse,
                                    conditionValue = data19Parse,
                                    rarity = data20Parse,
                                    sort = data21Parse,
                                    group = data22Parse
                                };
                                baseArr[j-1] = data;
                            }

                            traitT.SetTraitData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(traitT, "Assets/Tables/TraitTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            
            
            
            
            //10.Monster Group Data
            
            if (GUILayout.Button("Monster Group Table Export"))
            {
                MonsterGroupTable monGT = ScriptableObject.CreateInstance<MonsterGroupTable>();
                using (var stream = File.Open(pathProj+ path10, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();

                        
                        //시트 개수만큼 반복
                        for (int i = 0; i < result.Tables.Count; i++)
                        {
                            MonsterGroupData[] baseArr = new MonsterGroupData[result.Tables[i].Rows.Count-1];
                            //해당 시트의 행데이터(한줄씩)로 반복
                            for (int j = 1; j < result.Tables[i].Rows.Count; j++)
                            {
                                //해당행의 0,1,2 셀의 데이터 파싱
                                string data1 = result.Tables[i].Rows[j][0].ToString();
                                string data2 = result.Tables[i].Rows[j][1].ToString();
                                string data3 = result.Tables[i].Rows[j][2].ToString();
                                string data4 = result.Tables[i].Rows[j][3].ToString();
                                string data5 = result.Tables[i].Rows[j][4].ToString();
                                string data6 = result.Tables[i].Rows[j][5].ToString();
                                string data7 = result.Tables[i].Rows[j][6].ToString();
                                string data8 = result.Tables[i].Rows[j][7].ToString();
                                string data9 = result.Tables[i].Rows[j][8].ToString();
                                string data10 = result.Tables[i].Rows[j][9].ToString();
                                string data11 = result.Tables[i].Rows[j][10].ToString();
                                string data12 = result.Tables[i].Rows[j][11].ToString();
                                string data13 = result.Tables[i].Rows[j][12].ToString();
                                string data14 = result.Tables[i].Rows[j][13].ToString();
                                string data15 = result.Tables[i].Rows[j][14].ToString();
                                string data16 = result.Tables[i].Rows[j][15].ToString();
                                string data17 = result.Tables[i].Rows[j][16].ToString();
                                string data18 = result.Tables[i].Rows[j][17].ToString();
                                string data19 = result.Tables[i].Rows[j][18].ToString();
                                string data20 = result.Tables[i].Rows[j][19].ToString();
                                string data21 = result.Tables[i].Rows[j][20].ToString();
                                string data22 = result.Tables[i].Rows[j][21].ToString();
                                
                                int data1Parse = Int32.Parse(data1);
                                int data2Parse = Int32.Parse(data2);
                                int data3Parse = Int32.Parse(data3);
                                int data4Parse = Int32.Parse(data4);
                                int data5Parse = Int32.Parse(data5);
                                int data6Parse = Int32.Parse(data6);
                                int data7Parse = Int32.Parse(data7);
                                int data8Parse = Int32.Parse(data8);
                                int data9Parse = Int32.Parse(data9);
                                int data10Parse = Int32.Parse(data10);
                                int data11Parse = Int32.Parse(data11);
                                int data12Parse = Int32.Parse(data12);
                                int data13Parse = Int32.Parse(data13);
                                int data14Parse = Int32.Parse(data14);
                                int data15Parse = Int32.Parse(data15);
                                int data16Parse = Int32.Parse(data16);
                                int data17Parse = Int32.Parse(data17);
                                int data18Parse = Int32.Parse(data18);
                                int data19Parse = Int32.Parse(data19);
                                int data20Parse = Int32.Parse(data20);
                                int data21Parse = Int32.Parse(data21);
                                int data22Parse = Int32.Parse(data22);

                                List<MonsterParty> parties = new List<MonsterParty>();

                                if (data3Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data2Parse, monsterCount = data3Parse});
                                }
                                if (data5Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data4Parse, monsterCount = data5Parse});
                                }
                                if (data7Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data6Parse, monsterCount = data7Parse});
                                }
                                if (data9Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data8Parse, monsterCount = data9Parse});
                                }
                                if (data11Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data10Parse, monsterCount = data11Parse});
                                }
                                if (data13Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data12Parse, monsterCount = data13Parse});
                                }
                                if (data15Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data14Parse, monsterCount = data15Parse});
                                }
                                if (data17Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data16Parse, monsterCount = data17Parse});
                                }
                                if (data19Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data18Parse, monsterCount = data19Parse});
                                }
                                if (data21Parse != 0)
                                {
                                    parties.Add(new MonsterParty(){monsterId = data20Parse, monsterCount = data21Parse});
                                }

                                var arr = parties.ToArray();

                                MonsterGroupData newB = new MonsterGroupData()
                                {
                                    groupId = data1Parse,
                                    parties = arr,
                                    reward = data22Parse
                                };
                                baseArr[j-1] = newB;
                            }

                            monGT.SetMonGroupData(baseArr);
                        }
                        
                        AssetDatabase.CreateAsset(monGT, "Assets/Tables/MonsterGroupTable.asset");
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            
        }
    }
}