using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace _20200209_unit_test_gyudong
{
    public class crudfunc
    {
        public string connString { get; set; }
        MySqlConnection conn;


        public crudfunc()
        {
            connString = @"server=localhost;database=ACK;username=root;password=111111";
        }
        // MySQL DB Connect
        public bool Connect()
        {
            conn = new MySqlConnection(connString);
            conn.Open();

            return conn.State == ConnectionState.Open;
        }
        // MySQL DB Disconnect
        public bool Disconnect()
        {
            conn = new MySqlConnection(connString);
            conn.Close();

            return conn.State == ConnectionState.Closed;
        }
        // MySQL Command Get
        public MySqlCommand GetMySqlCommand(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
        // MySQL Execute Command
        public DataTable ExecuteMySqlCommand(MySqlCommand cmd)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable result = new DataTable();
            adp.Fill(result);
            return result;
        }
        // 1. insert
        public bool InsertStudent(student s)
        {
            string InsertQuery = "insert into student values(@grade, @cclass, @no, @name, @score)";
            try
            {
                MySqlCommand cmd = GetMySqlCommand(InsertQuery);
                cmd.Parameters.Add("grade", MySqlDbType.Int32).Value = s.grade;
                cmd.Parameters.Add("cclass", MySqlDbType.Int32).Value = s.cclass;
                cmd.Parameters.Add("no", MySqlDbType.Int32).Value = s.no;
                cmd.Parameters.Add("name", MySqlDbType.VarChar).Value = s.name;
                cmd.Parameters.Add("score", MySqlDbType.VarChar).Value = s.score;

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        // 2. update
        public void UpdateStudent(student s)
        {
            string UpdateQuery = "update student set grade = @grade, cclass = @cclass, no = @no, name = @name, score = @score where no = @no";

            MySqlCommand cmd = GetMySqlCommand(UpdateQuery);
            cmd.Parameters.Add("grade", MySqlDbType.Int32).Value = s.grade;
            cmd.Parameters.Add("cclass", MySqlDbType.Int32).Value = s.cclass;
            cmd.Parameters.Add("no", MySqlDbType.Int32).Value = s.no;
            cmd.Parameters.Add("name", MySqlDbType.VarChar).Value = s.name;
            cmd.Parameters.Add("score", MySqlDbType.VarChar).Value = s.score;

            int rows = cmd.ExecuteNonQuery();
            if (rows != 1)
                throw new ArgumentException("Could not update the student.");

        }
        // 3. delete
        public void DeleteStudent(student s)
        {
            string DeleteQuery = "delete from student where no = @no";

            MySqlCommand cmd = GetMySqlCommand(DeleteQuery);
            cmd.Parameters.Add("no", MySqlDbType.Int32).Value = s.no;

            int rows = cmd.ExecuteNonQuery();
            if (rows != 1)
                throw new ArgumentException("Could not delete.");
        }
        // 4. select
        public student SelectStudent(int no)
        {
            string SelectQuery = "select * from student where no = @no";

            try
            {
                MySqlCommand cmd = GetMySqlCommand(SelectQuery);
                cmd.Parameters.Add("no", MySqlDbType.Int32).Value = no;
                MySqlDataReader sqlData = cmd.ExecuteReader();

                if (!sqlData.HasRows)
                    return null;

                student s = new student();

                while (sqlData.Read())
                {
                    s.grade = sqlData.GetInt32(0);
                    s.cclass = sqlData.GetInt32(1);
                    s.no = sqlData.GetInt32(2);
                    s.name = sqlData.GetString(3);
                    s.score = sqlData.GetString(4);
                }

                sqlData.Close();

                return s;
            }
            catch (MySqlException)
            {
                return null;
            }
        }
        // 5. select all
        public List<student> SelectAllStudents()
        {
            string SelectAllQuery = "select * from student";

            try
            {
                MySqlCommand cmd = GetMySqlCommand(SelectAllQuery);
                MySqlDataReader sqlData = cmd.ExecuteReader();

                if (!sqlData.HasRows)
                    return null;


                List<student> students = new List<student>();

                while (sqlData.Read())
                {
                    student s = new student();

                    s.grade = sqlData.GetInt32(0);
                    s.cclass = sqlData.GetInt32(1);
                    s.no = sqlData.GetInt32(2);
                    s.name = sqlData.GetString(3);
                    s.score = sqlData.GetString(4);

                    students.Add(s);
                }

                sqlData.Close();

                return students;
            }
            catch (MySqlException)
            {
                return null;
            }
        }
    }
}
