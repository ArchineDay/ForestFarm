using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class MouseController : ViewController
    {
        private Grid mGrid;
        private Camera mMainCamera;
        private SpriteRenderer mSprite;

        void Start()
        {
            mGrid = FindObjectOfType<GridController>().GetComponent<Grid>();
            mMainCamera = Camera.main;
            mSprite = GetComponent<SpriteRenderer>();
            mSprite.enabled = false;
        }

        private void Update()
        {
            var playerPos = mGrid.WorldToCell(Global.Player.transform.Position());

            var worldMousePos = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            //var cellPos = mGrid.WorldToCell(worldMousePos);
            Vector3 cellPos = mGrid.WorldToCell(worldMousePos);
            
            // 计算cellPos与playerPos之间的水平和垂直距离
            int deltaX = (int)Mathf.Abs(cellPos.x - playerPos.x);
            int deltaY = (int)Mathf.Abs(cellPos.y - playerPos.y);
            // if (cellPos.x - playerPos.x == -1 && cellPos.y - playerPos.y == 1 ||
            //     cellPos.x - playerPos.x == -1 && cellPos.y - playerPos.y == 0 ||
            //     cellPos.x - playerPos.x == -1 && cellPos.y - playerPos.y == -1 ||
            //     cellPos.x - playerPos.x == 0 && cellPos.y - playerPos.y == 1 ||
            //     cellPos.x - playerPos.x == 0 && cellPos.y - playerPos.y == -1 ||
            //     cellPos.x - playerPos.x == 1 && cellPos.y - playerPos.y == 1 ||
            //     cellPos.x - playerPos.x == 1 && cellPos.y - playerPos.y == 0 ||
            //     cellPos.x - playerPos.x == 1 && cellPos.y - playerPos.y == -1)
            // {
            //     //格子左下角原点
            //     //var gridOriginPos = mGrid.CellToWorld(cellPos);
            //     //格子中心点
            //     //gridOriginPos += mGrid.cellSize * 0.5f;
            //     transform.Position(gridOriginPos.x, gridOriginPos.y);
            // }

            // 检查cellPos是否在playerPos周围（相邻或对角线位置）
            if (deltaX <= 1 && deltaY <= 1 && (deltaX + deltaY) > 0)
            {
                // cellPos在playerPos周围
                cellPos += mGrid.cellSize * 0.5f;
                transform.Position(cellPos.x, cellPos.y);
                mSprite.enabled = true;
            }
            else
            {
                mSprite.enabled = false;
            }
        }
    }
}