using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using HiddenTest.Input;
using HiddenTest.Level;
using HiddenTest.Scopes;
using HiddenTest.UI;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class LevelService : Service<LevelServiceSettings>,  ILevelService, ITickable
    {
        private readonly IInputService _inputService;
        private readonly List<ObjectSettings> _objects;
        private readonly LevelModule _levelModule;
        private readonly ILevelScreen _levelScreen;
        private readonly IMessageWindow _messageWindow;

        private float _timer;
        private int _nextIndex;
        private CancellationTokenSource _cancellationTokenSource;

        private int ShowObjectCount => Settings.ShowObjectCount;
        private string WinMessage => Settings.WinMessage;
        private string LooseMessage => Settings.LooseMessage;

        private CancellationToken CancellationToken => _cancellationTokenSource.Token;

        private bool AllFound => _nextIndex >= _objects.Count + ShowObjectCount;
        private bool LevelObjectViewNeeded => _nextIndex <= _objects.Count;
        private bool GameFinished => AllFound || (Settings.TimerSeconds > 0 && _timer <= 0);

        public LevelService(ILevelScreen levelScreen, IMessageWindow messageWindow, IInputService inputService, LevelModule levelModule, LevelServiceSettings settings, Transform rootTransform,
            IObjectResolver container)
            : base(settings, rootTransform, container)
        {
            _levelScreen = levelScreen;
            _messageWindow = messageWindow;
            _inputService = inputService;
            _levelModule = levelModule;
            _objects = new List<ObjectSettings>();
        }

        protected override async UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _levelScreen.Model.Objects = _objects;
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            using (ListScope<UniTask>.Create(out var tasks))
            using (ListScope<ObjectView>.Create(out var objectViews))
            {
                _levelModule.ObjectViews.ToList(objectViews);

                for (var i = 0; i < Settings.ObjectSettingsList.Count; i++)
                {
                    var objectSettings = Settings.ObjectSettingsList[i];

                    if (!objectViews.TryGetFirst(item => item.Id == objectSettings.Id, out var objectView))
                    {
                        Debug.LogWarning($"Couldn't find object view with id {objectSettings.Id}");
                        continue;
                    }

                    objectViews.Remove(objectView);
                    _objects.Add(objectSettings);

                    if (i >= ShowObjectCount)
                    {
                        continue;
                    }

                    tasks.Add(_levelScreen.ShowObjectAsync(i, cancellationToken));
                    _nextIndex++;
                }

                foreach (var objectView in objectViews)
                {
                    Debug.LogWarning($"Couldn't find object settings with id {objectView.Id}");
                }

                tasks.Add(_levelScreen.ShowAsync(cancellationToken));

                await tasks.WhenAll();
            }

            _inputService.ClickableClicked += OnClickableClicked;
            _inputService.ExitClicked += OnExitClicked;
            _timer = Settings.TimerSeconds;
        }

        protected override void OnDispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _inputService.ClickableClicked -= OnClickableClicked;
            _inputService.ExitClicked -= OnExitClicked;
            _objects.Clear();
        }

        private void OnClickableClicked(IClickable clickable)
        {
            if (GameFinished || clickable is not ObjectView objectView)
            {
                return;
            }

            var index = _objects.FindIndex(item => item.Id == objectView.Id);

            if (index < 0 || index >= _nextIndex)
            {
                return;
            }

            _nextIndex++;

            SetNextAsync(CancellationToken).Forget();

            return;

            async UniTask SetNextAsync(CancellationToken cancellationToken)
            {
                // TODO: Rework
                await UniTask.WhenAll(objectView.HideAsync(cancellationToken),
                    _levelScreen.HideObjectAsync(index, cancellationToken));

                if (AllFound)
                {
                    _messageWindow.SetMessage(WinMessage);
                    _messageWindow.ShowAsync(CancellationToken).Forget();
                }
                else if (LevelObjectViewNeeded)
                {
                    await _levelScreen.ShowObjectAsync(_nextIndex - 1, cancellationToken);
                }
            }
        }

        private void OnExitClicked()
        {
            // TODO: Move to StateService or GameService
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #region ITickable

        void ITickable.Tick()
        {
            if (AllFound || _timer <= 0)
            {
                return;
            }

            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = 0;

                _messageWindow.SetMessage(LooseMessage);
                _messageWindow.ShowAsync(CancellationToken).Forget();
            }

            _levelScreen.SetTimer(_timer);
        }

        #endregion
    }
}