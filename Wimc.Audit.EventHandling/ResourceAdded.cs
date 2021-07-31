namespace Wimc.Audit.EventHandling
{
    public class ResourceAdded
    {
        public int ResourceContainerId { get; set; }
        public string CloudId { get; set; }
        public string Name { get; set; }
    }
}