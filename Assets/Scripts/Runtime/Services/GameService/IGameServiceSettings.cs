using System.Collections.Generic;
using HiddenTest.Level;

namespace HiddenTest.Services
{
    public interface IGameServiceSettings : IServiceSettings
    {
        float TimerSeconds { get; }
        string WinMessage { get; }
        string LooseMessage { get; }
        LevelView LevelViewPrefab { get; }
        IReadOnlyList<ObjectSettings> ObjectSettingsList { get; }
    }
}