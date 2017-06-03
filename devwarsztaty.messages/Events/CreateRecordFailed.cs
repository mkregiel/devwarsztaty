using System;
using System.Collections.Generic;
using System.Text;

namespace devwarsztaty.messages.Events
{
    public class CreateRecordFailed : IEvent
    {
        public string Key { get; }

        public string Reason { get; }

        protected CreateRecordFailed()
        {

        }

        public CreateRecordFailed(string key, string reason)
        {
            Key = key;
            Reason = reason;
        }
    }
}
