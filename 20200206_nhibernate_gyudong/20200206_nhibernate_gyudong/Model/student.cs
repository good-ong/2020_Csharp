using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20200206_nhibernate_gyudong.Model
{
    public class student
    {
        public virtual int grade { get; set; }
        public virtual int cclass { get; set; }
        public virtual int no { get; set; }
        public virtual string name { get; set; }
        public virtual string score { get; set; }
    }
}
