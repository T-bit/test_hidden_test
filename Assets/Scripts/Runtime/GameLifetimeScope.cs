using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameSettings _gameSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var serviceInstaller in _gameSettings.ServiceInstallers)
            {
                serviceInstaller.SetRootTransform(transform);
                serviceInstaller.Install(builder);
            }
        }
    }
}