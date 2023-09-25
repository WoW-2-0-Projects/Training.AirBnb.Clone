using Backend_Project.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Entities
{
    public class City :Entity
    {
        public string Name {  get; set; }
        public City(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
