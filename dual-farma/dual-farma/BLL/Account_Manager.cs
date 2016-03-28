namespace dual_farma.BLL
{

    public class Account_Manager
    {
        private string userID;
        private string password;
        var factory = new DbConnectionFactory("local");
        var context = new DbContext(factory);
        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public Account_Manager(string userID, string password)
        {
            userID = this.userID;
            password = this.password;
        }

        public Account_Manager()
        { }

        private int AuthorizeLogin(string userID, string password){
            if
            
            }
       
       

    }
}