using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace IndieFarm.Tool
{
    public class ToolShovel:ITool
    {
        public bool Selectable(ToolData toolData)
        {
            return Global.CurrentTool == Constant.TOOL_SHOVEL && toolData.ShowGrid[toolData.CellPos.x,toolData.CellPos.y] == null;
        }

        public void Use(ToolData toolData)
        {
            toolData.SoilTilemap.SetTile(toolData.CellPos, toolData.Pen);
            toolData.ShowGrid[toolData.CellPos.x,toolData.CellPos.y] = new SoliData();
            AudioController.Get.SfxShovelDig.Play();
        }
    }
}