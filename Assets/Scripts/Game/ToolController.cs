using IndieFarm.Tool;
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
            mTilemap = mGridController.Soil;

            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }
        
        private ToolData mTooldata = new ToolData();

        bool ToolInRange(Vector3Int mouseCellPos, Vector3Int playerPos, int range)
        {
            // 计算cellPos与playerPos之间的水平和垂直距离
            float deltaX = Mathf.Abs(mouseCellPos.x - playerPos.x);
            float deltaY = Mathf.Abs(mouseCellPos.y - playerPos.y);
            return deltaX <= range && deltaY <= range && (deltaX + deltaY) >= 0;
        }
        private void Update()
        {
            mSprite.enabled = false;
            var playerPos = mGrid.WorldToCell(Global.Player.transform.Position());

            var worldMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            //图标位置在格子右下角
            Icon.Position(worldMousePos.x + 0.5f, worldMousePos.y - 0.5f);
            var mouseCellPos = mGrid.WorldToCell(worldMousePos);
            
            // 检查cellPos是否在playerPos周围（相邻或对角线位置）
            if (ToolInRange(mouseCellPos, playerPos,Global.CurrentTool.Value.Range))
            {
                if (mouseCellPos is { x: >= 0 and < 10, y: >= 0 and < 10 })
                {
                    ShowSelectCenter(mouseCellPos);
                    mTooldata.GridCenterPos = ShowSelectCenter(mouseCellPos);

                    mTooldata.ShowGrid = mShowGrid;
                    mTooldata.CellPos = mouseCellPos;
                    mTooldata.SoilTilemap = mTilemap;
                    mTooldata.Pen = mGridController.Pen;
                    //使用工具
                    if (Global.CurrentTool.Value.Selectable(mTooldata))
                    {
                        if (Input.GetMouseButton(0))
                        {
                            Global.CurrentTool.Value.Use(mTooldata);
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