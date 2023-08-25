using System.Linq;
using QFramework;
using UnityEngine;

namespace IndieFarm.Tool
{
    public class ToolHand : ITool
    {
        public Item Item { get; set; }
        public string Name { get; set; } = "hand";

        public int Range => Global.HandRange1UnLock ? 2 : 1;

        public bool Selectable(ToolData toolData)
        {
            return toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] != null &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant &&
                   toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].PlantState == PlantStates.Ripe;
        }

        public void Use(ToolData toolData)
        {
            //摘果子，植物消失
            Object.Destroy(PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y].GameObject);
            //toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].HasPlant = false;
            //重置土地的状态
            //toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y].PlantState = PlantStates.Seed;
            toolData.ShowGrid[toolData.CellPos.x, toolData.CellPos.y] = null;

            Global.OnPlantHarvest.Trigger(PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y]);


            void HarvestPlant(Plant plant, string plantName, BindableProperty<int> globalCount)
            {
                if (plant.name == plantName)
                {
                    var plantItem = Config.Items.FirstOrDefault(item => item.Name == plantName);
                    if (plantItem == null)
                    {
                        //找到对应名字的Item
                        plantItem = Config.CreatePlantItem(plantName, 1);
                        Config.Items.Add(plantItem);
                    }
                    else
                    {
                        plantItem.Count.Value++;
                    }

                    globalCount.Value++;
                }
            }

            if (PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as Plant)
            {
                var plant = PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as Plant;

                HarvestPlant(plant, "pumpkin", Global.PumpkinCount);

                HarvestPlant(plant, "potato", Global.PotatoCount);

                HarvestPlant(plant, "tomato", Global.TomatoCount);

                HarvestPlant(plant, "bean", Global.BeanCount);
            }

            if (PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as PlantCarrot)
            {
                //胡萝卜+1
                var carrotItem = Config.Items.FirstOrDefault(item => item.Name == "carrot");
                if (carrotItem == null)
                {
                    carrotItem = Config.CreateCarrot(1);
                    Config.Items.Add(carrotItem);
                }
                else
                {
                    carrotItem.Count.Value++;
                }

                Global.CarrotCount.Value++;
            }

            AudioController.Get.SfxHarvest.Play();
        }
    }
}