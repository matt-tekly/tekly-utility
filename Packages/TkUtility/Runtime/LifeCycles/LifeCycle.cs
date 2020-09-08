using System.Collections;
using UnityEngine;

namespace Tekly.Utility.LifeCycles
{
    public class LifeCycle : ILifeCycle
    {
        public static ILifeCycle Instance = new LifeCycle();
        
        public event UpdateDelegate Update
        {
            add => m_updateDelegates.Add(value);
            remove => m_updateDelegates.Remove(value);
        }
        
        public event QuitDelegate Quit
        {
            add => m_quitDelegates.Add(value);
            remove => m_quitDelegates.Remove(value);
        }
        
        public event FocusDelegate Focus
        {
            add => m_focusDelegates.Add(value);
            remove => m_focusDelegates.Remove(value);
        }
        
        public event PauseDelegate Pause
        {
            add => m_pauseDelegates.Add(value);
            remove => m_pauseDelegates.Remove(value);
        }
        
        private static LifeCycleListener s_listener;
        
        private readonly SafeList<UpdateDelegate> m_updateDelegates = new SafeList<UpdateDelegate>();
        private readonly SafeList<QuitDelegate> m_quitDelegates = new SafeList<QuitDelegate>();
        private readonly SafeList<FocusDelegate> m_focusDelegates = new SafeList<FocusDelegate>();
        private readonly SafeList<PauseDelegate> m_pauseDelegates = new SafeList<PauseDelegate>();
        
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            var gameObject = new GameObject("LifeCycle");
            Object.DontDestroyOnLoad(gameObject);

            s_listener = gameObject.AddComponent<LifeCycleListener>();
            s_listener.LifeCycle = Instance as LifeCycle;
        }

        public void Updated()
        {
            foreach (var del in m_updateDelegates) {
                del.Invoke();
            }
        }
        
        public void OnApplicationQuit()
        {
            foreach (var del in m_quitDelegates) {
                del.Invoke();
            }
        }

        public void OnApplicationPause(bool paused)
        {
            foreach (var del in m_pauseDelegates) {
                del.Invoke(paused);
            }
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            foreach (var del in m_focusDelegates) {
                del.Invoke(hasFocus);
            }
        }

        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return s_listener.StartCoroutine(enumerator);
        }
    }
}
