using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm
{
    public partial class GridController : ViewController
    {
        private EasyGrid<SoliData> mShowGrid = new EasyGrid<SoliData>(5, 4);
        
        public EasyGrid<SoliData> ShowGrid=>mShowGrid;

        public TileBase Pen;
        public TileBase PlantablePen;

        void Start()
        {
            mShowGrid[0, 0] = new SoliData();
            mShowGrid[2, 2] = new SoliData();

            for (var i = 0; i < ShowGrid.Width; i++)
            {
                for (var j = 0; j < ShowGrid.Height; j++)
                {
                    Ground.SetTile(new Vector3Int(i,j),PlantablePen);
                }
            }
            
            mShowGrid.ForEach((x, y, data) =>
            {
                if (data != null)
                {
                    //用pen来设置
                    Soil.SetTile(new Vector3Int(x, y), Pen);
                }
            });
         
        }
    }
}