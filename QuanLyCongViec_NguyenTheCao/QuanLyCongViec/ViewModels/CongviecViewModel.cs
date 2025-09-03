using QuanLyCongViec.DTO;

namespace QuanLyCongViec.ViewModels
{
    public class CongviecViewModel
    {
        public List<CongViecDTO> CongViecList { get; set; }
        public CongViecDTO Request { get; set; }
        public CongViecDTO Response { get; set; }
    }
}
