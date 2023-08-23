using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace IndieFarm
{
	public partial class Home : ViewController
	{
		private void OnTriggerEnter2D(Collider2D col)
		{

			if (col.name.StartsWith("Player"))
			{
				Global.Days.Value++;
				col.PositionY(this.Position().y - 3);//Y轴位置设置为当前触发器的Y轴位置减去3个单位
				AudioController.Get.SfxNextDay.Play();
			}
		}
	} 
}