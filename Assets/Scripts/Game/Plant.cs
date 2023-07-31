using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class Plant : ViewController
	{
		public int XCell;
		public int YCell;
		
		private PlantStates mState = PlantStates.Seed;
		public PlantStates State => mState;//get方法

		public void SetState(PlantStates newState)
		{
			if (newState!=mState)
			{
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
				}
				//同步到SoilData中
				FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
			}
		}

	}
}
