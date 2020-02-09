using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace _20200201_wcf_gyudong
{
    public partial class Form1 : Form
    {
        ServiceHost host;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Address 
                string address = "net.tcp://localhost:8080/myAddress";

                // Binding : TCP 사용
                NetTcpBinding binding = new NetTcpBinding();

                // Service Host 만들기
                host = new ServiceHost(typeof(MyService));

                // End Point 추가
                host.AddServiceEndpoint(typeof(IMyContract), binding, address);

                // Service Host 시작
                host.Open();
                // labeling
                label1.Text = "Service Start Successfully";
            }
            catch (Exception)
            {
                label1.Text = "Starting Error!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Service Host 종료
                host.Close();
                // labeling
                label1.Text = "Service Stop Successfully";
            }
            catch (Exception)
            {
                label1.Text = "Stopping Error!";
            }
        }
    }
    // Client 쪽에서 호출될 함수 Interface
    [ServiceContract]
    public interface IMyContract
    {
        [OperationContract]
        string conndb(string s, string i, string p, string d);
    }

    // 실제로 Client에서 호출될 함수
    public class MyService : IMyContract
    {
        public string conndb(string s, string i, string p, string d)
        {
            return "server=" + s + ";username=" + i + ";password=" + p + ";database=" + d;
        }
    }
}
