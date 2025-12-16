using System;
using HiddenTest.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.UI
{
    [Serializable]
    public abstract class FragmentInstaller<TFragment, TFragmentInterface, TFragmentModel, TFragmentView>: IFragmentInstaller
        where TFragment : Fragment<TFragmentModel, TFragmentView>
        where TFragmentInterface : IFragment
        where TFragmentModel : FragmentModel
        where TFragmentView : FragmentView<TFragmentModel>
    {
        [SerializeField]
        private TFragmentView _viewPrefab;

        private TFragmentInterface GetFragment(IObjectResolver container)
        {
            var uiService =  container.Resolve<IUIService>();
            return uiService.GetFragment<TFragmentInterface>();
        }

        protected virtual void OnInstall(IContainerBuilder builder)
        {
        }

        #region IInstaller

        void IInstaller.Install(IContainerBuilder builder)
        {
            builder.Register<TFragmentModel>(Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_viewPrefab, Lifetime.Scoped);
            builder.Register<TFragment>(Lifetime.Scoped)
                   .As<IFragment>()
                   .AsSelf();
            builder.Register(GetFragment, Lifetime.Scoped);

            OnInstall(builder);
        }

        #endregion
    }
}