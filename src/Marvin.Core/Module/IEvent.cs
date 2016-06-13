namespace Marvin.Core.Module
{
    public interface IEvent
    {
        string GetName();
        string GetEvent();
        string GetDescription();
    }
}
