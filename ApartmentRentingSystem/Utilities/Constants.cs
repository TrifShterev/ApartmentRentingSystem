namespace ApartmentRentingSystem.Utilities
{
    public static class Constants
    {
        public const int ApartmentTypeMaxLength = 50;
        public const int ApartmentTypeMinLength = 2;
        public const int CategoryNameMaxLength = 20;

        public static class User
        {
            public const int UserNameMaxLength = 50;
            public const int UserNameMinLength = 2;
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }

        public static class Broker
        {
            public const int BrokerNameMaxLength = 30;
            public const int BrokerNameMinLength = 1;
            public const int BrokerPhoneNumberMaxLength = 30;
            public const int BrokerPhoneNumberMinLength = 5;
        }

        public static class WebConstants
        {
            public const string AdminRoleName = "Administrator";
        }
       
    }
}