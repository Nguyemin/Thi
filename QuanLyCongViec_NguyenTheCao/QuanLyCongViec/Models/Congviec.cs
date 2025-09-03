using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCongViec.Models
{
    public class Congviec
    {
        [Key]
        public int Id { get; set; }   
        public string TenCongViec { get; set; }
        public string Tag { get; set; }
        public bool TrangThai { get; set; }
        
    }
}

