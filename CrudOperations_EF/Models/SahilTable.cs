using System.ComponentModel.DataAnnotations;

namespace CrudOperations_EF.Models
{
    public class SahilTable
    {
        [Key] public int rowval { get; set; }
        public string Name { get; set; }
        public string Sirname { get; set; }
    }
}
