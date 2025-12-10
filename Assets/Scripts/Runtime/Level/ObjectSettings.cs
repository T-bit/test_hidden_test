using System;
using UnityEngine;

namespace HiddenTest.Level
{
    [Serializable]
    public sealed class ObjectSettings
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private Sprite _sprite;

        public string Name => _name;

        public Sprite Sprite => _sprite;

        // That'll work for now
        public string Id => _sprite?.name;
    }
}