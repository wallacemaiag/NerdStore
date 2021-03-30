using System.Text.RegularExpressions;

namespace NS.Core.DomainObjects
{
    public class Email
    {
        public const int AddressEmailMaxLength = 254;
        public const int AddressEmailMinLength = 5;
        public string AddressEmail { get; set; }
        protected Email() { }
        public Email(string addressEmail)
        {
            if (!Validate(addressEmail)) throw new DomainException("E-mail Inválido!");
            AddressEmail = addressEmail;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
