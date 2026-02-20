using Photon.Pun;
using GorillaLocomotion;
using UnityEngine;
using Astras_Comp_GUI.Core;
using Photon.Realtime;
using Photon.Voice;
using UnityEngine.InputSystem;
using System.Diagnostics;


namespace Astras_Comp_GUI.Core
{
    public class MakeUI : MonoBehaviour
    {
        private enum SpeedSettings
        {

        }
        private bool stylesINIT = false;
        public bool GUIOPENH = false;


        private void OnGUI()
        {
            if (!stylesINIT)
            {
                CrateUIStyles();
            }
            // real window soon
        }











        private void CrateUIStyles()
        {
            stylesINIT = true;
            // hi
        }




        // gui style method
        Texture2D MakeTex(int width, int height, Color col)
        {
            Texture2D result = new Texture2D(width, height);
            for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) result.SetPixel(x, y, col);
            result.Apply();
            return result;
        }
        Texture2D MakeRoundedTex(int width, int height, Color col, int radius = 20)
        {
            Texture2D result = new Texture2D(width, height);
            result.filterMode = FilterMode.Bilinear;

            Color clear = new Color(0, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool draw = true;

                    
                    if (x < radius && y < radius)
                        draw = Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius)) <= radius;

                    
                    if (x < radius && y >= height - radius)
                        draw = Vector2.Distance(new Vector2(x, y), new Vector2(radius, height - radius)) <= radius;

                    
                    if (x >= width - radius && y < radius)
                        draw = Vector2.Distance(new Vector2(x, y), new Vector2(width - radius, radius)) <= radius;

                    
                    if (x >= width - radius && y >= height - radius)
                        draw = Vector2.Distance(new Vector2(x, y), new Vector2(width - radius, height - radius)) <= radius;

                    result.SetPixel(x, y, draw ? col : clear);
                }
            }

            result.Apply();
            return result;
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
        private void Update()
        {
            HHHHHHHHHHHHH();
   
            // fow later when gui is real
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                GUIOPENH = !GUIOPENH;
            }
        }
        

        public void OnLeaveRoom()
        {
            OnScreenNotify.SendIT("You left the room");
            Notify.Show("You left the room");

        }
        private bool InRoom = false;
    }
}