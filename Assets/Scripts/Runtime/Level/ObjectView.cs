using UnityEngine;

namespace HiddenTest.Level
{
    public sealed class ObjectView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        // That'll work for now
        public string Id => _spriteRenderer?.sprite?.name;

        public void Hide()
        {
            // TODO
        }
    }
}