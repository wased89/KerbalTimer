using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace KerbalTimer
{
    [KSPAddon(KSPAddon.Startup.Flight,false)]
    public class HeadMaster : MonoBehaviour
    {
        public int myTimer;
        public static List<Timer> timers = new List<Timer>();
        //GUI vars
        bool showGUI = false;
        int mainGUID;
        Rect mainGUI = new Rect(100,100,250,300);
        Vector2 scrollbar = new Vector2();

        private ApplicationLauncherButton KTButton = null;
        private bool buttonNeedsInit = true;

        public void Start()
        {
            mainGUID = Guid.NewGuid().GetHashCode();
            buttonNeedsInit = true;
            InitButtons();
            myTimer = TimerFunctions.AddTimer();
            TimerFunctions.AddTimer();
        }

        public void OnGUI()
        {
            if (showGUI)
            {
                mainGUI = GUILayout.Window(mainGUID, mainGUI, MainWindow, "Timer~");
            }
        }
        public void MainWindow(int windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Start"))
            {
                TimerFunctions.StartTimer(myTimer);
            }
            if (GUILayout.Button("Lap"))
            {
                TimerFunctions.LapTimer(myTimer);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Stop"))
            {
                TimerFunctions.StopTimer(myTimer);
            }
            if(GUILayout.Button("Reset"))
            {
                TimerFunctions.ResetTimer(myTimer);
            }
            GUILayout.EndHorizontal();
            Timer timer = timers[myTimer];
            GUILayout.Label("Current time: " 
                + timer.maintimer.Elapsed.Hours + ":" + timer.maintimer.Elapsed.Minutes + ":" + timer.maintimer.Elapsed.Seconds + "." + timer.maintimer.Elapsed.Milliseconds);
            scrollbar = GUILayout.BeginScrollView(scrollbar);
            for(int i = 0; i < timer.laptimes.Count; i++)
            {
                GUILayout.Label("Lap " + i + ": " + timer.laptimes[i]);
            }
            
            GUILayout.EndScrollView();
            GUILayout.Label("Final Time: " + timer.finaltime);
            GUILayout.EndVertical();
            GUI.DragWindow();
        }
        public void SaveTime()
        {

        }


        void OnAppButtonReady()
        {
            Debug.Log("App launcher Ready");
            if (ApplicationLauncher.Ready)
            {
                Debug.Log("Doing Flight Button things");
                KTButton = ApplicationLauncher.Instance.AddModApplication(
                    OnAppLaunchToggleOn,
                    OnAppLaunchToggleOff,
                    DummyVoid,
                    DummyVoid,
                    DummyVoid,
                    DummyVoid,
                    ApplicationLauncher.AppScenes.FLIGHT,
                    (Texture)GameDatabase.Instance.GetTexture("KerbalTimer/Textures/appButton", false));
            }
        }

        private void ShowGUI()
        {
            Debug.Log("ShowGUI");
            showGUI = true;
        }

        private void HideGUI()
        {
            Debug.Log("HideGUI");
            showGUI = false;
        }

        void OnAppLaunchToggleOff()
        {
            showGUI = false;
        }

        void OnAppLaunchToggleOn()
        {
            showGUI = true;
        }

        void DummyVoid() { }

        void InitButtons()
        {
            Debug.Log("Init Buttons");
            if (KTButton == null)
            {
                Debug.Log("AppButton Null, proceeding to ready.");
                GameEvents.onGUIApplicationLauncherReady.Add(OnAppButtonReady);
                if (ApplicationLauncher.Ready)
                {
                    OnAppButtonReady();
                }
            }

            GameEvents.onShowUI.Add(ShowGUI);
            GameEvents.onHideUI.Add(HideGUI);

            buttonNeedsInit = false;
        }

        void DestroyButtons()
        {
            Debug.Log("Destroying Buttons");
            GameEvents.onGUIApplicationLauncherReady.Remove(OnAppButtonReady);
            GameEvents.onShowUI.Remove(ShowGUI);
            GameEvents.onHideUI.Remove(HideGUI);

            if (KTButton != null)
                ApplicationLauncher.Instance.RemoveModApplication(KTButton);


            buttonNeedsInit = true;
        }

        void OnDestroy()
        {
            Debug.Log("Destroying all timers");
            DestroyButtons();
        }
    }
}
