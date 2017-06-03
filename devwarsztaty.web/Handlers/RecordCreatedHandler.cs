using devwarsztaty.messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devwarsztaty.web.Handlers
{
    public class RecordCreatedHandler : IEventHandler<RecordCreated>
    {
        private readonly IStorage _storage;

        public RecordCreatedHandler(IStorage storage)
        {
            _storage = storage;
        }

        public async Task HandleAsync(RecordCreated @event)
        {
            Console.WriteLine($"Record with key {@event.Key} has been created !");

            _storage.Add(@event.Key);

            await Task.CompletedTask;
        }
    }
}
