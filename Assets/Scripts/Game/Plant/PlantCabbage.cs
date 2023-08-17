using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class PlantCabbage : ViewController, IPlant
    {
        public int RipeDay { get; private set; }
        public GameObject GameObject => gameObject;

        public int XCell { get;  set; }
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
                if (newState == PlantStates.Small)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.smallCabbageSprite;
                }
                else if (newState == PlantStates.Ripe)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.ripeCabbageSprite;
                }
                else if (newState == PlantStates.Seed)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.seedCabbageSprite;
                }
                else if (newState == PlantStates.Old)
                {
                    GetComponent<SpriteRenderer>().sprite = ResController.Instance.oldSprite;
                }

                //同步到SoilData中
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }

        private int mSeedStateDay = 0;
        private int mSmallStateDay = 0;

        public void Grow(SoliData soilData)
        {
            if (State == PlantStates.Seed)
            {
                if (soilData.Watered)
                {
                    mSeedStateDay++;
                    if (mSeedStateDay == 2)
                    {
                        //切换到small状态
                        SetState(PlantStates.Small);
                    }
                }
            }
            else if (State == PlantStates.Small)
            {
                if (soilData.Watered)
                {
                    mSmallStateDay++;
                    if (mSmallStateDay == 2)
                    {
                        SetState(PlantStates.Ripe);
                    }
                }
            }
        }
    }
}