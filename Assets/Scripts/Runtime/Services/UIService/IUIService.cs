using HiddenTest.UI;

namespace HiddenTest.Services
{
    public interface IUIService : IService
    {
        TFragment GetFragment<TFragment>()
            where TFragment : IFragment;
    }
}