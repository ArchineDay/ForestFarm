namespace IndieFarm
{
    public class SoliData
    {
        //属性，同时具有 getter 和 setter 方法,属性的默认值被设置为 false。
        public bool HasPlant { get; set; } = false;
        public bool Watered { get; set; } = false;
        public PlantStates PlantState { get; set; } = PlantStates.Seed;
        
    }
}