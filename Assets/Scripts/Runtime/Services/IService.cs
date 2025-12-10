using System;
using VContainer.Unity;

namespace HiddenTest.Services
{
    public interface IService : IAsyncStartable, IDisposable
    {
    }
}