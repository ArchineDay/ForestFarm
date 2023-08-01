using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public  GameObject waterPrefab;
		public  GameObject plantPrefab;
		
		public Sprite seedSprite;
		public Sprite smallPlantSprite;
		public Sprite ripeSprite;
		public Sprite oldSprite;

		//在整个应用程序中只有一个指定类型的对象实例存在
		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		
		public void OnSingletonInit()
		{
		}
	}
}
