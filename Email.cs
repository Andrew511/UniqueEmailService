using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueEmailService
{
    public class Email
    {
        //list of domains that support account matching
        private static readonly IEnumerable<string> DomainSupportsMatching = new HashSet<string> { "gmail.com" };
        private string _rawEmail;
        private string _uniqueEmail;
        private const string ACC_MATCH_IGNORE_AFTER = "+";
        private const string ACC_MATCH_IGNORE_CHAR = ".";

        public Email(string email)
        {
            RawEmail = email;
        }

        public string RawEmail { 
            get
            {
                return _rawEmail;
            }
            set
            {
                _rawEmail = value;
                _uniqueEmail = GetUniqueEmail();
            }
        }

        private string Domain
        {
            get
            {
                //Per RFC 1035 3.1, domains must be compared case insensitive, so convert to lower case.
                //Per RFC 5321 2.4 Mailbox local parts (before @) are case sensitive (despite implementations that ignore this) and so it is dangerous to
                //  check for uniqueness ignoring case on the entire email address.
                return RawEmail.Substring(RawEmail.IndexOf('@'), RawEmail.Length).ToLower();
            }
        }

        public string UniqueEmail {
            get 
            {
                return _uniqueEmail;
            }
        }

        private string GetUniqueEmail() {
            string uniqueEmail;
            if (Email.DomainSupportsMatching.Contains(Domain))
            {
                string localPart = RawEmail.Substring(0, RawEmail.IndexOf('@'));
                localPart = localPart.Substring(0, localPart.IndexOf(ACC_MATCH_IGNORE_AFTER));
                localPart = localPart.Replace(ACC_MATCH_IGNORE_CHAR, String.Empty);

                uniqueEmail = localPart + Domain;
            }
            else
            {
                uniqueEmail = _rawEmail;
            }
            return uniqueEmail;
        }
    }
}
