namespace EditoraBLL.Models
{
    public class AutorAsset
    {
        public int Id { get; set; } // Primary key

        public int? AutorId { get; set; } // Foreign key
        public Autor Autor { get; set; } // Reference navigation
    }

}
