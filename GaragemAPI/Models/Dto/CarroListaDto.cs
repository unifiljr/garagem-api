namespace GaragemAPI.Models.Dto
{
    public class CarroListaDto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public decimal Preco { get; set; }

        public int StatusId { get; set; }
        public string StatusDescricao { get; set; }
    }
}
