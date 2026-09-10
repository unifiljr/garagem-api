using System.Text.Json.Serialization;

namespace GaragemAPI.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public decimal Preco { get; set; }
        public int StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }
    }
}
