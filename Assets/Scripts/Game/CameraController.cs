using System;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class CameraController : ViewController
	{
		private Transform mPlayer;
		void Start()
		{
			mPlayer = FindObjectOfType<Player>().transform;
		}

		private void Update()
		{
			var position=Vector2.Lerp(transform.position,mPlayer.position,1-Mathf.Exp(-Time.deltaTime*10));
			transform.position= new Vector3(position.x, position.y, transform.position.z);
			
		}
		
		
	}
}
