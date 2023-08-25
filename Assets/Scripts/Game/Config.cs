using System.Collections.Generic;
using IndieFarm.Tool;
using QFramework;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;

namespace IndieFarm
{
    public class Config
    {
        public static Item CreatePlantItem(string name, int count)
        {
            return name switch
            {
                "tomato" => CreateTomato(count),
                "bean" => CreateBean(count),
                "carrot" => CreateCarrot(count),
                "pumpkin" => CreatePumpkin(count),
                "potato" => CreatePotato(count),
                _ => null
            };
        }

        public static Item CreateHand() =>
            Item.CreateItem("ToolHand_0", 1, false, false, "hand", string.Empty, new ToolHand());

        public static Item CreateHandShovel() =>
            Item.CreateItem("ToolShovel_0", 1, false, false, "shovel", string.Empty, new ToolShovel());

        public static Item CreateWateringCan() =>
            Item.CreateItem("ToolWateringCan_0", 1, false, false, "watering_can",
                string.Empty, new ToolWateringCan());

        public static Item CreateCarrot(int count) =>
            Item.CreateItem("CarrotIcon", count, true, false, "carrot", string.Empty, null);

        public static Item CreatePumpkin(int count) =>
            Item.CreateItem("PumpkinIcon", count, true, false, "pumpkin", string.Empty, null);

        public static Item CreatePotato(int count) =>
            Item.CreateItem("PotatoIcon", count, true, false, "potato", string.Empty, null);

        public static Item CreateTomato(int count) =>
            Item.CreateItem("TomatoIcon", count, true, false, "tomato", string.Empty, null);

        public static Item CreateBean(int count) =>
            Item.CreateItem("BeanIcon", count, true, false, "bean", string.Empty, null);

        public static Item CreateSeedCarrot(int count = 5) =>
            Item.CreateItem("CarrotSeedIcon", count, true, true, "seed_carrot", "PlantCarrot", new ToolSeed());

        public static Item CreateSeedPumpkin(int count = 5) =>
            Item.CreateItem("PumpkinSeedIcon", count, true, true, "seed_pumpkin", "PlantPumpkin", new ToolSeed());

        public static Item CreateSeedPotato(int count = 5) =>
            Item.CreateItem("PotatoSeedIcon", count, true, true, "seed_potato", "PlantPotato", new ToolSeed());

        public static Item CreateSeedTomato(int count = 5) =>
            Item.CreateItem("TomatoSeedIcon", count, true, true, "seed_tomato", "PlantTomato", new ToolSeed());

        public static Item CreateSeedBean(int count = 5)=> 
            Item.CreateItem("BeanSeedIcon", count, true, true, "seed_bean", "PlantBean", new ToolSeed());
        
        public static List<Item> Items = new List<Item>()
        {
            CreateHand(),
            CreateHandShovel(),
            CreateWateringCan(),

            CreateSeedCarrot(),
            CreateSeedPumpkin(),
            CreateSeedPotato(),
            CreateSeedTomato(),
            CreateSeedBean()
        };
    }
}