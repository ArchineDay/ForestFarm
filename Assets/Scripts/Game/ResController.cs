using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public  GameObject waterPrefab;
		public  GameObject plantPrefab;
		public GameObject plantRadishPrefab;
		public GameObject plantCabbagePrefab;
		
		public Sprite seedSprite;
		public Sprite smallPlantSprite;
		public Sprite ripeSprite;
		public Sprite oldSprite;
		
		public Sprite seedRadishSprite;
		public Sprite smallPlantRadishSprite;
		public Sprite ripeRadishSprite;
		
		public Sprite seedCabbageSprite;
		public Sprite smallCabbageSprite;
		public Sprite ripeCabbageSprite;


		public List<Sprite> Sprites = new List<Sprite>();

		public Sprite LoadSprites(string spriteName)
		{
			return Sprites.Single(spr=>spr.name==spriteName);
		}
		
		//在整个应用程序中只有一个指定类型的对象实例存在
		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;
		
		public void OnSingletonInit()
		{
		}
	}
}
