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

        private float _timer;
        private int _currentIndex;
        private CancellationTokenSource _cancellationTokenSource;

        private string WinMessage => Settings.WinMessage;
        private string LooseMessage => Settings.LooseMessage;

        private CancellationToken CancellationToken => _cancellationTokenSource.Token;

        public LevelService(ILevelScreen levelScreen, IInputService inputService, LevelModule levelModule, LevelServiceSettings settings, Transform rootTransform, IObjectResolver container)
            : base(settings, rootTransform, container)
        {
            _levelScreen = levelScreen;
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

                    if (i > 2)
                    {
                        continue;
                    }

                    _currentIndex++;
                    tasks.Add(_levelScreen.ShowObjectAsync(i, cancellationToken));
                }

                foreach (var objectView in objectViews)
                {
                    Debug.LogWarning($"Couldn't find object settings with id {objectView.Id}");
                }

                tasks.Add(_levelScreen.ShowAsync(cancellationToken));

                await tasks.WhenAll();
            }

            _inputService.ClickableClicked += OnClickableClicked;
            _timer = Settings.TimerSeconds;
        }

        protected override void OnDispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _inputService.ClickableClicked -= OnClickableClicked;
            _objects.Clear();
        }

        private void OnClickableClicked(IClickable clickable)
        {
            if (clickable is not ObjectView objectView)
            {
                return;
            }

            var index = _objects.FindIndex(item => item.Id == objectView.Id);

            if (index < 0 || index > _currentIndex)
            {
                return;
            }

            _currentIndex++;

            SetNextAsync(CancellationToken).Forget();

            return;

            async UniTask SetNextAsync(CancellationToken cancellationToken)
            {
                // TODO: Rework
                await UniTask.WhenAll(objectView.HideAsync(cancellationToken),
                    _levelScreen.HideObjectAsync(index, cancellationToken));

                if (_currentIndex >= Settings.ObjectSettingsList.Count + 2)
                {
                    // TODO Win message
                    Debug.Log(WinMessage);
                }
                else if (_currentIndex < Settings.ObjectSettingsList.Count)
                {
                    await _levelScreen.ShowObjectAsync(_currentIndex, cancellationToken);
                }
            }
        }

        #region ITickable

        void ITickable.Tick()
        {
            if (_timer <= 0)
            {
                return;
            }

            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = 0;
                // TODO: Loose message
                Debug.Log(LooseMessage);
            }

            _levelScreen.SetTimer(_timer);
        }

        #endregion
    }
}