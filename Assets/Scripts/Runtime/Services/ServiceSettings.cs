using System;

namespace HiddenTest.Services
{
    [Serializable]
    public abstract class ServiceSettings : IServiceSettings
    {
        protected abstract Type Type { get; }
        protected abstract Type ServiceType { get; }

        #region IServiceSettings

        Type IServiceSettings.Type => Type;
        Type IServiceSettings.ServiceType => ServiceType;

        #endregion
    }

    [Serializable]
    public abstract class ServiceSettings<TService, TInterface> : ServiceSettings
        where TService : Service
    {
        protected override Type Type => typeof(TInterface);
        protected override Type ServiceType => typeof(TService);
    }
}