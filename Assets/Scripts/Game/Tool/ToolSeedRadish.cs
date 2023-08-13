using QFramework;

namespace IndieFarm.Tool
{
    public class ToolSeedRadish : ITool
    {
        public string Name { get; set; } = "seed_radish";

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant != true &&
                   Global.RadishSeedCount > 0;
        }

        public void Use(ToolData toolData)
        {
            Global.RadishSeedCount.Value--;
            var plantGameObj = ResController.Instance.plantRadishPrefab
                .Instantiate()
                .Position(toolData.GridCenterPos);
            var plant = plantGameObj.GetComponent<PlantRadish>();
            plant.XCell = toolData.CellPos.x;
            plant.YCell = toolData.CellPos.y;

            //添加到 Plants 数组
            PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] = plant;
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = true;

            AudioController.Get.SfxSeed.Play();
        }
    }
}