using devwarsztaty.messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devwarsztaty.web.Handlers
{
    public class CreateRecordFailedHandler: IEventHandler<CreateRecordFailed>
    {
        public async Task HandleAsync(CreateRecordFailed @event)
        {
            Console.WriteLine($"FAILED to create Record with key {@event.Key} ! Reason: {@event.Reason}");

            await Task.CompletedTask;
        }
    }
}
