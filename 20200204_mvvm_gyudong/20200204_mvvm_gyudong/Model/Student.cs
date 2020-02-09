using System.Windows.Controls;
namespace _20200204_mvvm_gyudong.Model
{
    public class Student
    {
        DataGrid DG = new DataGrid();
        
        public string Grade
        {
            get
            {
                return (DG.SelectedCells[0].Column.GetCellContent(DG.SelectedItem) as TextBlock).Text;
            }
        }
        public string Cclass
        {
            get
            {
                return (DG.SelectedCells[1].Column.GetCellContent(DG.SelectedItem) as TextBlock).Text;
            }
        }
        public string No
        {
            get
            {
                return (DG.SelectedCells[2].Column.GetCellContent(DG.SelectedItem) as TextBlock).Text;
            }
        }
        public string Name
        {
            get
            {
                return (DG.SelectedCells[3].Column.GetCellContent(DG.SelectedItem) as TextBlock).Text;
            }
        }
        public string Score
        {
            get
            {
                return (DG.SelectedCells[4].Column.GetCellContent(DG.SelectedItem) as TextBlock).Text;
            }
        }
    }
}
