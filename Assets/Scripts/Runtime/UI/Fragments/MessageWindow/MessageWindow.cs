using JetBrains.Annotations;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    public sealed class MessageWindow : Fragment<MessageWindowModel, MessageWindowView>, IMessageWindow
    {
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