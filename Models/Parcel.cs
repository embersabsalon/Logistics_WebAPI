using System.ComponentModel.DataAnnotations;

namespace WorldWebServer.Models {
    public class Parcel {
        [Key]
        public int TrackingID { get; set; }
        public string SenderName { get; set; }
        public int SenderNum { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public int ReceiverNum { get; set; }
        public string ReceiverAddress { get; set; }
        public int OriginDistCenterID { get; set; }
        public int NextDistCenterID { get; set; }
        public int FinalDistCenterID { get; set; }
        public string Status { get; set; }
    }
}