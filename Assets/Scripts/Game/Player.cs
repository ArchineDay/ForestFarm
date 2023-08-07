using System;
using System.Linq;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace IndieFarm
{
    public partial class Player : ViewController
    {
        public Grid Grid;
        public Tilemap Tilemap;

        private void Awake()
        {
            Global.Player = this;
        }

        private void Start()
        {
            Global.Days.Register(day =>
            {
                Global.RipeAndHarvestCountInCurrentDay.Value = 0;
                Global.HarvestCountInCurrentDay.Value = 0;
                var soilDatas = FindObjectOfType<GridController>().ShowGrid;
                //天数变更小植物成熟
                PlantController.Instance.Plants.ForEach((x, y, plant) =>
                {
                    if (plant)
                    {
                        if (plant.State == PlantStates.Seed)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                //plant切换到small状态
                                plant.SetState(PlantStates.Small);
                            }
                        }
                        else if (plant.State == PlantStates.Small)
                        {
                            if (soilDatas[x, y].Watered)
                            {
                                plant.SetState(PlantStates.Ripe);
                            }
                        }
                    }
                });


                //清空水的状态
                soilDatas.ForEach(soilData =>
                {
                    if (soilData != null)
                    {
                        soilData.Watered = false;
                    }
                });
                //清空水的对象
                foreach (var water in SceneManager.GetActiveScene()
                             .GetRootGameObjects()
                             .Where(gameObj => gameObj.name.StartsWith("Water")))
                {
                    water.DestroySelf();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGUI()
        {
            //显示天数
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10); //默认是verticle垂直方向，是行间距
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("天数:" + Global.Days.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("果子:" + Global.FruitCount.Value);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("下一天: F");
            GUILayout.EndHorizontal();
            
            //GUILayout.Label($"当前工具: {Constant.DisplayName(Global.CurrentTool)}");

            GUILayout.FlexibleSpace();

            //GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1] 手 [2] 铁锹 [3] 种子 [4] 花洒");
        }

        private void Update()
        {
            //天数变更
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioController.Get.SfxNextDay.Play();
                Global.Days.Value++;
            }

            //结束游戏
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePass");
            }
        }

        private void OnDestroy()
        {
            Global.Player = null;
        }
    }
}