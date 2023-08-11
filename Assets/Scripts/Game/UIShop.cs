using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class UIShop : ViewController
	{
		void Start()
		{
			Global.FruitCount.RegisterWithInitValue(fruitCount =>
			{
				if (fruitCount >= 1)
				{
					BtnBuyFruitSeed.Show();
				}
				else
				{
					BtnBuyFruitSeed.Hide();
				}

				if (fruitCount>=2)
				{
					BtnBuyRadish.Show();
				}
				else
				{
					BtnBuyRadish.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.RadishCount.RegisterWithInitValue(radishCount =>
			{
				if (radishCount >= 1)
				{
					BtnBuyRadishSeed.Show();
				}
				else
				{
					BtnBuyRadishSeed.Hide();
				}

				if (radishCount>=2)
				{
					BtnBuyFruit.Show();
				}
				else
				{
					BtnBuyFruit.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BtnBuyFruitSeed.onClick.AddListener(()=>
			{
				Global.FruitCount.Value -= 1;
				Global.FruitSeedCount.Value+= 2;
			});
			
			BtnBuyRadishSeed.onClick.AddListener(()=>
			{
				Global.RadishCount.Value -= 1;
				Global.RadishSeedCount.Value+= 2;
			});
			
			BtnBuyFruit.onClick.AddListener(() =>
			{
				Global.FruitCount.Value += 1;
				Global.RadishCount.Value -= 2;
			});
			
			BtnBuyRadish.onClick.AddListener(() =>
			{
				Global.RadishCount.Value += 1;
				Global.FruitCount.Value -= 2;
			});
		}
	}
}
