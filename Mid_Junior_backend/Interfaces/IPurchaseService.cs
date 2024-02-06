namespace Mid_Junior_backend.Interfaces
{
    public interface IPurchaseService
    {
       
            PurchaseResult BuyProduct(User user, int productId, int quantity);
            IQueryable<product> GetProducts();
            product Find(int id);
            void DepositCoins(User user, int[] coinValues);
            void ResetDeposit(User user);
            Dictionary<int, int> CalculateChange(decimal amount);
            Dictionary<int, int> GetChangeAmount(string userName);
            bool CheckProductAvailability(int productId);
            void AddToCart(string userName, int productId, int quantity);


    }
}
