using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using HiddenTest.Level;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    public sealed class LevelScreen : Fragment<LevelScreenModel,  LevelScreenView>, ILevelScreen
    {
        private readonly List<LevelObjectView> _views;
        private readonly Queue<LevelObjectView> _hiddenViews;
        private readonly Func<Transform, LevelObjectView> _viewFactory;

        private IReadOnlyList<ObjectSettings> Objects => Model.Objects;
        private RectTransform ObjectsContainer => View.ObjectsContainer;

        [Preserve]
        public LevelScreen(Func<Transform, LevelObjectView> viewFactory, LevelScreenModel model, LevelScreenView view)
            : base(model, view)
        {
            _views = new List<LevelObjectView>();
            _hiddenViews = new Queue<LevelObjectView>();
            _viewFactory = viewFactory;
        }

        #region ILevelScreen

        void ILevelScreen.SetTimer(float timerSeconds)
        {
            Model.Timer = timerSeconds;
        }

        UniTask ILevelScreen.ShowObjectAsync(int index, CancellationToken cancellationToken)
        {
            var objectSettings = Objects[index];

            if (!_hiddenViews.TryDequeue(out var objectView))
            {
                objectView = _viewFactory.Invoke(ObjectsContainer);
                _views.Add(objectView);
            }

            objectView.Set(objectSettings, index);

            return objectView.ShowAsync(cancellationToken);
        }

        async UniTask ILevelScreen.HideObjectAsync(int index, CancellationToken cancellationToken)
        {
            if (!_views.TryGetFirst(item => item.Index == index, out var view))
            {
                Debug.LogError($"No view found for index {index}");
                return;
            }

            await view.HideAsync(cancellationToken)
                      .SuppressCancellationThrow();
            _hiddenViews.Enqueue(view);
        }

        #endregion
    }
}