using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class ResController : ViewController,ISingleton
	{
		public  GameObject seedPrefab;
		public  GameObject waterPrefab;
		public  GameObject smallPlantPrefab;

		//在整个应用程序中只有一个指定类型的对象实例存在
		public static ResController Instance => MonoSingletonProperty<ResController>.Instance;

		void Start()
		{
			// Code Here
		}

		public void OnSingletonInit()
		{
			//throw new System.NotImplementedException();
		}
	}
}
