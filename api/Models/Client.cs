// {"id": 1, "name": "Raymond Inc", "address": "1296 Daniel Road Apt. 349", "city": "Pierceview", "zip_code": "28301", "province": "Colorado",
// "country": "United States", "contact_name": "Bryan Clark", "contact_phone": "242.732.3483x2573", "contact_email": "robertcharles@example.net", "created_at": "2010-04-28 02:22:53", "updated_at": "2022-02-09 20:22:35"}

namespace api.Models
{
    public class Client
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string Address { get; set;}
        public string City { get; set;}
        public string Zip_code { get; set;}
        public string Province { get; set;}
        public string Country { get; set;}
        public string ContactName { get; set;}
        public string ContactEmail { get; set;}
        public DateTime CreatedAt { get; set;}
        public DateTime UpdatedAt { get; set;}
    }
}
