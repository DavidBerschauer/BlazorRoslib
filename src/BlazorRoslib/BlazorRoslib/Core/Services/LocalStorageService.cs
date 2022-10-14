using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorRoslib.Core.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Task<T?> GetItem<T>(string key)
        {
            return GetItem<T>(key, default);
        }

        public async Task<T?> GetItem<T>(string key, T? defaultValue)
        {
            try {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

                if (json == null)
                    return defaultValue;

                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return defaultValue;
            }
        }

        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
            Console.WriteLine($"Item set: {key} : {value}");
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}