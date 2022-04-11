using Newtonsoft.Json;
using System;
using System.Text;

namespace BaarakuMiniBankAPIs.Middleware.Core.Services
{
    public static class Util
    {
        static readonly JsonSerializerSettings settings = new() { MissingMemberHandling = MissingMemberHandling.Ignore };
        public static string SerializeAsJson<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public static T DeserializeFromJson<T>(string input)
        {

            return JsonConvert.DeserializeObject<T>(input, settings);
        }

        public static string GenerateNumbers(int numberLength, int minimumNumber, int maxNumber)
        {
            StringBuilder builder = new();
            Random rnd = new();
            for (int i = 0; i < numberLength; i++)
            {
                var num = rnd.Next(minimumNumber, maxNumber);
                builder.Append(num);
            }
            var number = builder.ToString();
            return number;
        }
    }
}
