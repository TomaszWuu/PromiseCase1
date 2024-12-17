using RestLibrary.Exception;
using RestLibrary.Interfaces;
using RestLibrary.Model;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;


namespace RestLibrary;

public class RestLibrary : IRestLibrary
{
    private HttpClient _httpClient;

    public HttpClient HttpClient   
    {
        get { return _httpClient; } 
        set { _httpClient = value; }
    }
       
    public RestLibrary(string baseUrl, string bearerToken)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        var response = await _httpClient.GetAsync("/api/books");
        if (!response.IsSuccessStatusCode)
        {
            throw new RestClientException("Failed to fetch books", response.StatusCode);
        }
        var content = await response.Content.ReadAsStringAsync();
             
        return JsonSerializer.Deserialize<IEnumerable<Book>>(content);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        int page = 1;
        List<Order> allOrders = new();


        while (true)
        {
            HttpResponseMessage? response = null;
            
            response = await _httpClient.GetAsync($"http://api/orders"); 
                        
            if (!response.IsSuccessStatusCode)
            {
                throw new RestClientException("Failed to fetch orders", response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(content);

            if (orders == null || !orders.Any())
            {
                break;
            }

            allOrders.AddRange(orders);
            page++;
        }
        return allOrders;
    }

    public async Task AddBookAsync(Book book)
    {
        var payload = JsonSerializer.Serialize(book);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/books", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new RestClientException("Failed to add book", response.StatusCode);
        }
    }
}