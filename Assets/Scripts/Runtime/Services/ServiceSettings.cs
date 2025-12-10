using System;

namespace HiddenTest.Services
{
    [Serializable]
    public abstract class ServiceSettings
    {
        public Type Type => GetType();
        public abstract Type ServiceType { get; }
    }

    [Serializable]
    public abstract class ServiceSettings<TService> : ServiceSettings
        where TService : Service
    {
        public override Type ServiceType => typeof(TService);
    }
}