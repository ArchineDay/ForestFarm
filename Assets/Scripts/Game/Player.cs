using System;
using System.Linq;
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
            GUILayout.Label("天数" + Global.Days.Value);
            GUILayout.EndHorizontal();
        }

        private void Update()
        {
            //天数变更
            if (Input.GetKeyDown(KeyCode.F))
            {
                Global.Days.Value++;
            }

            //
            var cellPosition = Grid.WorldToCell(transform.position);
            var grid = FindObjectOfType<GridController>().ShowGrid;

            //var tileWorldPos = transform.position;角色位置在左下角
            var tileWorldPos = Grid.CellToWorld(cellPosition);
            var cellSize = Grid.cellSize;
            tileWorldPos.x += cellSize.x / 2;
            tileWorldPos.y += cellSize.y / 2;

            if (cellPosition is { x: >= 0 and < 10, y: >= 0 and < 10 })
            {
                TileSelectController.Instance.Position(tileWorldPos);
                TileSelectController.Instance.Show();
            }
            else
            {
                TileSelectController.Instance.Hide();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (cellPosition is { x: >= 0 and < 10, y: >= 0 and < 10 })
                {
                    //没耕地
                    if (grid[cellPosition.x, cellPosition.y] == null)
                    {
                        //开垦
                        Tilemap.SetTile(cellPosition, FindObjectOfType<GridController>().Pen);
                        grid[cellPosition.x, cellPosition.y] = new SoliData();
                    }
                    //耕地了
                    else if (grid[cellPosition.x, cellPosition.y].HasPlant != true)
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
            if (!Input.GetKeyDown(KeyCode.E)) return;
            //if (cellPosition.x >= 0 && cellPosition.x < 10 && cellPosition.y >= 0 && cellPosition.y < 10)
            if (cellPosition is not { x: >= 0 and < 10, y: >= 0 and < 10 }) return;
            if (grid[cellPosition.x, cellPosition.y] == null) return;
            if (grid[cellPosition.x, cellPosition.y].Watered == true) return;
            ResController.Instance.waterPrefab
                .Instantiate()
                .Position(tileWorldPos);

            grid[cellPosition.x, cellPosition.y].Watered = true;
        }
    }
}