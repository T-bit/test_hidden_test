using System.Collections.Generic;
using HiddenTest.Attributes;
using HiddenTest.Services;
using UnityEngine;

namespace HiddenTest.Level
{
    [CreateAssetMenu(menuName = "HiddenTest/GameSettings",  fileName = "GameSettings")]
    public sealed class GameSettings : ScriptableObject
    {
        [SerializeReference]
        [SerializeReferencePicker]
        private ServiceSettings[] _serviceSettingsList;

        public IReadOnlyList<ServiceSettings> ServiceSettingsList => _serviceSettingsList;
    }
}