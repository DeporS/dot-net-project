namespace DAOMock
{
    public class Producer: Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Shoe>? Shoes { get; set; }
    }
}
