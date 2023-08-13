using QFramework;
using UnityEngine;

namespace IndieFarm.Tool
{
    public class ToolWateringScan : ITool
    {
        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].Watered != true &&
                   Global.CurrentTool.Value == Constant.TOOL_WATERING_SCAN;
        }

        public void Use(ToolData toolData)
        {
            ResController.Instance.waterPrefab
                .Instantiate()
                .Position(toolData.GridCenterPos);

            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].Watered = true;
            AudioController.Get.SfxWater.Play();
            Debug.Log("grid" + toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].PlantState);
        }
    }
}