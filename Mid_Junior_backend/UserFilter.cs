namespace Mid_Junior_backend
{
    public class UserFilter
    {
       
            public string Username { get; set; }
            public string Role { get; set; }
            public int MinDeposit { get; set; }
            public int MaxDeposit { get; set; }
            public string SortBy { get; set; }
            public bool SortAscending { get; set; }
       
    }
}
