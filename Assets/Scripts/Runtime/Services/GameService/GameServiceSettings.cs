using System;
using System.Collections.Generic;
using HiddenTest.Level;
using UnityEngine;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class GameServiceSettings : ServiceSettings<GameService, IGameServiceSettings>, IGameServiceSettings
    {
        [SerializeField]
        [Min(0)]
        private float _timerSeconds;

        [SerializeField]
        private string _winMessage;

        [SerializeField]
        private string _looseMessage;

        [SerializeField]
        private LevelView _levelViewPrefab;

        [SerializeField]
        private ObjectSettings[] _objectSettingsList;

        #region IGameServiceSettings

        float IGameServiceSettings.TimerSeconds => _timerSeconds;
        string IGameServiceSettings.WinMessage => _winMessage;
        string IGameServiceSettings.LooseMessage => _looseMessage;
        LevelView IGameServiceSettings.LevelViewPrefab => _levelViewPrefab;
        IReadOnlyList<ObjectSettings> IGameServiceSettings.ObjectSettingsList => _objectSettingsList;

        #endregion
    }
}