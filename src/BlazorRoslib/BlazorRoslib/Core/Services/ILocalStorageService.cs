using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorRoslib.Core.Services
{
    public interface ILocalStorageService
    {
        Task<T?> GetItem<T>(string key);
        Task<T?> GetItem<T>(string key, T? defaultValue);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }
}
