using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm
{
    public partial class Player : ViewController
    {
        public Grid Grid;
        public Tilemap Tilemap;

        void Start()
        {
            // Code Here
        }

        private void Update()
        {
            var cellPosition = Grid.WorldToCell(transform.position);
            var grid = FindObjectOfType<GridController>().ShowGrid;
            
            var tileWorldPos = Grid.CellToWorld(cellPosition);
            var cellSize = Grid.cellSize;
            tileWorldPos.x+=cellSize.x/2;
            tileWorldPos.y+=cellSize.y/2; 
            
            if (cellPosition.x >= 0 && cellPosition.x < 10 && cellPosition.y >= 0 && cellPosition.y < 10)
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
                if (cellPosition.x >= 0 && cellPosition.x < 10 && cellPosition.y >= 0 && cellPosition.y < 10)
                {
                    //没耕地
                    if (grid[cellPosition.x, cellPosition.y] == null)
                    {
                        //开垦
                        Tilemap.SetTile(cellPosition, FindObjectOfType<GridController>().Pen);
                        grid[cellPosition.x, cellPosition.y] = new SoliData();

                    }
                    //耕地了
                    else if(grid[cellPosition.x, cellPosition.y].HasPlant!=true)
                    {
                        //放种子
                        ResController.Instance.seedPrefab
                            .Instantiate()
                            .Position(tileWorldPos);
                        
                        grid[cellPosition.x, cellPosition.y].HasPlant = true;
                    }
                    
                }
            }
            
            
            if (Input.GetMouseButtonDown(1))
            {
                if (cellPosition.x >= 0 && cellPosition.x < 10 && cellPosition.y >= 0 && cellPosition.y < 10)
                {
                    if (grid[cellPosition.x, cellPosition.y] != null)
                    {
                        Tilemap.SetTile(cellPosition, null);
                        grid[cellPosition.x, cellPosition.y] = null;
                    }
                }
            }
        }
    }
}