using devwarsztaty.messages.Commands;
using devwarsztaty.messages.Events;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devwarsztaty.service.Handlers
{
    public class CreateRecordHandler : ICommandHandler<CreateRecord>
    {
        private readonly IBusClient _client;

        public CreateRecordHandler(IBusClient client)
        {
            _client = client;
        }

        public async Task HandleAsync(CreateRecord command)
        {
            Console.WriteLine($"Received command with key {command.Key}");

            if (string.IsNullOrEmpty(command.Key))
            {
                await _client.PublishAsync(new CreateRecordFailed(command.Key, "Record key cannot be empty"));
                return;
            }

            await _client.PublishAsync(new RecordCreated(command.Key));
        }
    }
}
