using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Level;
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

        private LevelView _levelView;

        public GameStateController(GeneralSettings generalSettings, LevelSettings levelSettings, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            _generalSettings = generalSettings;
            _levelSettings = levelSettings;
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _levelView = Container.Instantiate(_levelSettings.LevelViewPrefab, RootTransform);

            foreach (var objectSettings in _levelSettings.ObjectSettingsList)
            {
                Debug.Log(objectSettings.Id);
            }

            return UniTask.CompletedTask;
        }
    }
}