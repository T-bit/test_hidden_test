using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace HiddenTest.Input
{
    public sealed class InputModule : MonoBehaviour
    {
        [SerializeField]
        private EventSystem _eventSystem;

        [SerializeField]
        private InputSystemUIInputModule _inputSystemUIInputModule;

        public EventSystem EventSystem => _eventSystem;
        public InputSystemUIInputModule InputSystemUIInputModule => _inputSystemUIInputModule;
    }
}