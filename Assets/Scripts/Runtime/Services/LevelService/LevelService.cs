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
        private readonly List<ObjectSettings> _objectSettingsList;
        private readonly LevelModule _levelModule;
        private readonly ILevelScreen _levelScreen;

        private float _timer;

        private string WinMessage => Settings.WinMessage;
        private string LooseMessage => Settings.LooseMessage;

        private LevelScreenModel LevelScreenModel => _levelScreen.Model;

        public LevelService(ILevelScreen levelScreen, IInputService inputService, LevelModule levelModule, LevelServiceSettings settings, Transform rootTransform, IObjectResolver container)
            : base(settings, rootTransform, container)
        {
            _levelScreen = levelScreen;
            _inputService = inputService;
            _levelModule = levelModule;
            _objectSettingsList = new List<ObjectSettings>();
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            using (ListScope<ObjectView>.Create(out var objectViews))
            {
                _levelModule.ObjectViews.ToList(objectViews);

                foreach (var objectSettings in Settings.ObjectSettingsList)
                {
                    if (!objectViews.TryGetFirst(item => item.Id == objectSettings.Id, out var objectView))
                    {
                        Debug.LogWarning($"Couldn't find object view with id {objectSettings.Id}");
                        continue;
                    }

                    objectViews.Remove(objectView);
                    _objectSettingsList.Add(objectSettings);

                    // TODO: UI Initialization
                }

                foreach (var objectView in objectViews)
                {
                    Debug.LogWarning($"Couldn't find object settings with id {objectView.Id}");
                }
            }

            _inputService.ClickableClicked += OnClickableClicked;
            _timer = Settings.TimerSeconds;

            return _levelScreen.ShowAsync(cancellationToken);
        }

        protected override void OnDispose()
        {
            _inputService.ClickableClicked -= OnClickableClicked;
            _objectSettingsList.Clear();
        }

        private void OnClickableClicked(IClickable clickable)
        {
            if (clickable is ObjectView objectView)
            {
                Debug.Log($"Click object {objectView.Id}");
            }
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = 0;
                // TODO: Loose message
            }

            LevelScreenModel.Timer = _timer;
        }
    }
}