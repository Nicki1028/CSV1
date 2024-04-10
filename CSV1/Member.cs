using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV1
{
    internal class Member
    {
        public string Number { get; set; }
        public string Name { get; set; }
        //public string Birth { get; set; }
        public Member(string number, string name, string birth) 
        {
            Number = number;
            Name = name;    
            //Birth = birth;
        }
        public Member() { }
    }
}
