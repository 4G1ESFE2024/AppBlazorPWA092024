using AppBlazorPWA.Data.DTOs;
using System.Net.Http.Json;

namespace AppBlazorPWA.Data.Services
{
    public class PostService
    {
        // Cliente HTTP privado y de solo lectura para realizar solicitudes.
        private readonly HttpClient _httpClient;

        // Constructor del servicio que recibe una fábrica de clientes HTTP.
        public PostService(IHttpClientFactory httpClientFactory)
        {
            // Crea un cliente HTTP utilizando la fábrica y un nombre específico de cliente.
            _httpClient = httpClientFactory.CreateClient("JsonPlaceholder_API");
        }

        // Método asíncrono para obtener una lista de posts.
        public async Task<List<PostDTO>> GetPostsAsync()
        {
            // Realiza una solicitud GET para obtener la lista de posts en formato JSON.
            var lista = await _httpClient.GetFromJsonAsync<List<PostDTO>>("posts");

            // Devuelve la lista obtenida o una nueva lista vacía si es nula.
            if (lista != null)
                return lista;
            else
                return new List<PostDTO>();
        }

        // Método asíncrono para crear un nuevo post.
        public async Task<PostDTO> CreatePostAsync(PostDTO post)
        {
            // Realiza una solicitud POST para crear un nuevo post en formato JSON.
            var response = await _httpClient.PostAsJsonAsync("posts", post);

            // Asegura que la respuesta sea exitosa; lanza una excepción si no lo es.
            response.EnsureSuccessStatusCode();

            // Lee la respuesta en formato JSON y la convierte a un objeto PostDTO.
            var postResult = await response.Content.ReadFromJsonAsync<PostDTO>();

            // Devuelve el post creado o un nuevo PostDTO si es nulo.
            if (postResult != null)
                return postResult;
            else
                return new PostDTO();
        }
    }
}
