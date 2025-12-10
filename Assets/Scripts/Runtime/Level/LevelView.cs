using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Level
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField]
        private ObjectView[] _objectViews;

        public IEnumerable<ObjectView> ObjectViews => _objectViews;
    }
}