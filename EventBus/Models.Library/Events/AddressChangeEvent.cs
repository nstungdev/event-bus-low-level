namespace Models.Library.Events
{
    public class AddressChangeEvent : BaseOrderEvent
    {
        public string OldAddress { get; set; }
        public string NewAddress { get; set; }
    }
}
