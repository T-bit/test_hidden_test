using System;
using HiddenTest.Input;

namespace HiddenTest.Services
{
    public interface IInputService : IService
    {
        event Action<IClickable> ClickableClicked;
    }
}