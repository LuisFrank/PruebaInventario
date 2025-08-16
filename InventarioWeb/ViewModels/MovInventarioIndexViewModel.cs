namespace InventarioWeb.ViewModels
{
    public class MovInventarioIndexViewModel
    {
        public MovInventarioSearchViewModel SearchModel { get; set; } = new();
        public List<MovInventarioViewModel> MovInventarios { get; set; } = new();
    }
}