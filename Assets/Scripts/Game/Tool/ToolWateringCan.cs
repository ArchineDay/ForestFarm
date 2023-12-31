using QFramework;
using UnityEngine;

namespace IndieFarm.Tool
{
    public class ToolWateringCan : ITool
    {
        public Item Item { get; set; }
        public string Name { get; set; } ="watering_can";

        public int Range=>Global.WateringCanRange1UnLock?2:1;

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].Watered != true;
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

        public int EnergyCost { get; } = 3;
    }
}