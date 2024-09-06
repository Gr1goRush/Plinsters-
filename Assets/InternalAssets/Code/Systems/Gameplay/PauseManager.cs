using System;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public static event Action<bool> onPauseStateUpdated;
    private static bool _paused;

    public UnityEvent OnPaused;
    public UnityEvent OnRemuse;

    public static bool Paused => _paused;

    private void Start()
    {
        Continue();
    }

    public void Pause()
    {
        _paused = true;
        onPauseStateUpdated?.Invoke(Paused);
        OnPaused?.Invoke();
    }

    public void Continue()
    {
        _paused = false;
        onPauseStateUpdated?.Invoke(Paused);
        OnRemuse?.Invoke();
    }
}
