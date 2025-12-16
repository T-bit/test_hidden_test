using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.UI
{
    [Serializable]
    public sealed class LevelScreenInstaller : FragmentInstaller<LevelScreen, ILevelScreen, LevelScreenModel, LevelScreenView>
    {
        [SerializeField]
        private LevelObjectView _levelObjectViewPrefab;

        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterFactory(LevelObjectViewFactory, Lifetime.Scoped);
        }

        private Func<Transform, LevelObjectView> LevelObjectViewFactory(IObjectResolver container)
        {
            return transform => container.Instantiate(_levelObjectViewPrefab, transform);
        }
    }
}