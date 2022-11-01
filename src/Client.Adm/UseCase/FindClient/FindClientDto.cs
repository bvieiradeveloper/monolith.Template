namespace Client.Adm.UseCase.FindClient
{
    public class FindClientInputDto
    {
        public string ClientId { get; init; }
    }

    public class FindClientOutputDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
