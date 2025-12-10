using System;
using UnityEngine;

namespace HiddenTest.Level
{
    [Serializable]
    public sealed class GeneralSettings
    {
        [SerializeField]
        [Min(0)]
        private float _timerSeconds;

        [SerializeField]
        private string _winMessage;

        [SerializeField]
        private string _looseMessage;

        public float TimerSeconds => _timerSeconds;

        public string WinMessage => _winMessage;

        public string LooseMessage => _looseMessage;
    }
}