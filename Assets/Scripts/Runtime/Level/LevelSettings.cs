using System;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Level
{
    [Serializable]
    public sealed class LevelSettings
    {
        [SerializeField]
        private LevelView _levelViewPrefab;

        [SerializeField]
        private ObjectSettings[] _objectSettingsList;

        public LevelView LevelViewPrefab => _levelViewPrefab;

        public IReadOnlyList<ObjectSettings> ObjectSettingsList => _objectSettingsList;
    }
}