namespace CargoHub.Models
{
    public class ApiKeyInfo
    {
        public int Id { get; set; }
        public string ApiKey { get; set; }
        public List<string> AllowedRoutes { get; set; } // List of allowed endpoints or permissions for this key
    }
}

