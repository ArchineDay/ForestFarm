using System;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class TimeController : ViewController
	{
		public static float Seconds = 0;
		private void Start()
		{
			Seconds = 0;
		}

		private void Update()
		{
			Seconds+= Time.deltaTime;
		}

		private void OnDestroy()
		{
			Debug.Log($"通关花费了{Seconds}");
		}

		private void OnGUI()
		{
			IMGUIHelper.SetDesignResolution(640,360);
			
			GUI.Label(new Rect(640-50,360-20,50,50),$"{(int)Seconds}s");
		}
	}
}
