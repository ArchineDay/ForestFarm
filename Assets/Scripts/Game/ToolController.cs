using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm
{
    public partial class ToolController : ViewController
    {
        private Tilemap mTilemap;
        private GridController mGridController;
        private Grid mGrid;
        private Camera mMainCamera;
        private SpriteRenderer mSprite;
        private EasyGrid<SoliData> mShowGrid;

        private void Awake()
        {
            Global.MouseTool = this;
        }

        private void OnDestroy()
        {
            Global.MouseTool = null;
        }

        void Start()
        {
            mGridController = FindObjectOfType<GridController>();
            mShowGrid = mGridController.ShowGrid;
            mGrid = mGridController.GetComponent<Grid>();
            mTilemap = mGridController.Tilemap;

            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }

        private void Update()
        {
            mSprite.enabled = false;
            var playerPos = mGrid.WorldToCell(Global.Player.transform.Position());

            var worldMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            //图标位置在格子右下角
            Icon.Position(worldMousePos.x + 0.5f, worldMousePos.y - 0.5f);
            var cellPos = mGrid.WorldToCell(worldMousePos);

            // 计算cellPos与playerPos之间的水平和垂直距离
            float deltaX = Mathf.Abs(cellPos.x - playerPos.x);
            float deltaY = Mathf.Abs(cellPos.y - playerPos.y);

            // 检查cellPos是否在playerPos周围（相邻或对角线位置）
            if (deltaX <= 1 && deltaY <= 1 && (deltaX + deltaY) >=0)
            {
                if (cellPos is { x: >= 0 and < 10, y: >= 0 and < 10 })
                {
                    ShowSelectCenter(cellPos);
                    var gridCenterPos = ShowSelectCenter(cellPos);

                    //开垦
                    if (Global.CurrentTool == Constant.TOOL_SHOVEL && mShowGrid[cellPos.x, cellPos.y] == null)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            mTilemap.SetTile(cellPos, mGridController.Pen);
                            mShowGrid[cellPos.x, cellPos.y] = new SoliData();
                            AudioController.Get.SfxShovelDig.Play();
                        }
                    }

                    //放种子
                    else if (Global.CurrentTool == Constant.TOOL_SEED &&
                             mShowGrid[cellPos.x, cellPos.y] != null &&
                             mShowGrid[cellPos.x, cellPos.y].HasPlant != true)
                    {
                        if (Global.FruitSeedCount > 0)
                        {
                            if (Input.GetMouseButton(0))
                            {
                                Global.FruitSeedCount.Value--;
                                var plantGameObj = ResController.Instance.plantPrefab
                                    .Instantiate()
                                    .Position(gridCenterPos);
                                var plant = plantGameObj.GetComponent<Plant>();
                                plant.XCell = cellPos.x;
                                plant.YCell = cellPos.y;

                                //添加到 Plants 数组
                                PlantController.Instance.Plants[cellPos.x, cellPos.y] = plant;
                                mShowGrid[cellPos.x, cellPos.y].HasPlant = true;

                                AudioController.Get.SfxSeed.Play();
                            }
                        }
                    }
                    //放萝卜种子
                    else if (Global.CurrentTool == Constant.TOOL_SEED_RADISH &&
                             mShowGrid[cellPos.x, cellPos.y] != null &&
                             mShowGrid[cellPos.x, cellPos.y].HasPlant != true)
                    {
                        if (Global.RadishSeedCount > 0)
                        {
                            if (Input.GetMouseButton(0))
                            {
                                Global.RadishSeedCount.Value--;
                                var plantGameObj = ResController.Instance.plantRadishPrefab
                                    .Instantiate()
                                    .Position(gridCenterPos);
                                var plant = plantGameObj.GetComponent<PlantRadish>();
                                plant.XCell = cellPos.x;
                                plant.YCell = cellPos.y;

                                //添加到 Plants 数组
                                PlantController.Instance.Plants[cellPos.x, cellPos.y] = plant;
                                mShowGrid[cellPos.x, cellPos.y].HasPlant = true;

                                AudioController.Get.SfxSeed.Play();
                            }
                        }
                    }
                    //浇水
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                             mShowGrid[cellPos.x, cellPos.y].Watered != true &&
                             Global.CurrentTool.Value == Constant.TOOL_WATERING_SCAN)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            ResController.Instance.waterPrefab
                                .Instantiate()
                                .Position(gridCenterPos);

                            mShowGrid[cellPos.x, cellPos.y].Watered = true;
                            AudioController.Get.SfxWater.Play();
                            Debug.Log("grid" + mShowGrid[cellPos.x, cellPos.y].PlantState);
                        }
                    }
                    //收割
                    else if (mShowGrid[cellPos.x, cellPos.y] != null &&
                             mShowGrid[cellPos.x, cellPos.y].HasPlant &&
                             mShowGrid[cellPos.x, cellPos.y].PlantState == PlantStates.Ripe &&
                             Global.CurrentTool.Value == Constant.TOOL_HAND)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            //摘果子，植物消失
                            Destroy(PlantController.Instance.Plants[cellPos.x, cellPos.y].GameObject);
                            mShowGrid[cellPos.x, cellPos.y].HasPlant = false;
                            //重置土地的状态
                            mShowGrid[cellPos.x, cellPos.y].PlantState = PlantStates.Seed;

                            Global.OnPlantHarvest.Trigger(PlantController.Instance.Plants[cellPos.x, cellPos.y]);


                            if (PlantController.Instance.Plants[cellPos.x, cellPos.y] as Plant)
                            {
                                //果子+1
                                Global.FruitCount.Value++;
                            }

                            if (PlantController.Instance.Plants[cellPos.x, cellPos.y] as PlantRadish)
                            {
                                //萝卜+1
                                Global.RadishCount.Value++;
                            }

                            AudioController.Get.SfxHarvest.Play();
                        }
                    }
                }
                else
                {
                    mSprite.enabled = false;
                }
            }
        }

        Vector3 ShowSelectCenter(Vector3Int cellPos)
        {
            //格子左下角原点
            var gridOriginPos = mGrid.CellToWorld(cellPos);
            //格子中心点
            var gridCenterPos = gridOriginPos + mGrid.cellSize * 0.5f;
            transform.Position(gridCenterPos.x, gridCenterPos.y);
            mSprite.enabled = true;
            return gridCenterPos;
        }
    }
}