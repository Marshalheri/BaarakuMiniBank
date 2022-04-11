using BaarakuMiniBankAPIs.Middleware.Core.Services;
using System.Collections.Generic;

namespace BaarakuMiniBankAPIs.Middleware.Core.Fakes
{
    public class FakeMessagePackProvider : IMessagePackProvider
    {
        readonly IDictionary<int, MessagePack> _packs;
        public MessagePack GetPack()
        {
            return (_packs.TryGetValue(1, out var pack)) ? pack : null;
        }

        public FakeMessagePackProvider()
        {
            _packs = new Dictionary<int, MessagePack>(1)
            {
                { 1, GetMessagePack() },
            };
        }

        private MessagePack GetMessagePack()
        {
            var pack = new MessagePack("We are currently experiencing network issues. Please try again later.")
            {
                Mappings = new Dictionary<string, string>
                {
                    ["BMB001"] = "Invalid Request",
                    ["BMB002"] = "No request body was found.",
                    ["BMB003"] = "Invalid account number supplied.",
                    ["BMB004"] = "Unable to carry out transaction on account.",


                    ["BMB999"] = "Opps, something went wrong. This is on us, please try again",
                },

            };

            return pack;
        }
    }
}
