using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace IndieFarm.Tool
{
    public interface ITool
    {
        Item Item { get; set; }
        string Name { get; set; }
        int Range { get; }
        bool Selectable(ToolData toolData);

        void Use(ToolData toolData);
        
        int EnergyCost { get; }
    }

    public class ToolData
    {
        public EasyGrid<SoliData> ShowGrid { get; set; }

        public Vector3Int CellPos { get; set; }

        public Tilemap SoilTilemap { get; set; }

        public TileBase Pen { get; set; }

        public Vector3 GridCenterPos { get; set; }
    }
}