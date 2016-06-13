using Marvin.Core.Models;

namespace Marvin.Core.Module
{
    public interface IAction
    {
        string GetName();
        string GetAction();
        string GetDescription();
        ActionMessage GetSample();

        void Execute(ActionMessage msg);
    }
}
