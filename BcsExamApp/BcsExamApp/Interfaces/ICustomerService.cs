using BcsExamApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BcsExamApp.Interfaces
{
    public interface ICustomerService 
    {
        Task<GenericResponse<List<Customer>>> GetCustomer(string parkCode, string arrivingDate);
        Task<GenericResponse<bool>> PostResponse(Response response);

    }
}
