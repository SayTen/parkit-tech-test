using System;
using System.ComponentModel.DataAnnotations;

namespace Parkit.Core.Models
{
    public class User
    {
        public Guid? Id { get; set; }

        [EmailAddress, Required]
        public string? Email { get; set; }

        [Required]
        public string? GivenName { get; set; }

        [Required]
        public string? FamilyName { get; set; }

        public DateTime? Created { get; set; }
    }
}
