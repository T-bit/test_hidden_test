namespace HiddenTest.UI
{
    public interface IMessageWindow : IFragment<MessageWindowModel, MessageWindowView>
    {
        void SetMessage(string message);
    }
}