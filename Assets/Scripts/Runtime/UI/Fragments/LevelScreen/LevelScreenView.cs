using TMPro;
using UnityEngine;

namespace HiddenTest.UI
{
    public sealed class LevelScreenView : FragmentView<LevelScreenModel>
    {
        [SerializeField]
        private TMP_Text _timerText;

        private float Timer => Model.Timer;
        private int Minutes => (int)(Timer / 60);
        private int Seconds => (int)(Timer % 60);

        #region Unity

        private void Update()
        {
            _timerText.text = $"{Minutes:D2}:{Seconds:D2}";
        }

        #endregion
    }
}