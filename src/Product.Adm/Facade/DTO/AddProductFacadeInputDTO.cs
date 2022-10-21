namespace Product.Adm.Facade.DTO
{
    public abstract class AddProductFacadeInputDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public long PurchasePrice { get; set; }
        public long Stock { get; set; }
    }
}
