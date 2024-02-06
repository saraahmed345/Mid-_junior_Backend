namespace Mid_Junior_backend
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Deposit { get; set; }
        public string Role { get; set; }
        
        public Dictionary<int, PurchaseItem> CartItems { get; set; } 
    }
}
