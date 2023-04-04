using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.MessageService
{
    public class SMSService : ISMSService
    {
        public async Task<string> SendMessageAsync(SmsModel smsModel)
        {
            var baseAddress = new Uri("https://www.msegat.com");

            using var httpClient = new HttpClient { BaseAddress = baseAddress };
            {
                using (var content = new StringContent(@"{
                  ""userName"": ""takidco2022"",
                  ""numbers"":  """ + smsModel.Numbers + @""",
                  ""userSender"": ""takid"",
                  ""apiKey"": ""a986ac7270d210d686ec337987b7d1aa"",
                  ""msg"": """ + smsModel.Msg + @""",
                  ""msgEncoding"":""UTF8""
                }
            ", System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("/gw/sendsms.php", content))
                    {
                        string responseHeaders = response.Headers.ToString();
                        string responseData = await response.Content.ReadAsStringAsync();
                        return responseData;
                    }
                }
            }
        }
    }

}
