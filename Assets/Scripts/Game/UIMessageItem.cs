using UnityEngine;
using QFramework;

namespace IndieFarm
{
	public partial class UIMessageItem : ViewController
	{

		public void SetAlpha(float alpha)
		{
			Icon.ColorAlpha(alpha);
			TextWithIcon.ColorAlpha(alpha);
			Text.ColorAlpha(alpha);
		}
	}
}
