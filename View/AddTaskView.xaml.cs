using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace JobTimer.View
{
    /// <summary>
    /// Interaction logic for AddTaskView.xaml
    /// </summary>
    public partial class AddTaskView 
    {
        public AddTaskView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TaskName.Text)) return;

            if(!MainWindow.Tasks.ToList().Exists(x=>x.TaskName==TaskName.Text))
            {
                if(!string.IsNullOrEmpty(GetString(TaskDescription)))
                {
                    MainWindow.Tasks.Add(new Model.TaskModelLocal() { TaskName = TaskName.Text , Description = GetString(TaskDescription)});
                }
                else
                {
                    MainWindow.Tasks.Add(new Model.TaskModelLocal() { TaskName = TaskName.Text });
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("There is task with that name","JobTimer - Information",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        string GetString(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
    }
}
