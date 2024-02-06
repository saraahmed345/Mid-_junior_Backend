namespace Mid_Junior_backend
{
    public class PurchaseItem
    {
        public product Product { get; set; }
        public int Quantity { get; set; }
        
    }

    public class PurchaseResult 
    {
        public decimal TotalSpent { get; set; }
        public List<PurchaseItem> ItemsPurchased { get; set; }
        public Dictionary<int, int> ChangeCoins { get; set; } // Key: Coin value (5, 10, 20, 50, 100).
                                                              // Value: Number of coins.

       
    }
}
