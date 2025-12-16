using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class InputService : Service, IInputService
    {
        private InputModule _inputModule;
        private InputActions _inputActions;
        private bool _pressed;
        private IClickable _lastClickable;

        private event Action<IClickable> ClickableClicked;
        private event Action ExitClicked;

        private InputActions.GameActions GameActions => _inputActions.Game;
        private Vector2 PointerPosition => GameActions.Point.ReadValue<Vector2>();

        [UnityEngine.Scripting.Preserve]
        public InputService(InputModule inputModule, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            _inputModule = inputModule;
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _inputActions = new InputActions();

            GameActions.Click.performed += OnClickPerformed;
            GameActions.Exit.performed += OnExitPerformed;

            _inputActions.Enable();

            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {
            _inputActions.Disable();

            GameActions.Click.performed -= OnClickPerformed;
            GameActions.Exit.performed -= OnExitPerformed;

            _inputActions.Dispose();
            _inputActions = null;
            _inputModule = null;
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

        private void OnExitPerformed(InputAction.CallbackContext context)
        {
            ExitClicked?.Invoke();
        }

        private IClickable GetClickable()
        {
            var camera = Camera.main;
            // TODO: Camera service
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

        event Action IInputService.ExitClicked
        {
            add => ExitClicked += value;
            remove => ExitClicked -= value;
        }

        #endregion
    }
}