using System;

public interface IPopUpService
{
    bool IsShown { get; }

    void Show(PopUpConfig config, Action<PopUpResult> onClosed = null);
}
