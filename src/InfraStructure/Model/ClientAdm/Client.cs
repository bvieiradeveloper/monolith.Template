using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Model.ClientAdm
{
    public class ClientModel
    {
        [Key]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        public string Document { get; set; }
        [NotNull]
        public string Street { get; set; }
        [NotNull]
        public string Number { get; set; }
        [NotNull]
        public string Complement { get; set; }
        [NotNull]
        public string City { get; set; }
        [NotNull]
        public string State { get; set; }
        [NotNull]
        public string ZipCode { get; set; }
        [NotNull]
        public DateTime CreatedAt { get; set; }
        [NotNull]
        public DateTime UpdatedAt { get; set; }
    }
}
