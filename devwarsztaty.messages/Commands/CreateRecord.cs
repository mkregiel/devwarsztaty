using System;
using System.Collections.Generic;
using System.Text;

namespace devwarsztaty.messages.Commands
{
    public class CreateRecord : ICommand
    {
        public string Key { get; set; }

        public object Value { get; set; }
    }
}
