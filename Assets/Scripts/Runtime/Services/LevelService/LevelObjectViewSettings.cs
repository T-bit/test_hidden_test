using System;
using UnityEngine;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class LevelObjectViewSettings
    {
        [SerializeField]
        private bool _showName;

        [SerializeField]
        private bool _showImage;

        public bool ShowName =>  _showName;
        public bool ShowImage => _showImage;
    }
}