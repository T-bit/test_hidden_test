using System;
using HiddenTest.UI;
using UnityEngine;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class UIServiceSettings : ServiceSettings<UIService>
    {
        [SerializeField]
        private UIModule _uiModulePrefab;

        public UIModule UIModulePrefab => _uiModulePrefab;
    }
}