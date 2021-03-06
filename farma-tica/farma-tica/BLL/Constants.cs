namespace farma_tica.BLL
{
    /// <summary>
    /// Constants which determine the message displayed by the GUI after trying to log in to the application.
    /// </summary>
    public static class Constants
    {
        //Authorize login constants.
        public const int INVALID_USER = 0;
        public const int INVALID_PASSWORD = 1;
        public const int SUCCESSFUL_LOGIN = 2;

        //Create user constants.
        public const int ALREADY_REGISTERED = 0;
        public const int USER_CREATED = 1;

        //Update user constants.
        public const int USER_NOT_UPDATED = 0;
        public const int USER_UPDATED = 1;
        //Delete user constants.
        public const int USER_NOT_DELETED = 0;
        public const int USER_DELETED = 1;

        //Create medicine constants.
        public const int ALREADY_EXISTS = 0;
        public const int MEDICINE_CREATED = 1;

        //Update medicine constants.
        public const int MEDICINE_NOT_UPDATED = 0;
        public const int MEDICINE_UPDATED = 1;

        //Delete user constants.
        public const int MEDICINE_NOT_DELETED = 0;
        public const int MEDICINE_DELETED = 1;

        //Standardized constants
        public const int FAIL = 0;
        public const int SUCCESS = 1;
        public const int ERROR = 0;
        //Standardized constants
        public const string LOW_PRIORITY = "Baja";
        public const string NORMAL_PRIORITY = "Normal";
        public const int CREATED_ORDER_STATUS = 0;
        public const int PREPARED_ORDER_STATUS = 1;
        public const int INVOICED_ORDER_STATUS = 2;
        public const int RETIRED_ORDER_STATUS = 3;
    }
}