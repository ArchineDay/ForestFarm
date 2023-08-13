using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm.Tool
{
    public class ToolSeed : ITool
    {
        public string Name { get; set; } = "seed";

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant != true &&
                   Global.FruitSeedCount > 0;
        }

        public void Use(ToolData toolData)
        {
            Global.FruitSeedCount.Value--;
            var plantGameObj = ResController.Instance.plantPrefab
                .Instantiate()
                .Position(toolData.GridCenterPos);
            var plant = plantGameObj.GetComponent<Plant>();
            plant.XCell = toolData.CellPos.x;
            plant.YCell = toolData.CellPos.y;

            //添加到 Plants 数组
            PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] = plant;
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = true;

            AudioController.Get.SfxSeed.Play();
        }
    }
}