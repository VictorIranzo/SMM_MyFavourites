using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Minutes
{
    public interface IEntryStore
    {
        Task<List<Entry>> ReadAsync();
        Task WriteAsync(Entry entry);
        Task DeleteAsycn(Entry entry);
    }
}
