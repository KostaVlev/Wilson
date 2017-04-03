namespace Wilson.Web
{
    public static class Constants
    {
        public static class AccountManageMessagesEn
        {
            public const string UserCreated = "User has been created";
            public const string PasswordChanged = "Your password has been changed.";
            public const string PasswordSet = "Your password has been set.";
            public const string Error = "An error has occurred.";
            public const string PhoneWasAdded = "Your phone number was added.";
            public const string PhoneRemoved = "Your phone number was removed.";
            public const string UserDeactivated = "User was deactivated.";
            public const string UserActivated = "User was activated.";
            public const string UserEdit = "User has been edited.";
        }

        public static class Roles
        {
            public const string Administrator = "Admin";
            public const string Accouter = "Accounter";
            public const string User = "User";
        }

        public static class Areas
        {
            public const string Admin = "Admin";
            public const string Accounting = "Accounting";
            public const string Companies = "Companies";
        }

        public static class DateTimeFormats
        {
            public const string Short = "dd-MMM-yyyy";
        }

        public static class InquiriesMessages
        {
            public const string OnlyForEmployees = "You need to be employee to perform this operation.";
            public const string Error = "Error! Please try again.";
        }

        public static class ExceptionMessages
        {
            public const string FileToLarge = "Maximum upload size is :";
            public const string InvalidFile = "Invalid file.";
        }
    }
}
