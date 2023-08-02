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

        private void Start()
        {
            Global.Days.Register(day =>
            {
                var soilDatas = FindObjectOfType<GridController>().ShowGrid;
                //天数变更小植物成熟
                // var smallPlants = SceneManager.GetActiveScene()
                //     .GetRootGameObjects()
                //     .Where(gameObj => gameObj.name.StartsWith("SmallPlant"));
                // foreach (var smallPlant in smallPlants)
                // {
                //     var tilePos = Grid.WorldToCell(smallPlant.transform.position);
                //
                //     var tileData = soilDatas[tilePos.x, tilePos.y];
                //
                //     if (tileData is not { Watered: true }) continue; //跳过本次循环
                //     ResController.Instance.ripePrefab.Instantiate()
                //         .Position(smallPlant.transform.position);
                //
                //     smallPlant.DestroySelf();
                //     //tileData.RipeState = true;
                // }

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
            GUILayout.Label("浇水: E");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("下一天: F");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label($"当前工具: {Constant.DisplayName(Global.CurrentTool)}");
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1] 手 [2] 铁锹 [3] 种子");
        }

        private void Update()
        {
            var cellPosition = Grid.WorldToCell(transform.position);
            var grid = FindObjectOfType<GridController>().ShowGrid;

            //var tileWorldPos = transform.position;角色位置在左下角
            var tileWorldPos = Grid.CellToWorld(cellPosition);
            var cellSize = Grid.cellSize;
            tileWorldPos.x += cellSize.x / 2;
            tileWorldPos.y += cellSize.y / 2;

            if (cellPosition is { x: >= 0 and < 10, y: >= 0 and < 10 })
            {
                if (Global.CurrentTool == Constant.TOOL_SHOVEL && grid[cellPosition.x, cellPosition.y] == null)
                {
                    TileSelectController.Instance.Position(tileWorldPos);
                    TileSelectController.Instance.Show();
                }
                else if (Global.CurrentTool == Constant.TOOL_SEED &&
                         grid[cellPosition.x, cellPosition.y] != null &&
                         grid[cellPosition.x, cellPosition.y].HasPlant != true)
                {
                    TileSelectController.Instance.Position(tileWorldPos);
                    TileSelectController.Instance.Show();
                }
                else
                {
                    TileSelectController.Instance.Hide();
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (cellPosition is { x: >= 0 and < 10, y: >= 0 and < 10 })
                {
                    //没耕地
                    if (grid[cellPosition.x, cellPosition.y] == null &&
                        Global.CurrentTool.Value == Constant.TOOL_SHOVEL)
                    {
                        //开垦
                        Tilemap.SetTile(cellPosition, FindObjectOfType<GridController>().Pen);
                        grid[cellPosition.x, cellPosition.y] = new SoliData();
                    }

                    //耕地了
                    else if (grid[cellPosition.x, cellPosition.y] != null&&
                             grid[cellPosition.x, cellPosition.y].HasPlant != true &&
                             Global.CurrentTool.Value == Constant.TOOL_SEED)
                    {
                        //放种子
                        var plantGameObj = ResController.Instance.plantPrefab
                            .Instantiate()
                            .Position(tileWorldPos);
                        var plant = plantGameObj.GetComponent<Plant>();
                        plant.XCell = cellPosition.x;
                        plant.YCell = cellPosition.y;

                        //添加到 Plants 数组
                        PlantController.Instance.Plants[cellPosition.x, cellPosition.y] = plant;

                        grid[cellPosition.x, cellPosition.y].HasPlant = true;
                    }

                    return;
                    if (grid[cellPosition.x, cellPosition.y].HasPlant)
                    {
                        if (grid[cellPosition.x, cellPosition.y].PlantState == PlantStates.Ripe)
                        {
                            //摘果子，植物消失
                            Destroy(PlantController.Instance.Plants[cellPosition.x, cellPosition.y].gameObject);
                            grid[cellPosition.x, cellPosition.y].HasPlant = false;
                            PlantController.Instance.Plants[cellPosition.x, cellPosition.y].SetState(PlantStates.Seed);

                            //果子+1
                            Global.FruitCount.Value++;
                        }
                    }
                }
            }


            if (Input.GetMouseButtonDown(1))
            {
                if (cellPosition is { x: >= 0 and < 10, y: >= 0 and < 10 })
                {
                    if (grid[cellPosition.x, cellPosition.y] != null)
                    {
                        Tilemap.SetTile(cellPosition, null);
                        grid[cellPosition.x, cellPosition.y] = null;
                    }
                }
            }

            //浇水
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cellPosition is not { x: >= 0 and < 10, y: >= 0 and < 10 }) return;
                if (grid[cellPosition.x, cellPosition.y] == null) return;
                if (grid[cellPosition.x, cellPosition.y].Watered == true) return;
                ResController.Instance.waterPrefab
                    .Instantiate()
                    .Position(tileWorldPos);

                grid[cellPosition.x, cellPosition.y].Watered = true;
            }

            //天数变更
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;
            }

            //结束游戏
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePass");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Global.CurrentTool.Value = Constant.TOOL_HAND;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Global.CurrentTool.Value = Constant.TOOL_SHOVEL;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Global.CurrentTool.Value = Constant.TOOL_SEED;
            }
        }
    }
}