using Ivas.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivas.Entities.Models
{
    public class Security : Entity
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public int Employees { get; set; }
    }
}
