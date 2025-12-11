using System;
using HiddenTest.Input;
using UnityEngine;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class InputServiceSettings : ServiceSettings<InputService>
    {
        [SerializeField]
        private InputModule _inputModulePrefab;

        public InputModule InputModulePrefab => _inputModulePrefab;
    }
}