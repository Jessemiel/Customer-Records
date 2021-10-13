using BcsExamApp.Interfaces;
using System;
using Xamarin.Essentials;

namespace BcsExamApp.Services
{
    public class ConnectivityService : IConnectivityService 
    {
        public void CheckConnectivity()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new Exception("No Internet Connection");
        }
        public bool IsConnected()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return false;
            }
            return true;
        }
    }
}
