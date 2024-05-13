namespace ExtraUtils.Api
{
    public class ApiUrls
    {
        /// <summary>
        /// End points of all apis.
        /// </summary>
        
        public class GetApi
        {
            public const string CmsList =  "/mgp-rummy/api/cms-list";
            public const string CmsView =  "/mgp-rummy/api/view-cms";
            public const string ReferHistory = "/mgp-rummy/api/referral-history";
            public const string TransactionHistory = "/mgp-rummy/api/fetch-wallet-details-list";
            public const string GameHistory = "/mgp-rummy/api/fetch-game-result-list";
            public const string FetchUserData = "/mgp-rummy/api/fetch-wallet";
            public const string InAppNotifications = "/mgp-rummy/api/fetch-notification-list";
        }
        
        public struct PostApi
        {
            public const string Login =  "/mgp-rummy/api/login";
            public const string ValidateOtp = "/mgp-rummy/api/verify-otp";
            public const string CompleteProfile = "/mgp-rummy/api/complete-profile";
            public const string RequestKyc =  "/mgp-rummy/api/add-kyc-details";
            public const string ResendOtp = "/mgp-rummy/api/resend-otp";
            public const string AutoLogin =  "/mgp-rummy/api/auto-login";
            public const string EditProfile =  "/mgp-rummy/api/edit-profile";
            public const string AddBankDetails =  "/mgp-rummy/api/add-bank-details";
            public const string FetchEntries = "/mgp-rummy/api/fetch-entryFees";
            public const string PaymentGateway = "/mgp-rummy/api/addPhonePeOrder";
         //   public const string CheckPaymentStatus = "/mgp-rummy/api/checkPaymentStatus";
            public const string CheckPaymentStatus = "/mgp-rummy/api/checkPaymentStatus";
            public const string WithdrawAmount = "/mgp-rummy/api/withdrawAmount";
            public const string FetchEntriesList = "/mgp-rummy/api/fetch-entryFees-list";
            public const string CreatePrivateTable = "/mgp-rummy/api/create-room";
            public const string FetchPrivateRoomDetails = "/mgp-rummy/api/fetch-room-details";
            public const string VerifyEmail =  "/mgp-rummy/api/send-verification-email";
            public const string ReportPlayer =  "/mgp-rummy/api/report-player";
            public const string LogOut =  "/mgp-rummy/api/user-logout";
            public const string DeleteAccount =  "/mgp-rummy/api/delete-account";
            
        }
    }
}
