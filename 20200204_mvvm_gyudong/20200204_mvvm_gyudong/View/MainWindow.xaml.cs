using System.Windows;
using _20200204_mvvm_gyudong.ViewModel;

namespace _20200204_mvvm_gyudong
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        StudentViewModel StVM = new StudentViewModel();
        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            StVM.SelectView(datagridStudent);
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            // Insert 버튼 클릭 이벤트
            string insertQuery = "INSERT INTO student VALUES (@grade, @cclass, @no, @name, @score)";
            StVM.ExecuteQuery(datagridStudent, insertQuery);
            // insert 후 새로고침한 table 출력
            StVM.SelectView(datagridStudent);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Update 버튼 클릭 이벤트
            string updateQuery = "UPDATE student SET grade = @grade, cclass = @cclass, no = @no, name = @name, score = @score WHERE grade = @grade";
            StVM.ExecuteQuery(datagridStudent, updateQuery);
            // update 후 새로고침한 table 출력
            StVM.SelectView(datagridStudent);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete 버튼 클릭 이벤트
            string deleteQuery = "DELETE FROM student WHERE grade = @grade";
            StVM.ExecuteQuery(datagridStudent, deleteQuery);
            // delete 후 새로고침한 table 출력
            StVM.SelectView(datagridStudent);
        }
    }
}
