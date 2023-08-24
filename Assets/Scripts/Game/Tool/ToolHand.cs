using System.Linq;
using UnityEngine;

namespace IndieFarm.Tool
{
    public class ToolHand:ITool
    {
        public Item Item { get; set; }
        public string Name { get; set; } = "hand";

        public int Range=>Global.HandRange1UnLock?2:1;

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


            if (PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as Plant)
            {
                var plant = PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as Plant;
                if (plant.name=="fruit")
                {
                    //果子+1
                    Global.FruitCount.Value++;
                }
                else if (plant.name=="pumpkin")
                {
                    var pumpkinItem=Config.Items.FirstOrDefault(item => item.Name == "pumpkin");
                    if (pumpkinItem==null)
                    {
                        pumpkinItem = Config.CreatePumpkin(1);
                        Config.Items.Add(pumpkinItem);
                    }
                    else 
                    {
                        pumpkinItem.Count.Value++;
                    }
                    Global.PumpkinCount.Value++;
                }
                else if (plant.name=="potato")
                {
                    var potatoItem=Config.Items.FirstOrDefault(item => item.Name == "potato");
                    if (potatoItem==null)
                    {
                        potatoItem = Config.CreatePotato(1);
                        Config.Items.Add(potatoItem);
                    }
                    else 
                    {
                        potatoItem.Count.Value++;
                    }
                    Global.PotatoCount.Value++;
                }
                else if (plant.name=="tomato")
                {
                    var tomatoItem=Config.Items.FirstOrDefault(item => item.Name == "tomato");
                    if (tomatoItem==null)
                    {
                        tomatoItem = Config.CreateTomato(1);
                        Config.Items.Add(tomatoItem);
                    }
                    else 
                    {
                        tomatoItem.Count.Value++;
                    }
                    Global.TomatoCount.Value++;
                }
                else if (plant.name=="bean")
                {
                    var beanItem=Config.Items.FirstOrDefault(item => item.Name == "bean");
                    if (beanItem==null)
                    {
                        beanItem = Config.CreateBean(1);
                        Config.Items.Add(beanItem);
                    }
                    else 
                    {
                        beanItem.Count.Value++;
                    }
                    Global.BeanCount.Value++;
                }

           
               
            }

            if (PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as PlantRadish)
            {
                //萝卜+1
                Global.RadishCount.Value++;
            }
            
            if (PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as PlantCabbage)
            {
                //白菜+1
                Global.CabbageCount.Value++;
            }
            if (PlantController.Instance.Plants[toolData.CellPos.x, toolData.CellPos.y] as PlantCarrot)
            {
                //胡萝卜+1
                var carrotItem=Config.Items.FirstOrDefault(item => item.Name == "carrot");
                if (carrotItem==null)
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