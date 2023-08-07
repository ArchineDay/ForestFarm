using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class PlantRadish : ViewController, IPlant
    {
        public int RipeDay { get; private set; }
        public GameObject GameObject => gameObject;

        public int XCell;
        public int YCell;

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
                if (newState == PlantStates.Small)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.smallPlantRadishSprite;
                }
                else if (newState == PlantStates.Ripe)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.ripeRadishSprite;
                }
                else if (newState == PlantStates.Seed)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.seedRadishSprite;
                }
                else if (newState == PlantStates.Old)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.oldSprite;
                }

                //同步到SoilData中
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }

        private int mSmallDay;

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
                    mSmallDay++;
                    if (mSmallDay == 2)
                    {
                        SetState(PlantStates.Ripe);
                    }
                }
            }
        }
    }
}