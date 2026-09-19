using System;
using System.Collections.Generic;
using UnityEngine;

public class PauseService : IPauseService
{
    private readonly HashSet<object> _owners = new();
    private readonly InputSystem _input;

    private bool _savedCursorVisible;
    private CursorLockMode _savedCursorLockState;
    private float _savedTimeScale = 1f;
    private bool _savedPlayerInputEnabled = true;

    public event Action<bool> PauseChanged;

    public bool IsPaused => _owners.Count > 0;

    public PauseService(InputSystem input) => _input = input;

    public void Pause(object owner)
    {
        if (owner == null || _owners.Add(owner) == false || _owners.Count > 1)
            return;

        _savedCursorVisible = Cursor.visible;
        _savedCursorLockState = Cursor.lockState;
        _savedTimeScale = Time.timeScale;
        _savedPlayerInputEnabled = _input.Player.enabled;

        Time.timeScale = 0f;
        AudioListener.pause = true;
        _input.Player.Disable();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PauseChanged?.Invoke(true);
    }

    public void Resume(object owner)
    {
        if (owner == null || _owners.Remove(owner) == false || _owners.Count > 0)
            return;

        Time.timeScale = _savedTimeScale;
        AudioListener.pause = false;

        // Restored, not forced on: a popup can pause the main menu, where the gameplay map must stay off.
        if (_savedPlayerInputEnabled)
            _input.Player.Enable();

        Cursor.visible = _savedCursorVisible;
        Cursor.lockState = _savedCursorLockState;

        PauseChanged?.Invoke(false);
    }

    // Owners live in the unloaded scene and can never release the pause, so the state is dropped on scene change.
    // The cursor is left alone: the entry point of the loaded scene has already set it up by this moment.
    public void ResetState()
    {
        var wasPaused = IsPaused;

        _owners.Clear();
        _savedTimeScale = 1f;
        _savedPlayerInputEnabled = true;

        Time.timeScale = 1f;
        AudioListener.pause = false;
        _input.Player.Enable();

        if (wasPaused)
            PauseChanged?.Invoke(false);
    }
}
