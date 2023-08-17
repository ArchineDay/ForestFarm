using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IndieFarm.Tool
{
    public class ToolShovel:ITool
    {
        public Item Item { get; set; }
        public string Name { get; set; }="shovel";

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x,toolData.CellPos.y] == null;
        }

        public void Use(ToolData toolData)
        {
            toolData.SoilTilemap.SetTile(toolData.CellPos, toolData.Pen);
            toolData.ShowGrid[toolData.CellPos.x,toolData.CellPos.y] = new SoliData();
            AudioController.Get.SfxShovelDig.Play();
        }
    }
}