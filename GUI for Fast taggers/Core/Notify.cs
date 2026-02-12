using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Astras_Comp_GUI.Core
{
    public class Notify : MonoBehaviour
    {
        
        private static Notify? instance;

        private GameObject? ui;
        private TextMeshProUGUI? text;
        private Queue<string> queue = new Queue<string>();
        private bool showing;

        private Transform? head;
        private Camera? cam;

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            while (GorillaTagger.Instance == null || GorillaTagger.Instance.offlineVRRig == null)
                yield return null;

            head = GorillaTagger.Instance.offlineVRRig.headMesh.transform;
            cam = Camera.main;
        }

        void Update()
        {
            if (ui != null && ui.activeSelf && cam != null)
                ui.transform.LookAt(ui.transform.position + (ui.transform.position - cam.transform.position));
        }

        public static void Show(string message)
        {
            if (instance == null) return;

            instance.queue.Enqueue(message);

            if (!instance.showing)
                instance.StartCoroutine(instance.ShowLoop());
        }

        private IEnumerator ShowLoop()
        {
            showing = true;

            while (queue.Count > 0)
            {
                if (ui == null)
                    CreateUI();
                if(text != null)
                   text.text = queue.Dequeue();

                yield return Animate(true);
                yield return new WaitForSeconds(2.5f);
                yield return Animate(false);
            }

            showing = false;
        }

        private IEnumerator Animate(bool show)
        {
            if (ui == null)
                yield break;

            ui.SetActive(true);

            float t = 0f;
            float dur = show ? 0.2f : 0.15f;

            Vector3 a = show ? Vector3.one * 0.0002f : Vector3.one * 0.0004f;
            Vector3 b = show ? Vector3.one * 0.0004f : Vector3.one * 0.0002f;

            while (t < dur)
            {
                t += Time.deltaTime;

                if (ui == null)
                    yield break;

                ui.transform.localScale = Vector3.Lerp(
                    a, b, Mathf.SmoothStep(0, 1, t / dur)
                );

                yield return null;
            }

            if (ui != null)
                ui.transform.localScale = b;

            if (!show && ui != null)
                ui.SetActive(false);
        }


        private void CreateUI()
        {
            ui = new GameObject("NotifyUI");
            ui.transform.SetParent(head);
            ui.transform.localPosition = new Vector3(0f, 0.12f, 0.18f);
            ui.transform.localScale = Vector3.one * 0.0002f;

            Canvas c = ui.AddComponent<Canvas>();
            c.renderMode = RenderMode.WorldSpace;

            CanvasScaler s = ui.AddComponent<CanvasScaler>();
            s.dynamicPixelsPerUnit = 10f;

            GameObject bg = new GameObject("BG");
            bg.transform.SetParent(ui.transform);

            Image img = bg.AddComponent<Image>();
            img.color = new Color(0.08f, 0.08f, 0.12f, 0.95f);

            bg.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 24);

            GameObject t = new GameObject("Text");
            t.transform.SetParent(bg.transform);

            text = t.AddComponent<TextMeshProUGUI>();
            text.fontSize = 9;
            text.alignment = TextAlignmentOptions.Center;
            text.fontStyle = FontStyles.Bold;
            text.color = Color.white;

            t.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 20);

            ui.SetActive(false);
        }
    }
}
