using HiddenTest.StateControllers;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.LifetimeScopes
{
    public abstract class StateLifetimeScope<TStateController> : LifetimeScope
        where TStateController : StateController
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TStateController>();
        }
    }
}