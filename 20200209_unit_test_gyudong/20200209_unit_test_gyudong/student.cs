using System;

namespace _20200209_unit_test_gyudong
{
    public class student
    {
        public int grade { get; set; }
        public int cclass { get; set; }
        public int no { get; set; }
        public string name { get; set; }
        public string score { get; set; }

        public student(int grade, int cclass, int no, string name, string score)
        {
            this.grade = grade;
            this.cclass = cclass;
            this.no = no;
            this.name = name;
            this.score = score;
        }

        public student()
        {
        }
    }
}