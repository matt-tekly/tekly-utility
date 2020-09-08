using System.Collections;
using UnityEngine;

namespace Tekly.Utility.LifeCycles
{
    public delegate void UpdateDelegate();
    public delegate void QuitDelegate();
    public delegate void FocusDelegate(bool hasFocus);
    public delegate void PauseDelegate(bool paused);
    
    public interface ILifeCycle
    {
        event UpdateDelegate Update;
        event QuitDelegate Quit;
        event FocusDelegate Focus;
        event PauseDelegate Pause;

        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}