using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using HiddenTest.Level;
using HiddenTest.Scopes;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.StateControllers
{
    [UsedImplicitly]
    public sealed class GameStateController : StateController
    {
        private readonly GeneralSettings _generalSettings;
        private readonly LevelSettings _levelSettings;
        private readonly List<ObjectSettings> _objectSettingsList;
        private LevelView _levelView;

        private float TimerSeconds => _generalSettings.TimerSeconds;
        private string WinMessage => _generalSettings.WinMessage;
        private string LooseMessage => _generalSettings.LooseMessage;

        public GameStateController(GeneralSettings generalSettings, LevelSettings levelSettings, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            _generalSettings = generalSettings;
            _levelSettings = levelSettings;
            _objectSettingsList = new List<ObjectSettings>();
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _levelView = Container.Instantiate(_levelSettings.LevelViewPrefab, RootTransform);

            using (ListScope<ObjectView>.Create(out var objectViews))
            {
                _levelView.ObjectViews.ToList(objectViews);

                foreach (var objectSettings in _levelSettings.ObjectSettingsList)
                {
                    if (!objectViews.TryGetFirst(item => item.Id == objectSettings.Id, out var objectView))
                    {
                        Debug.LogWarning($"Couldn't find object view with id {objectSettings.Id}");
                        continue;
                    }

                    _objectSettingsList.Add(objectSettings);

                    // TODO: UI Initialization
                }

                foreach (var objectView in objectViews)
                {
                    Debug.LogWarning($"Couldn't find object settings with id {objectView.Id}");
                }
            }

            return UniTask.CompletedTask;
        }
    }
}