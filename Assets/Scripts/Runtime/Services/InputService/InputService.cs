using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class InputService : Service<InputServiceSettings>, IInputService
    {
        private InputModule _inputModule;
        private InputActions _inputActions;
        private bool _pressed;
        private IClickable _lastClickable;

        private event Action<IClickable> ClickableClicked;

        private InputActions.GameActions GameActions => _inputActions.Game;
        private Vector2 PointerPosition => GameActions.Point.ReadValue<Vector2>();

        public InputService(InputServiceSettings settings, Transform rootTransform, IObjectResolver container)
            : base(settings, rootTransform, container)
        {
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _inputModule = Container.Instantiate(Settings.InputModulePrefab, RootTransform);
            _inputActions = new InputActions();

            GameActions.Click.performed += OnClickPerformed;

            _inputActions.Enable();

            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {
            _inputActions.Disable();

            GameActions.Click.performed -= OnClickPerformed;

            _inputActions.Dispose();
            _inputActions = null;
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            if (!_pressed)
            {
                _lastClickable = null;
            }

            _pressed = !Mathf.Approximately(context.ReadValue<float>(), 0f);

            if (_pressed)
            {
                _lastClickable = GetClickable();
            }
            else if (_lastClickable != null)
            {
                var clickable = GetClickable();

                if (clickable == _lastClickable)
                {
                    ClickableClicked?.Invoke(clickable);
                }
            }
        }

        private IClickable GetClickable()
        {
            var camera = Camera.main;
            // TODO: Get camera
            var worldPoint = camera.ScreenToWorldPoint(PointerPosition);
            var hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent<IClickable>(out var clickable))
            {
                return clickable;
            }

            return null;
        }

        #region IInputService

        event Action<IClickable> IInputService.ClickableClicked
        {
            add => ClickableClicked += value;
            remove => ClickableClicked -= value;
        }

        #endregion
    }
}