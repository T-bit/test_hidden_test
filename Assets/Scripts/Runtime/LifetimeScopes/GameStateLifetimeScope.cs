using HiddenTest.Level;
using HiddenTest.StateControllers;
using UnityEngine;
using VContainer;

namespace HiddenTest.LifetimeScopes
{
    public sealed class GameStateLifetimeScope : StateLifetimeScope<GameStateController>
    {
        [SerializeField]
        private GameSettings _gameSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings.GeneralSettings);
            builder.RegisterInstance(_gameSettings.LevelSettings);

            base.Configure(builder);
        }
    }
}