using JetBrains.Annotations;
using VContainer;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    public sealed class MessageWindow : Fragment<MessageWindowModel, MessageWindowView>, IMessageWindow
    {
        [Preserve]
        public MessageWindow(MessageWindowModel model, MessageWindowView view)
            : base(model, view)
        {
        }

        #region IMessageWindow

        void IMessageWindow.SetMessage(string message)
        {
            Model.Message = message;
        }

        #endregion
    }
}