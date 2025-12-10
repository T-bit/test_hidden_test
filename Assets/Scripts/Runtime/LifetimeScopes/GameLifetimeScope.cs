using HiddenTest.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.LifetimeScopes
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameSettings _gameSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var serviceSettings in _gameSettings.ServiceSettingsList)
            {
                builder.RegisterInstance(serviceSettings).As(serviceSettings.Type);
                builder.Register(serviceSettings.ServiceType, Lifetime.Scoped)
                       .WithParameter(transform)
                       .AsImplementedInterfaces();
            }
        }
    }
}