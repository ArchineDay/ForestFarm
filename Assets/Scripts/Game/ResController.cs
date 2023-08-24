using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public  GameObject waterPrefab;



		public List<Sprite> Sprites = new List<Sprite>();

		//加载图片
		public Sprite LoadSprite(string spriteName)
		{
			return Sprites.Single(spr=>spr.name==spriteName);
		}

		public List<GameObject> PlantPrefabs = new List<GameObject>();

		//加载预制体
		public GameObject LoadPrefab(string prefabName)
		{
			return PlantPrefabs.Single(pre=>pre.name==prefabName);
		}

		//在整个应用程序中只有一个指定类型的对象实例存在
		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		
		public void OnSingletonInit()
		{
		}
	}
}
