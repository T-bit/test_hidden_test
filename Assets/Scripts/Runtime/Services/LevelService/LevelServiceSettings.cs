using System;
using System.Collections.Generic;
using HiddenTest.Level;
using UnityEngine;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class LevelServiceSettings : ServiceSettings<LevelService>
    {
        [SerializeField]
        [Min(0)]
        private float _timerSeconds;

        [SerializeField]
        private string _winMessage;

        [SerializeField]
        private string _looseMessage;

        [SerializeField]
        private LevelModule _levelModulePrefab;

        [SerializeField]
        private ObjectSettings[] _objectSettingsList;

        public float TimerSeconds => _timerSeconds;
        public string WinMessage => _winMessage;
        public string LooseMessage => _looseMessage;
        public LevelModule LevelModulePrefab => _levelModulePrefab;
        public IReadOnlyList<ObjectSettings> ObjectSettingsList => _objectSettingsList;
    }
}