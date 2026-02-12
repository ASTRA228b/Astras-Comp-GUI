using Photon.Pun;
using GorillaLocomotion;
using UnityEngine;
using Astras_Comp_GUI.Core;
using Photon.Realtime;
using Photon.Voice;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using Meta.WitAi.Utilities;


namespace Astras_Comp_GUI.Core
{
    public class MakeUI : MonoBehaviour
    {
        private enum SpeedSettings
        {

        }
        private bool stylesINIT = false;














        private void CrateUIStyles()
        {

        }




        // gui style method
        Texture2D MakeTex(int width, int height, Color col)
        {
            Texture2D result = new Texture2D(width, height);
            for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) result.SetPixel(x, y, col);
            result.Apply();
            return result;
        }

        // MOD BullShit 
        static float WallWalkSpeed = 0f;
        bool CONTROLLERn = false;
        bool HasInit = false;


        void walk()
        {
            if (ControllerInputPoller.instance.rightGrab && ControllerInputPoller.instance.leftGrab)
            {
                GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(
                    GTPlayer.Instance.bodyCollider.transform.forward * WallWalkSpeed,
                    ForceMode.Acceleration
                );
            }
        }


        void CON()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !CONTROLLERn)
            {
                WallWalkSpeed += 5f;
                if (WallWalkSpeed >= 100f)
                {
                    WallWalkSpeed = 0f;
                }
                OnScreenNotify.SendIT($"[WallWalk] Speed set to {WallWalkSpeed}");
            }
            if (ControllerInputPoller.instance.rightControllerSecondaryButton && !HasInit)
            {
                WallWalkSpeed = 0f;
                OnScreenNotify.SendIT("[WallWalk] Speed reset to 0");
            }

            HasInit = ControllerInputPoller.instance.rightControllerSecondaryButton;
            CONTROLLERn = ControllerInputPoller.instance.rightControllerPrimaryButton;
        }








        // notify shit 

        public void HHHHHHHHHHHHH()
        {
            bool inroom = PhotonNetwork.InRoom;
            if (!InRoom && inroom)
            {
                OnJoinedRoom();
            }
            if (InRoom && !inroom)
            {
                OnLeaveRoom();
            }
            InRoom = inroom;
        }

        public void OnJoinedRoom()
        {
            string YOURNAME = PhotonNetwork.CurrentRoom.Name;
            string h = "You Joined the room: " + YOURNAME;
            OnScreenNotify.SendIT(h);
            Notify.Show(h);
        }
        void Update()
        {
            HHHHHHHHHHHHH();
            CON();
            walk();
        }
        void WELLCOME()
        {
            OnScreenNotify.SendIT("Hello, welcome to ASTRAS WALL WALK!");
            OnScreenNotify.SendIT("Press both grips to wall walk.");
            OnScreenNotify.SendIT("Press A to change Wall Walk Speed.");
            OnScreenNotify.SendIT("Press B to reset the Wall Walk Speed.");
            OnScreenNotify.SendIT($"[WallWalk] Speed set to {WallWalkSpeed}");
        }

        public void OnLeaveRoom()
        {
            OnScreenNotify.SendIT("You left the room");
            Notify.Show("You left the room");

        }
        private bool InRoom = false;
    }
}