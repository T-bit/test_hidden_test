using UnityEngine;

namespace HiddenTest.UI
{
    public sealed class LevelScreenView : FragmentView<LevelScreenModel>
    {
        private float Timer => Model.Timer;

        #region Unity

        private void Update()
        {
            Debug.Log(Timer);
        }

        #endregion
    }
}