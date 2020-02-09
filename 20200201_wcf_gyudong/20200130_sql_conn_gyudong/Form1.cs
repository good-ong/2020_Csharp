using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.ServiceModel;

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

        MySqlConnection conn;
        // = new MySqlConnection("server=192.168.136.129;username=root;password=111111;database=ACK;");
        MySqlCommand comm;

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
                comm = new MySqlCommand(query, conn);

                comm.Parameters.Add("@grade", MySqlDbType.Int32);
                comm.Parameters.Add("@cclass", MySqlDbType.Int32);
                comm.Parameters.Add("@no", MySqlDbType.Int64);
                comm.Parameters.Add("@name", MySqlDbType.VarChar, 100);
                comm.Parameters.Add("@score", MySqlDbType.VarChar, 20);

                comm.Parameters[0].Value = dataGridView1.CurrentRow.Cells[0].Value;
                comm.Parameters[1].Value = dataGridView1.CurrentRow.Cells[1].Value;
                comm.Parameters[2].Value = dataGridView1.CurrentRow.Cells[2].Value;
                comm.Parameters[3].Value = dataGridView1.CurrentRow.Cells[3].Value;
                comm.Parameters[4].Value = dataGridView1.CurrentRow.Cells[4].Value;

                comm.ExecuteNonQuery();
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
        public void wcfConnect()
        {
            try
            {
                ChannelFactory<IMyContract> factory = new ChannelFactory<IMyContract>();

                // Address
                string address = "net.tcp://localhost:8080/myAddress";
                factory.Endpoint.Address = new EndpointAddress(address);

                // Binding : TCP 사용
                factory.Endpoint.Binding = new NetTcpBinding();

                // Contract 설정
                factory.Endpoint.Contract.ContractType = typeof(IMyContract);

                // Channel Factory 만들기
                IMyContract channel = factory.CreateChannel();

                // Server 쪽 함수 호출
                // 서버 : 가상환경 ubuntu 18.04
                string connstring = channel.conndb("192.168.136.129", "root", "111111", "ACK");
                
                // Close Channel
                ((ICommunicationObject)channel).Close();
                conn = new MySqlConnection(connstring);

            }
            catch (Exception)
            {
                MessageBox.Show("연결 실패");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                wcfConnect();
                selectView();
            }
            catch (Exception)
            {
                MessageBox.Show("연결 상태를 확인해주세요.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                wcfConnect();
                // Insert 버튼 클릭 이벤트
                string insertQuery = "INSERT INTO student VALUES (@grade, @cclass, @no, @name, @score)";
                ExecuteQuery(insertQuery);
                // insert 후 새로고침한 table 출력
                selectView();
            }
            catch (Exception)
            {
                MessageBox.Show("연결 상태를 확인해주세요.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                wcfConnect();
                // Update 버튼 클릭 이벤트
                string updateQuery = "UPDATE student SET grade = @grade, cclass = @cclass, no = @no, name = @name, score = @score WHERE grade = @grade";
                ExecuteQuery(updateQuery);
                // update 후 새로고침한 table 출력
                selectView();
            }
            catch (Exception)
            {
                MessageBox.Show("연결 상태를 확인해주세요.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                wcfConnect();
                // Delete 버튼 클릭 이벤트
                string deleteQuery = "DELETE FROM student WHERE grade = @grade";
                ExecuteQuery(deleteQuery);
                // delete 후 새로고침한 table 출력
                selectView();
            }
            catch (Exception)
            {
                MessageBox.Show("연결 상태를 확인해주세요.");
            }
        }

        [ServiceContract]
        public interface IMyContract
        {
            [OperationContract]
            string conndb(string s, string i, string p, string d);
        }
    }
}
