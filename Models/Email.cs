using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UniqueEmailService.Models
{
    public class Email
    {
        //list of domains that support account matching
        private static readonly IEnumerable<string> DomainSupportsMatching = new HashSet<string> { "gmail.com" };
        private string _rawEmail;
        private const string ACC_MATCH_IGNORE_AFTER = "+";
        private const string ACC_MATCH_IGNORE_CHAR = ".";
        private const string AT_SYM = "@";

        [EmailAddress]
        [Required]
        public string RawEmail
        {
            get
            {
                return _rawEmail;
            }
            set
            {
                _rawEmail = value;
                UniqueEmail = GetUniqueEmail();
            }
        }

        private string Domain
        {
            get
            {
                //Per RFC 1035 3.1, domains must be compared case insensitive, so convert to lower case.
                //Per RFC 5321 2.4 Mailbox local parts (before @) are case sensitive (despite implementations that ignore this) and so it is dangerous to
                //  check for uniqueness ignoring case on the entire email address.
                return RawEmail.Substring(RawEmail.IndexOf('@') + 1).ToLower();
            }
        }

        public string UniqueEmail { get; private set; }

        private string GetUniqueEmail()
        {
            string uniqueEmail;
            if (DomainSupportsMatching.Contains(Domain))
            {
                string localPart = RawEmail.Split(AT_SYM).First();
                localPart = localPart.Split(ACC_MATCH_IGNORE_AFTER).First();
                localPart = localPart.Replace(ACC_MATCH_IGNORE_CHAR, string.Empty);

                uniqueEmail = localPart + '@' + Domain;
            }
            else
            {
                uniqueEmail = _rawEmail;
            }
            return uniqueEmail;
        }
    }
}
