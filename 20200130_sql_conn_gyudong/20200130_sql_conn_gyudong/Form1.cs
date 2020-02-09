using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace _20200130_sql_conn_gyudong
{
    public partial class Form1 : Form
    {      
        public Form1()
        {
            InitializeComponent();
        }
        // MySQL 연결
        // data source : localhost
        // port : 3306
        // username : root
        // password : 111111
        // database name : ACK;
        MySqlConnection conn = new MySqlConnection("server=localhost;username=root;password=111111;database=ACK;");
        MySqlCommand command;

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
            // read 쿼리 실행 후 dataGridView에 출력
            string selectQuery = "SELECT * FROM student";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public void ExecuteQuery(string query)
        {
            // 입력할 쿼리에 dataGridView의 현재 행을 변수로 추가 후 쿼리 실행
            try {
                openConn();
                command = new MySqlCommand(query, conn);

                command.Parameters.Add("@grade", MySqlDbType.Int32);
                command.Parameters.Add("@cclass", MySqlDbType.Int32);
                command.Parameters.Add("@no", MySqlDbType.Int64);
                command.Parameters.Add("@name", MySqlDbType.VarChar, 100);
                command.Parameters.Add("@score", MySqlDbType.VarChar, 20);

                command.Parameters[0].Value = dataGridView1.CurrentRow.Cells[0].Value;
                command.Parameters[1].Value = dataGridView1.CurrentRow.Cells[1].Value;
                command.Parameters[2].Value = dataGridView1.CurrentRow.Cells[2].Value;
                command.Parameters[3].Value = dataGridView1.CurrentRow.Cells[3].Value;
                command.Parameters[4].Value = dataGridView1.CurrentRow.Cells[4].Value;

                command.ExecuteNonQuery();
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
        private void Form1_Load(object sender, EventArgs e)
        {
            // 윈폼 로드 이벤트
            // MessageBox.Show("database : ACK");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectView();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Insert 버튼 클릭 이벤트
            string insertQuery = "INSERT INTO student VALUES (@grade, @cclass, @no, @name, @score)";
            ExecuteQuery(insertQuery);
            // insert 후 새로고침한 table 출력
            selectView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update 버튼 클릭 이벤트
            string updateQuery = "UPDATE student SET grade = @grade, cclass = @cclass, no = @no, name = @name, score = @score WHERE grade = @grade";
            ExecuteQuery(updateQuery);
            // update 후 새로고침한 table 출력
            selectView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Delete 버튼 클릭 이벤트
            string deleteQuery = "DELETE FROM student WHERE grade = @grade";
            ExecuteQuery(deleteQuery);
            // delete 후 새로고침한 table 출력
            selectView();
        }
    }
}
