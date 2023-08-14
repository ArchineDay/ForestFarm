using QFramework;

namespace IndieFarm.Tool
{
    public class ToolSeedCabbage:ITool
    {
        public string Name { get; set; }
        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant != true &&
                   Global.CabbageSeedCount > 0;
        }

        public void Use(ToolData toolData)
        {
            Global.CabbageSeedCount.Value--;
            var plantGameObj = ResController.Instance.plantCabbagePrefab
                .Instantiate()
                .Position(toolData.GridCenterPos);
            var plant = plantGameObj.GetComponent<PlantCabbage>();
            plant.XCell = toolData.CellPos.x;
            plant.YCell = toolData.CellPos.y;

            //添加到 Plants 数组
            PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] = plant;
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = true;

            AudioController.Get.SfxSeed.Play();
        }
    }
}