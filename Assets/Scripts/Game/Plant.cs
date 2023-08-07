using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public interface IPlant
	{
		GameObject GameObject { get; }
		PlantStates State { get; }
		void SetState(PlantStates state);
		void Grow(SoliData soilData);

		int RipeDay { get; }
	}
	
	public partial class Plant : ViewController,IPlant
	{
		public int RipeDay { get; private set; }

		public GameObject GameObject => gameObject;
		
		public int XCell;
		public int YCell;
		
		private PlantStates mState = PlantStates.Seed;
		public PlantStates State => mState;//get方法
		
		public void SetState(PlantStates newState)
		{
			if (newState!=mState)
			{
				if (mState==PlantStates.Small &&newState ==PlantStates.Ripe)
				{
					RipeDay = Global.Days.Value;
				}
				mState=newState;
				//切换表现
				if (newState ==PlantStates.Small)
				{
					GetComponent<SpriteRenderer>().sprite = ResController.Instance.smallPlantSprite;
				}else if (newState ==PlantStates.Ripe)
				{
					GetComponent<SpriteRenderer>().sprite = ResController.Instance.ripeSprite;
				}else if (newState ==PlantStates.Seed)
				{
					GetComponent<SpriteRenderer>().sprite = ResController.Instance.seedSprite;
				}else if (newState ==PlantStates.Old)
				{
					GetComponent<SpriteRenderer>().sprite = ResController.Instance.oldSprite;
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
			       SetState(PlantStates.Ripe);
			    }
			}
		}

	
	}
}
