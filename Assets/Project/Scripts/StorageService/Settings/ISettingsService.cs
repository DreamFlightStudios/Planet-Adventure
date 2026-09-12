using System;

public interface ISettingsService
{
    UserSettingsData Settings { get; }

    void Subscribe(Action<UserSettingsData> handler);
    void Unsubscribe(Action<UserSettingsData> handler);
    void Apply(UserSettingsData settings);
    UserSettingsData CreateDefault();
}
