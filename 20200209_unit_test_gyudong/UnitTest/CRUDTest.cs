using System;
using Xunit;
using _20200209_unit_test_gyudong;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

namespace UnitTest
{
    public class CRUDTest
    {

        public string connString { get; set; }
        MySqlConnection conn;
        crudfunc test = new crudfunc();
        
        public CRUDTest()
        {
            connString = @"server=localhost;database=ACK;username=root;password=111111";
        }


        [Fact]
        public void GetConnStringTest()
        {
            conn = new MySqlConnection(connString);
            MySqlConnection conn2 = new MySqlConnection(test.connString);
            string actualString = conn.ConnectionString;
            string expectedString = conn2.ConnectionString;
            Assert.Equal(expectedString, actualString);
        }

        [Fact]
        public void ConnectTEst()
        {
            bool connected = test.Connect();
            Assert.True(connected);
            bool disconnected = test.Disconnect();
            Assert.True(disconnected);
        }

        [Fact]
        public void GetMySqlCommandTest()
        {
            string query = "Test";
            MySqlCommand cmd = test.GetMySqlCommand(query);

            Assert.NotNull(cmd);
            Assert.Equal(query, cmd.CommandText);
        }
        [Fact]
        public void ExecuteMySqlCommandTest()
        {
            string query = "Test2";
            test.Connect();
            MySqlCommand cmd = test.GetMySqlCommand(query);
            DataTable ds = test.ExecuteMySqlCommand(cmd);
            test.Disconnect();
            Assert.True(ds.Rows.Count > 0);
        }
        [Fact]
        public void InsertStudentTest()
        {
            int grade = 6;
            int cclass = 3;
            int no = 60301;
            string name = "Test Nam";
            string score = "x";

            test.Connect();
            bool inserted = test.InsertStudent(new student(grade, cclass, no, name, score));
            test.Disconnect();
            Assert.True(inserted);
        }
        [Fact]
        public void UpdateStudentTest()
        {
            int grade = 5;
            int cclass = 4;
            int no = 50401;
            string name = "Test Kim";
            string score = "t";
            string updatedName = "Test Lee";
            test.Connect();

            test.InsertStudent(new student(grade, cclass, no, name, score));

            student s = test.SelectStudent(no);

            s.name = updatedName;
            test.UpdateStudent(s);

            student s2 = test.SelectStudent(no);

            test.Disconnect();

            Assert.Equal(s.grade, s2.grade);
            Assert.Equal(s.cclass, s2.cclass);
            Assert.Equal(s.no, s2.no);
            Assert.Equal(updatedName, s2.name);
            Assert.Equal(s.score, s2.score);
        }
        [Fact]
        public void SelectStudentTest()
        {
            int grade = 4;
            int cclass = 3;
            int no = 40307;
            string name = "Test Min";
            string score = "t";
            test.Connect();

            test.InsertStudent(new student(grade, cclass, no, name, score));

            student s = test.SelectStudent(no);

            test.Disconnect();

            Assert.NotNull(s);
            Assert.Equal(no, s.no);
            Assert.NotEmpty(s.name);
        }
        [Fact]
        public void DeleteStudentTest()
        {
            int grade = 4;
            int cclass = 3;
            int no = 40307;
            string name = "Test Min";
            string score = "t";
            test.Connect();

            test.InsertStudent(new student(grade, cclass, no, name, score));

            student s = test.SelectStudent(no);

            test.DeleteStudent(s);

            student s2 = test.SelectStudent(no);

            test.Disconnect();

            Assert.Null(s2);
        }
        [Fact]
        public void SelectAllStudentTest()
        {
            int grade = 2;
            int cclass = 2;
            int no = 20202;
            string name = "Test";
            string score = "A";
            int insertCount = 5;

            test.Connect();

            for (int i = 0; i < insertCount; i++)
            {
                test.InsertStudent(new student(grade + i, cclass + i, no + (i * 10101), name + i, score + i));

            }

            List<student> students = test.SelectAllStudents();

            test.Disconnect();

            Assert.NotNull(students);
            Assert.True(students.Count == insertCount);
        }
    }
}
