using System;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class ChallengeCotroller : ViewController
    {
        void Start()
        {
            // Code Here
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(960, 540);

            GUI.Label(new Rect(960 - 300, 0, 300, 20), "@@ 挑战 @@");
            for (int i = 0; i < Global.ActiveChallenges.Count; i++)
            {
                var challenge = Global.ActiveChallenges[i];

                GUI.Label(new Rect(960 - 300, 20 + i * 20, 300, 20), challenge.Name);
            }

            for (int i = 0; i < Global.FinishedChallenges.Count; i++)
            {
                var challenge = Global.FinishedChallenges[i];

                GUI.Label(new Rect(960 - 300, 20 + (i + Global.ActiveChallenges.Count) * 20, 300, 20),
                    "<color=green>" + challenge.Name + "</color>");
            }
        }
    }
}