namespace SimpleVoter.Core.ViewModels.AccountViewModels
{
    public class GoogleAuthenticatorViewModel
    {
        public string SecretKey { get; set; }
        public string BarcodeUrl { get; set; }
        public string Code { get; set; }
    }
}