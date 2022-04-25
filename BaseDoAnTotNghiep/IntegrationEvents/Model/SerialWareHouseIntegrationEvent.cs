namespace KafKa.Net.IntegrationEvents
{
    public partial class SerialWareHouseIntegrationEvent: BaseIntegrationEvent
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string InwardDetailId { get; set; }
        public string OutwardDetailId { get; set; }
        public bool IsOver { get; set; }

    }
}
