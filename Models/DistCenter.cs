using System.ComponentModel.DataAnnotations;

namespace WorldWebServer.Models {
    public class DistCenter {
        [Key]
        public int DistCenterID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}