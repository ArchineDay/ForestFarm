using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class TileSelectController : ViewController,ISingleton
	{
		public static TileSelectController Instance => MonoSingletonProperty<TileSelectController>.Instance;

		public void OnSingletonInit()
		{
			//throw new System.NotImplementedException();
		}
	}
}
