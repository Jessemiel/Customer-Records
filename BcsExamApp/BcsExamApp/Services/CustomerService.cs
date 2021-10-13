using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using BcsExamApp.Interfaces;
using System.Threading.Tasks;
using BcsExamApp.Model;
using BcsExamApp.Constants;
using Newtonsoft.Json;
using BcsExamApp.Constants.enums;

namespace BcsExamApp.Services
{
    //Library used:
    //checking network connectivity - Xamarin.Essentials
    //json handling - Newtonsoft.Json    

    public class CustomerService : ICustomerService
    {
        private readonly IConnectivityService _connectivityService;

        public CustomerService(IConnectivityService connectivityService)
        {
            _connectivityService = connectivityService;
        }
        public async Task<GenericResponse<List<Customer>>> GetCustomer(string parkCode, string arrivingDate)
        {            
            var genericResponse = new GenericResponse<List<Customer>>();
            try
            {
                _connectivityService.CheckConnectivity();
                using (var httpClient = new HttpClient())
                {
                    UriBuilder endpoint = new UriBuilder(AppConstant.BASE_URI + Endpoint.GET_CUSTOMER);
                    endpoint.Query = $"parkCode={parkCode}&arriving={arrivingDate}";

                    var response = await httpClient.GetStringAsync(endpoint.Uri);
                    genericResponse.Status = Status.success;

                    if (response == null)
                    {
                        genericResponse.Value = new List<Customer>();
                        return genericResponse;
                    }
                    var ret = JsonConvert.DeserializeObject<List<Customer>>(response);                    
                    genericResponse.Value = ret;
                }
            }
            catch(Exception ex)
            {
                genericResponse.Status = Status.error;
                genericResponse.Detail = ex.Message;
                genericResponse.Value = new List<Customer>();
            }
            return genericResponse;
        }

        public async Task<GenericResponse<bool>> PostResponse(Response response)
        {
            var genericResponse = new GenericResponse<bool>();
            try
            {
                _connectivityService.CheckConnectivity();
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(response);
                    var payload = new StringContent(json, Encoding.UTF8, "application/json");
                    var res = await httpClient.PostAsync(AppConstant.BASE_URI + Endpoint.POST_RESPONSE, payload);
                    if (res.IsSuccessStatusCode)
                    {
                        genericResponse.Status = Status.success;
                    }
                }
            }
            catch (Exception ex)
            {
                genericResponse.Status = Status.error;
                genericResponse.Detail = ex.Message;
                genericResponse.Value = false;
            }
            return genericResponse;
        }
    }
}
