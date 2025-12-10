using UnityEngine;

namespace HiddenTest.Level
{
    [CreateAssetMenu(menuName = "HiddenTest/GameSettings",  fileName = "GameSettings")]
    public sealed class GameSettings : ScriptableObject
    {
        [SerializeField]
        private GeneralSettings _generalSettings;

        [SerializeField]
        private LevelSettings _levelSettings;

        public GeneralSettings GeneralSettings => _generalSettings;

        public LevelSettings LevelSettings => _levelSettings;
    }
}