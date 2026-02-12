using UnityEngine;

namespace Astras_Comp_GUI.Core
{
    public class Credits : MonoBehaviour
    {
        private GUIStyle? Stylelabel;
        private bool StylesLoaded = false;
        private void OnGUI()
        {
            MakeStyles();
            GUI.Label(new Rect(10, 10, 400, 20), "CREDS LIST:", Stylelabel);
            GUI.Label(new Rect(10, 30, 400, 20), "Made by ASTRA", Stylelabel);
            GUI.Label(new Rect(10, 50, 400, 20), "Creds To Spanky For some Code in The nofity", Stylelabel);
        }

        private void MakeStyles()
        {
            if (!StylesLoaded)
            {
                Stylelabel = new GUIStyle();
                Stylelabel.fontSize = 14;
                Stylelabel.normal.textColor = Color.white;
                StylesLoaded = true;
            }
        }

    }
}