using Microsoft.EntityFrameworkCore;
using Mid_Junior_backend.Interfaces;

namespace Mid_Junior_backend
{
    
   
    public class PurchaseService 
    {
        private readonly DbContext _context; // Database context
        private readonly vending_machine _cont;
        // In PurchaseService class
        public IQueryable<product> Products => _cont.Products;
        public PurchaseService(DbContext context,vending_machine v)
        {
            _context = context;
            _cont = v;
        }

        public PurchaseResult BuyProduct(User user, int productId, int quantity)
        {
           
            var product = _cont.Products.SingleOrDefault(p => p.Id == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.AmountAvailable < quantity)
            {
                throw new Exception("Insufficient product quantity");
            }

            if (user.Deposit < product.Cost * quantity)
            {
                throw new Exception("Insufficient funds");
            }

            // Start transaction
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    user.Deposit -= product.Cost * quantity;
                    product.AmountAvailable -= quantity;
                    _context.SaveChanges();

                    transaction.Commit();

                    return new PurchaseResult
                    {
                        TotalSpent = product.Cost * quantity,
                        ItemsPurchased = new List<PurchaseItem> { new PurchaseItem { Product = product, Quantity = quantity } },
                        ChangeCoins = CalculateChange(user.Deposit)
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public IQueryable<product> GetProducts()
        {
            return _cont.Products;
        }
        public product Find(int id)
        {
            return _cont.Products.SingleOrDefault(p => p.Id == id);
        }
        public void DepositCoins(User user, int[] coinValues)
        {
            if (!coinValues.All(v => new[] { 5, 10, 20, 50, 100 }.Contains(v)))
            {
                throw new Exception("Invalid coin values");
            }

            user.Deposit += coinValues.Sum();
            _context.SaveChanges();
        }

        public void ResetDeposit(User user)
        {
            user.Deposit = 0;
            _context.SaveChanges();
        }

        private Dictionary<int, int> CalculateChange(decimal amount)
        {
            var coins = new Dictionary<int, int>();
            foreach (var value in new[] { 100, 50, 20, 10, 5 })
            {
                coins[value] = (int)(Math.Floor(amount / value));
                amount -= coins[value] * value;
            }
            return coins;
        }
        public Dictionary<int, int> GetChangeAmount(string userName)
        {
            // Retrieve user's deposit amount and calculate change
            var user = _cont.Users.SingleOrDefault(u => u.Username == userName);
            return CalculateChange(user.Deposit);
        }

        public bool CheckProductAvailability(int productId)
        {
            var product = _cont.Products.SingleOrDefault(p => p.Id == productId);
            return product != null && product.AmountAvailable > 0;
        }



       










    }










}
