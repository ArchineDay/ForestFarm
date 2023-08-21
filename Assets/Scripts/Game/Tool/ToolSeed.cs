using QFramework;
using UnityEngine.Tilemaps;

namespace IndieFarm.Tool
{
    public class ToolSeed : ITool
    {
        public Item Item { get; set; }
        public string Name { get; set; } = "seed";

        //=>表示 get { return "seed"; }
        public int Range => 1;

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant != true &&
                   Item.Count.Value > 0;
        }

        public void Use(ToolData toolData)
        {
            //Global.FruitSeedCount.Value--;
            Item.Count.Value--;

            var plantGameObj = ResController.Instance.LoadPrefab(Item.PlantPrefabName)
                .Instantiate()
                .Position(toolData.GridCenterPos);
            var plant = plantGameObj.GetComponent<IPlant>();
            plant.XCell = toolData.CellPos.x;
            plant.YCell = toolData.CellPos.y;

            //添加到 Plants 数组
            PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] = plant;
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = true;

            AudioController.Get.SfxSeed.Play();
        }
    }
}