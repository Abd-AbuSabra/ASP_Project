namespace ASP_Project.Models.ViewModels
{
    public class FindBook
    {
        public string? Title { get; set; }
        public double? BookPrice { get; set; }
        public IEnumerable<EBook> EBooks { get; set; } = Enumerable.Empty<EBook>();
    }
}
