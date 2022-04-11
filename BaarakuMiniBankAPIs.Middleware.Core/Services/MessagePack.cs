using System.Collections.Generic;
namespace BaarakuMiniBankAPIs.Middleware.Core.Services
{
    public class MessagePack
    {
        public MessagePack(string defaultMessage)
        {
            DefaultMessage = defaultMessage;
        }

        public string DefaultMessage { get; set; }
        public IDictionary<string, string> Mappings { get; set; }
    }
}
