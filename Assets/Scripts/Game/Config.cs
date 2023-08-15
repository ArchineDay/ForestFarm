using System.Collections.Generic;

namespace IndieFarm
{
    public class Config
    {
        public static List<Item> Items = new List<Item>()
        {
            new Item()
            {
                IconName = "ToolHand_0",
                Count = 1,
                Countable = false,
                IsPlant = false,
                Name = "hand",
                PlantPrefab = string.Empty,
                Tool = Constant.ToolHand
            },
            new Item()
            {
                IconName = "ToolShovel_0",
                Count = 1,
                Countable = false,
                IsPlant = false,
                Name = "shovel",
                PlantPrefab = string.Empty,
                Tool = Constant.ToolShovel
            },
            new Item()
            {
                IconName = "ToolSeed_0",
                Count = 1,
                Countable = true,
                IsPlant = true,
                Name = "seed",
                PlantPrefab = "Plant",
                Tool = Constant.ToolSeed
            },
            new Item()
            {
                IconName = "ToolWateringCan_0",
                Count = 1,
                Countable = false,
                IsPlant = false,
                Name = "watering_can",
                PlantPrefab = string.Empty,
                Tool = Constant.ToolWateringCan
            },
            new Item()
            {
                IconName = "ToolSeedRadish_0",
                Count = 1,
                Countable = true,
                IsPlant = true,
                Name = "seed_radish",
                PlantPrefab = "PlantRadish",
                Tool = Constant.ToolSeedRadish
            },
            new Item()
            {
                IconName = "ToolSeedCabbage_0",
                Count = 1,
                Countable = true,
                IsPlant = true,
                Name = "seed_cabbage",
                PlantPrefab = "PlantCabbage",
                Tool = Constant.ToolSeedCabbage
            }
        };
    }
}