using _20200209_unit_test_gyudong;
using System;
using Xunit;

namespace UnitTest
{
    public class studentTest
    {
        [Fact]
        public void CreateStudent()
        {
            int grade = 1;
            int cclass = 1;
            int no = 10101;
            string name = "Gildong";
            string score = "A";
            student s = new student(grade, cclass, no, name, score);

            Assert.Equal(grade, s.grade);
            Assert.Equal(cclass, s.cclass);
            Assert.Equal(no, s.no);
            Assert.Equal(name, s.name);
            Assert.Equal(score, s.score);
        }
    }
}
