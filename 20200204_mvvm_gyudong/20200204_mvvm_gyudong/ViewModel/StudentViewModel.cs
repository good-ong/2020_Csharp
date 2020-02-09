using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using _20200204_mvvm_gyudong.Model;

namespace _20200204_mvvm_gyudong.ViewModel
{
    public class StudentViewModel
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=111111;Database=ACK");
        MySqlCommand cmd;
        MySqlDataAdapter adp;
        DataSet ds;
        Student st = new Student();

        public void OpenConn()
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
        }
        public void CloseConn()
        {
            if (conn.State == ConnectionState.Open) { conn.Close(); }
        }
        public void SelectView(DataGrid dg)
        {
            try
            {
                OpenConn();
                cmd = new MySqlCommand("Select * from student", conn);
                adp = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                adp.Fill(ds, "DataGridStudent");
                dg.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                CloseConn();
            }
        }
        public void ExecuteQuery(DataGrid dg, string query)
        {
            // 입력할 쿼리에 dataGridView의 현재 행을 변수로 추가 후 쿼리 실행
            try
            {
                OpenConn();
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@grade", MySqlDbType.Int32).Value = st.Grade;
                cmd.Parameters.Add("@cclass", MySqlDbType.Int32).Value = st.Cclass;
                cmd.Parameters.Add("@no", MySqlDbType.Int32).Value = st.No;
                cmd.Parameters.Add("@name", MySqlDbType.String).Value = st.Name;
                cmd.Parameters.Add("@score", MySqlDbType.String).Value = st.Score;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConn();
            }
        }
    }
}
