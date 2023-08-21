using System.Collections.Generic;
using IndieFarm.Tool;
using QFramework;
using Unity.VisualScripting.Dependencies.NCalc;

namespace IndieFarm
{
    public class Config
    {
        public static List<Item> Items = new List<Item>()
        {
            new Item()
            {
                IconName = "ToolHand_0",
                Count = new BindableProperty<int>(1),
                Countable = false,
                IsPlant = false,
                Name = "hand",
                PlantPrefabName = string.Empty,
                Tool = new ToolHand()
            },
            new Item()
            {
                IconName = "ToolShovel_0",
                Count = new BindableProperty<int>(1),
                Countable = false,
                IsPlant = false,
                Name = "shovel",
                PlantPrefabName = string.Empty,
                Tool = new ToolShovel()
            },
            new Item()
            {
                IconName = "ToolSeed_0",
                Count = new BindableProperty<int>(5),
                Countable = true,
                IsPlant = true,
                Name = "seed",
                PlantPrefabName = "Plant",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            }),
            new Item()
            {
                IconName = "ToolWateringCan_0",
                Count = new BindableProperty<int>(1),
                Countable = false,
                IsPlant = false,
                Name = "watering_can",
                PlantPrefabName = string.Empty,
                Tool = new ToolWateringCan()
            },
            new Item()
            {
                IconName = "ToolSeedRadish_0",
                Count = new BindableProperty<int>(5),
                Countable = true,
                IsPlant = true,
                Name = "seed_radish",
                PlantPrefabName = "PlantRadish",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            }),
            new Item()
            {
                IconName = "ToolSeedCabbage_0",
                Count = new BindableProperty<int>(5),
                Countable = true,
                IsPlant = true,
                Name = "seed_cabbage",
                PlantPrefabName = "PlantCabbage",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            }),
            new Item()
            {
                IconName = "CarrotSeedIcon",
                Count = new BindableProperty<int>(5),
                Countable = true,
                IsPlant = true,
                Name = "seed_carrot",
                PlantPrefabName = "PlantCarrot",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            }),
        };
    }
}