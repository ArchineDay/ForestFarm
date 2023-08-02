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

        void Start()
        {
            mShowGrid[0, 0] = new SoliData();
            mShowGrid[2, 2] = new SoliData();

            mShowGrid.ForEach((x, y, data) =>
            {
                if (data != null)
                {
                    //用的pen来设置
                    Tilemap.SetTile(new Vector3Int(x, y), Pen);
                }
            });
            // for (int i = 0; i < mShowGrid.Width; i++)
            // {
            // 	for (int j = 0; j < mShowGrid.Height; j++)
            // 	{
            // 		bool show = mShowGrid[i, j];
            // 		if (show)
            // 		{
            // 			Vector3Int position = new Vector3Int(i, j, 0);
            // 			Tilemap.SetTile(position, Pen);
            // 		}
            // 	}
            // }
        }
    }
}