using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class PlantPumpkin : ViewController, IPlant
    {
        public int RipeDay { get; private set; }
        public GameObject GameObject => gameObject;

        public int XCell { get; set; }
        public int YCell { get; set; }

        private PlantStates mState = PlantStates.Seed;
        public PlantStates State => mState; //get方法

        public void SetState(PlantStates newState)
        {
            if (newState != mState)
            {
                if (mState == PlantStates.Small && newState == PlantStates.Ripe)
                {
                    RipeDay = Global.Days.Value;
                }

                mState = newState;
                //切换表现
                if (newState == PlantStates.Seed)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.LoadSprite("PumpkinSeed");
                }
                else if (newState == PlantStates.Small)
                {
                    this.ClearSoilDigState();
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.LoadSprite("PumpkinSmall");
                }
                else if (newState == PlantStates.Middle)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.LoadSprite("PumpkinMiddle");
                }

                else if (newState == PlantStates.Big)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.LoadSprite("PumpkinBig");
                }
                else if (newState == PlantStates.Ripe)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.LoadSprite("PumpkinRipe");
                }

                //同步到SoilData中
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }

        public void Grow(SoliData soilData)
        {
            if (State == PlantStates.Seed)
            {
                if (soilData.Watered)
                {
                    //plant切换到small状态
                    SetState(PlantStates.Small);
                }
            }
            else if (State == PlantStates.Small)
            {
                if (soilData.Watered)
                {
                    SetState(PlantStates.Middle);
                }
            }
            else if (State == PlantStates.Middle)
            {
                if (soilData.Watered)
                {
                    SetState(PlantStates.Big);
                }
            }
            else if (State == PlantStates.Big)
            {
                if (soilData.Watered)
                {
                    SetState(PlantStates.Ripe);
                }
            }
        }
    }
}