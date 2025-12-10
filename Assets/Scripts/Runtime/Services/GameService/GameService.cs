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

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class GameService : Service<IGameServiceSettings>,  IGameService
    {
        private readonly List<ObjectSettings> _objectSettingsList;
        private LevelView _levelView;

        private float TimerSeconds => Settings.TimerSeconds;
        private string WinMessage => Settings.WinMessage;
        private string LooseMessage => Settings.LooseMessage;

        public GameService(IGameServiceSettings settings, Transform rootTransform, IObjectResolver container)
            : base(settings, rootTransform, container)
        {
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

            return UniTask.CompletedTask;
        }
    }
}