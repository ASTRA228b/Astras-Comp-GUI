using UnityEngine;
using BepInEx;
using Astras_Comp_GUI.Core;
using Astras_Comp_GUI.Safe;

namespace Astras_Comp_GUI.Plugin
{
    [BepInPlugin(INFOOOOO.GUID, INFOOOOO.NAME, INFOOOOO.VSERION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Patch.Apply();
            GameObject CompUI = new GameObject(INFOOOOO.Name);
            CompUI.AddComponent<Credits>();
            CompUI.AddComponent<Notify>();
            CompUI.AddComponent<OnScreenNotify>();
            CompUI.AddComponent<MakeUI>();

            DontDestroyOnLoad(CompUI);
        }
    }
}