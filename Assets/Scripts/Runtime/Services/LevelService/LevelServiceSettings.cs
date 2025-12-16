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
        [Min(1)]
        private int _showObjectCount = 1;

        [SerializeField]
        private string _winMessage;

        [SerializeField]
        private string _looseMessage;

        [SerializeField]
        private ObjectSettings[] _objectSettingsList;

        public float TimerSeconds => _timerSeconds;
        public int ShowObjectCount => _showObjectCount;
        public string WinMessage => _winMessage;
        public string LooseMessage => _looseMessage;
        public IReadOnlyList<ObjectSettings> ObjectSettingsList => _objectSettingsList;
    }
}