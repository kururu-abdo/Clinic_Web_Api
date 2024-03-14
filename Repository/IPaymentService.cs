
using clinic.Utils;

namespace clinic.Repository
{
     public interface IPaymentService
    {
        Task<Response<string>> SendPaymentRequest(string orderId);
        Task<Response<string>> CheckPaymentRequest(string orderId);
    }
}