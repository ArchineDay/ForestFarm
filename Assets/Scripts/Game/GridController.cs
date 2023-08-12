using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm
{
    public partial class GridController : ViewController
    {
        private EasyGrid<SoliData> mShowGrid = new EasyGrid<SoliData>(10, 10);
        
        public EasyGrid<SoliData> ShowGrid=>mShowGrid;

        public TileBase Pen;
        public TileBase PlantablePen;

        void Start()
        {
            mShowGrid[0, 0] = new SoliData();
            mShowGrid[2, 2] = new SoliData();

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Ground.SetTile(new Vector3Int(i,j),PlantablePen);
                }
            }
            
            mShowGrid.ForEach((x, y, data) =>
            {
                if (data != null)
                {
                    //用的pen来设置
                    Soil.SetTile(new Vector3Int(x, y), Pen);
                }
            });
         
        }
    }
}