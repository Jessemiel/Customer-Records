namespace BcsExamApp.Interfaces
{
    public interface IConnectivityService
    {
        bool IsConnected();
        void CheckConnectivity();
    }
}
