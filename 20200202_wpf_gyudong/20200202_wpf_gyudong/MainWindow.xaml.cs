using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace _20200202_wpf_gyudong
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;userid=root;password=111111;Database=ACK");
        MySqlCommand cmd;
        public void openConn()
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
        }
        public void closeConn()
        {
            if (conn.State == ConnectionState.Open) { conn.Close(); }
        }
        public void selectView()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("Select * from student", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "datagridStudent");
                datagridStudent.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void ExecuteQuery(string query)
        {
            // 입력할 쿼리에 dataGridView의 현재 행을 변수로 추가 후 쿼리 실행
            try
            {
                openConn();
                object item = datagridStudent.SelectedItem;
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@grade", MySqlDbType.Int32).Value = (datagridStudent.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                cmd.Parameters.Add("@cclass", MySqlDbType.Int32).Value = (datagridStudent.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                cmd.Parameters.Add("@no", MySqlDbType.Int64).Value = (datagridStudent.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = (datagridStudent.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                cmd.Parameters.Add("@score", MySqlDbType.VarChar, 20).Value = (datagridStudent.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                closeConn();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            selectView();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            // Insert 버튼 클릭 이벤트
            string insertQuery = "INSERT INTO student VALUES (@grade, @cclass, @no, @name, @score)";
            ExecuteQuery(insertQuery);
            // insert 후 새로고침한 table 출력
            selectView();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Update 버튼 클릭 이벤트
            string updateQuery = "UPDATE student SET grade = @grade, cclass = @cclass, no = @no, name = @name, score = @score WHERE grade = @grade";
            ExecuteQuery(updateQuery);
            // update 후 새로고침한 table 출력
            selectView();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete 버튼 클릭 이벤트
            string deleteQuery = "DELETE FROM student WHERE grade = @grade";
            ExecuteQuery(deleteQuery);
            // delete 후 새로고침한 table 출력
            selectView();
        }
    }
}
