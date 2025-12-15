using UnityEngine;
using VContainer.Unity;

namespace HiddenTest.Services
{
    public interface IServiceInstaller : IInstaller
    {
        void SetRootTransform(Transform rootTransform);
    }
}