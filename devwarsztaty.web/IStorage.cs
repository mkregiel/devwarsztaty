using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devwarsztaty.web
{
    public interface IStorage
    {
        void Add(string key);

        IEnumerable<string> GetAll();
    }

    public class InMemoryDb : IStorage
    {
        private readonly List<string> addedKeys = new List<string>();

        public void Add(string key)
        {
            addedKeys.Add(key);
        }

        public IEnumerable<string> GetAll() => addedKeys;
    }
}
