using System;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace IndieFarm
{
	public partial class GameStartController : ViewController
	{
		void Start()
		{
			// Code Here
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("Game");
			}
		}
	}
}
