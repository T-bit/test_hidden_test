using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using HiddenTest.Input;
using HiddenTest.Level;
using HiddenTest.Scopes;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class GameService : Service<GameServiceSettings>,  IGameService
    {
        private readonly IInputService _inputService;
        private readonly List<ObjectSettings> _objectSettingsList;
        private LevelView _levelView;

        private float TimerSeconds => Settings.TimerSeconds;
        private string WinMessage => Settings.WinMessage;
        private string LooseMessage => Settings.LooseMessage;

        public GameService(IInputService inputService, GameServiceSettings settings, Transform rootTransform, IObjectResolver container)
            : base(settings, rootTransform, container)
        {
            _inputService = inputService;
            _objectSettingsList = new List<ObjectSettings>();
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _levelView = Container.Instantiate(Settings.LevelViewPrefab, RootTransform);

            using (ListScope<ObjectView>.Create(out var objectViews))
            {
                _levelView.ObjectViews.ToList(objectViews);

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

            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {
            _inputService.ClickableClicked -= OnClickableClicked;
        }

        private void OnClickableClicked(IClickable clickable)
        {
            if (clickable is ObjectView objectView)
            {
                Debug.Log($"Click object {objectView.Id}");
            }
        }
    }
}