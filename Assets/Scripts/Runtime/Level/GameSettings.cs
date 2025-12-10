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
        private IServiceSettings[] _serviceSettingsList;

        public IReadOnlyList<IServiceSettings> ServiceSettingsList => _serviceSettingsList;
    }
}