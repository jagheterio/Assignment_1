using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Models
{
    internal class Contact
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";

        public string? PostalCode { get; set; }
        public string? StreetName { get; set; }
        public string City { get; set; } = null!;

        public string? Telephone { get; set; }
        public string Email { get; set; } = null!;
        public string PassWord { get; set; } = null!;

    }
}
