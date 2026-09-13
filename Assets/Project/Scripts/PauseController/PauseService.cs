using System;
using System.Collections.Generic;
using UnityEngine;

public class PauseService : IPauseService
{
    private readonly HashSet<object> _owners = new();

    private bool _savedCursorVisible;
    private CursorLockMode _savedCursorLockState;

    public event Action<bool> PauseChanged;

    public bool IsPaused => _owners.Count > 0;

    public void Pause(object owner)
    {
        if (owner == null || _owners.Add(owner) == false || _owners.Count > 1)
            return;

        _savedCursorVisible = Cursor.visible;
        _savedCursorLockState = Cursor.lockState;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PauseChanged?.Invoke(true);
    }

    public void Resume(object owner)
    {
        if (owner == null || _owners.Remove(owner) == false || _owners.Count > 0)
            return;

        Cursor.visible = _savedCursorVisible;
        Cursor.lockState = _savedCursorLockState;

        PauseChanged?.Invoke(false);
    }
}
