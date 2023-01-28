using System.ComponentModel.DataAnnotations;

namespace Agency.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Key { get; set; }
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
