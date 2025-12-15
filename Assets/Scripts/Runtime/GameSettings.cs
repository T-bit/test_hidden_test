using System.Collections.Generic;
using HiddenTest.Attributes;
using HiddenTest.Services;
using UnityEngine;

namespace HiddenTest
{
    [CreateAssetMenu(menuName = "HiddenTest/GameSettings",  fileName = "GameSettings")]
    public sealed class GameSettings : ScriptableObject
    {
        [SerializeReference]
        [SerializeReferencePicker]
        private IServiceInstaller[] _serviceInstallers;

        public IEnumerable<IServiceInstaller> ServiceInstallers => _serviceInstallers;
    }
}