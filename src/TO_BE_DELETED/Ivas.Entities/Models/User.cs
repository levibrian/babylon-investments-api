using Ivas.Entities.Base;
using System;

namespace Ivas.Entities.Models
{
    public class User : Entity
    {
        public Guid UniqueId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid ApiKey { get; set; }
    }
}
