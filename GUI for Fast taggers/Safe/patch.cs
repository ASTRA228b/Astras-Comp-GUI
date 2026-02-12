using HarmonyLib;
using UnityEngine;

namespace Astras_Comp_GUI.Safe
{
    internal class Patch
    {
        public static void Apply()
        {
            var HHH = new Harmony(INFOOOOO.GUID);
            HHH.PatchAll();
        }
    }
}