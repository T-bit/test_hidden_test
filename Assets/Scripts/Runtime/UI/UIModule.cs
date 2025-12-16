using UnityEngine;

namespace HiddenTest.UI
{
    public sealed class UIModule : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private RectTransform _enabledContainer;

        [SerializeField]
        private RectTransform _disabledContainer;

        public Canvas Canvas => _canvas;
        public RectTransform EnabledContainer => _enabledContainer;
        public RectTransform DisabledContainer => _disabledContainer;
    }
}