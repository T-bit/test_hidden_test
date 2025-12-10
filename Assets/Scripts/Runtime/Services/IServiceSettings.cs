using System;

namespace HiddenTest.Services
{
    public interface IServiceSettings
    {
        Type Type { get; }

        Type  ServiceType { get; }
    }
}