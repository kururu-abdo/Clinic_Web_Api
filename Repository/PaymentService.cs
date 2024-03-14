using System.Text;
using clinic.Data;
using clinic.Models;
using clinic.Models.Payment.Check.Request;
using clinic.Models.Payment.Check.Response;
using clinic.Models.Payment.Request;
using clinic.Models.Payment.Response;
using clinic.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static clinic.Utils.PaymentConfiguration;

namespace clinic.Repository
{
     public class PaymentService : IPaymentService
    {
        protected ClinicDb _dbContext { get; set; }

        public PaymentService(ClinicDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<string>> SendPaymentRequest(string orderId)
        {
 
            var result = new Response<string>();
            try
            {
                var order = await _dbContext.Bookings
                        .FirstOrDefaultAsync(x => x.Id == Guid.Parse(orderId))
                        
                        ;

                using (var httpClient = new HttpClient())
                {
                    double total =0.0;
                    foreach (var item in order.Schedule.Fees)
                    {
                        total+=item.amount;
                    }
                    httpClient.BaseAddress = new Uri(PaymentConfiguration.HttpClientBaseAddress);
                    httpClient.DefaultRequestHeaders.ExpectContinue = false;
                    string Auth = PaymentConfiguration.StoreId + ":" + PaymentConfiguration.AuthKey;
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(Auth);
                    Auth = Convert.ToBase64String(data);
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);

                    var request = new AddPaymentRequest();
                    request.Method = "create";
                    request.Store = PaymentConfiguration.StoreId;
                    request.Authkey = PaymentConfiguration.AuthKey;
                    request.Framed = PaymentConfiguration.Framed;

                    request.Order.CartId = orderId.ToString();
                    request.Order.Test = PaymentConfiguration.Test;
                    request.Order.Amount = total.ToString();
                    request.Order.Description = "Payment Order";
                    request.Order.Currency = PaymentConfiguration.Currency;

                    request.Return.authorised = PaymentConfiguration.AuthorisedUrl + "?orderId=" + orderId.ToString();
                    request.Return.cancelled = PaymentConfiguration.CancelledUrl + "?orderId=" + orderId.ToString();
                    request.Return.declined = PaymentConfiguration.DeclinedUrl + "?orderId=" + orderId.ToString();

                    var address = new Address
                    {
                        City = order.User.Address ?? string.Empty,
                        Country = order.User.Address ?? string.Empty
                    };
                    var customer = new Customer();
                    customer.Ref = order.User.Id.ToString();
                    customer.Email = order.User.Email;
                    customer.Phone = order.User.Mobile;
                    customer.Address = address;
                    request.Customer = customer;

                    var requestBody = JsonConvert.SerializeObject(request);
                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    var responseJson = (await httpClient.PostAsync(PaymentConfiguration.PostUrl, content));
                    if (responseJson.IsSuccessStatusCode)
                    {
                        var responseContent = await responseJson.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<AddPaymentResponse>(responseContent);
                        if (response.Order != null)
                        {
                            //insert response in local db
                            order.PaymentOrderRef = response.Order.Ref;
                            order.PaymentStatus = (int)PaymentStatusEnum.Pending;
                            order.PaymentUrl = response.Order.Url;

                            _dbContext.Bookings.Update(order);
                            var c = _dbContext.SaveChanges();

                            result.HasErrors = false;
                            result.Result = response.Order.Url;
                        }
                        else
                        {
                            result.HasErrors = true;
                            result.AddValidationError("payment", response.Error.Note);
                        }
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                result.HasErrors = true;
                result.AddValidationError("payment", "Something went wrong");
                return result;
            }
        }

        public async Task<Response<string>> CheckPaymentRequest(string orderId)
        {
 var result = new Response<string>();
            try
            {
                var order = await _dbContext.Bookings
                      .Where(x => x.Id == Guid.Parse(orderId)).FirstOrDefaultAsync();

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(PaymentConfiguration.HttpClientBaseAddress);
                    httpClient.DefaultRequestHeaders.ExpectContinue = false;
                    string Auth = PaymentConfiguration.StoreId + ":" + PaymentConfiguration.AuthKey;
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(Auth);
                    Auth = Convert.ToBase64String(data);
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Auth);

                    var request = new CheckPaymentRequest();
                    request.Method = "check";
                    request.Store = PaymentConfiguration.StoreId;
                    request.AuthKey = PaymentConfiguration.AuthKey;
                    request.Order.Ref = order.PaymentOrderRef;

                    var requestBody = JsonConvert.SerializeObject(request);
                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    var responseJson = (await httpClient.PostAsync(PaymentConfiguration.PostUrl, content));
                    if (responseJson.IsSuccessStatusCode)
                    {
                        var responseContent = await responseJson.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<CheckPaymentResponse>(responseContent);

                        if (response.Error == null)
                        {
                            //update response in local table
                            order.PaymentStatus = Convert.ToInt32(response.Order.Status.Code);
                            _dbContext.Bookings.Update(order);
                            var c = _dbContext.SaveChanges();

                            result.HasErrors = c <= 0;
                            result.Result = response.Order.Status.Text;
                        }
                        else
                        {
                            result.HasErrors = true;
                            result.AddValidationError("payment", response.Error.Note);
                        }
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                result.HasErrors = true;
                result.AddValidationError("payment", "Something Went Wrong");
                return result;
            }
        }
    }
}