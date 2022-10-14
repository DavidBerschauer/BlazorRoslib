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

        public async Task<T?> GetItem<T>(string key)
        {
            try {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

                if (json == null)
                    return default;

                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default;
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