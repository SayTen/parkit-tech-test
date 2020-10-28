using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Parkit.Core.DAL.Contexts.Models
{
    internal class User
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

    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // This is where I would customise the database schema and set up relationships etc.
        }
    }
}
