using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JobTimer.Model;

namespace JobTimer.View
{
    /// <summary>
    /// Interaction logic for DetailsView.xaml
    /// </summary>
    public partial class DetailsView
    {
        private TaskModelLocal Arg;
        public DetailsView(ref Model.TaskModelLocal arg)
        {
            InitializeComponent();
            Arg = arg;
            DataContext = arg;

            if (arg.EndTime == DateTime.MinValue)
            {
                EndTime.Visibility = Visibility.Collapsed;
                TotalTime.Visibility = Visibility.Collapsed;
            }
            else
            {
                Height += 45;
            }
        }

        private void InfoBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            Arg.Description = InfoBox.Text;
        }
    }
}
