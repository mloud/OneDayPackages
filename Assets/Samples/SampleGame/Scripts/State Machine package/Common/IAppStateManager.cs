namespace OneDay.StateMachine.Common
{
    public interface IAppStateManager
    {
        void Run();
        void MakeTransition(string transition);
    }
}