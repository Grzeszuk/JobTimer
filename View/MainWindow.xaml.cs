using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using JobTimer.View;
using MahApps.Metro.Controls;
using TaskModelLocal = JobTimer.Model.TaskModelLocal;

namespace JobTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Variables
   
        public static ObservableCollection<TaskModelLocal> Tasks
        {
            get;
            set;
        }
        public static ObservableCollection<TaskModelLocal> TasksDone
        {
            get;
            set;
        }

        public Entity.Model Context;

        private bool _loaded = false;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            Load();
            Tasks.CollectionChanged += ContextChange;
            TasksDone.CollectionChanged += ContextChange;
            DataContext = this;
        }

        #endregion

        #region Entity Connection
        private void ContextChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!_loaded) return;

            if (e.NewItems?.Count > 0)
            {
                foreach (TaskModelLocal task in e.NewItems)
                {
                    Context.Tasks.Add(task);
                }
            }

            if (e.OldItems?.Count > 0)
            {
                foreach (TaskModelLocal task in e.OldItems)
                {
                    Context.Tasks.Remove(task);
                }
            }
            CheckLists();
            Context.SaveChanges();
        }
        private void Load()
        {
            Context = new Entity.Model();
            Tasks = new ObservableCollection<TaskModelLocal>();
            TasksDone = new ObservableCollection<TaskModelLocal>();

            foreach (var task in Context.Tasks)
            {
                if (task.EndTime == DateTime.MinValue)
                {
                    Tasks.Add(task);
                }
                else
                {
                    TasksDone.Add(task);
                }
            }
            CheckLists();
            _loaded = !_loaded;
        }
        #endregion

        #region AppBar Functions
        private void AddTask(object sender, RoutedEventArgs e)
        {
            new AddTaskView().ShowDialog();
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(((TextBox)sender).Text)) ((TextBox)sender).Text = "search task";
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Tasks.IsNullOrEmpty())
            ScrollViewer.ItemsSource = Searcher.Text == "search task" ? Tasks : Tasks.Where(x => x.TaskName.Contains(Searcher.Text));

            if (!TasksDone.IsNullOrEmpty())
            ScrollViewerDone.ItemsSource = Searcher.Text == "search task" ? TasksDone : TasksDone.Where(x => x.TaskName.Contains(Searcher.Text));

            CheckSearch();
        }
        #endregion

        #region Task Buttons
        private void DeleteTask_OnClick(object sender, RoutedEventArgs e)
        {
            Tasks.Remove(Tasks.SingleOrDefault(x => x.TaskName == ((Label)((Grid)((StackPanel)((Button)sender).Parent
                                                        .GetParentObject()).Children[0]).Children[0])
                                                    .Content.ToString()));
            CheckLists();
        }
        private void DeleteDoneTask_OnClick(object sender, RoutedEventArgs e)
        {
            TasksDone.Remove(TasksDone.SingleOrDefault(x => x.TaskName ==
                                                            ((Label)((Grid)((StackPanel)((Button)sender).Parent
                                                                .GetParentObject()).Children[0]).Children[0])
                                                            .Content.ToString()));
            CheckLists();
        }
        private void EndTask_OnClick(object sender, RoutedEventArgs e)
        {
            var actualTask = Tasks.SingleOrDefault(x => x.TaskName == ((Label)((Grid)((StackPanel)((Button)sender)
                                                                .Parent.GetParentObject()).Children[0])
                                                            .Children[0]).Content.ToString());


            actualTask?.EndTask();
            Tasks.Remove(actualTask);
            TasksDone.Add(actualTask);
            CheckLists();
        }
        private void DetailTask_OnClick(object sender, RoutedEventArgs e)
        {
            if (Tasks.ToList().Exists((x => x.TaskName == ((Label)((Grid)((StackPanel)((Button)sender).Parent
                                                .GetParentObject()).Children[0]).Children[0])
                                            .Content.ToString())))
            {
                var temp = Tasks.SingleOrDefault(x => x.TaskName == ((Label)((Grid)((StackPanel)((Button)sender)
                                                          .Parent
                                                          .GetParentObject()).Children[0]).Children[0])
                                                      .Content.ToString());

                new DetailsView(ref temp).ShowDialog();
                Tasks.Remove(temp);
                Tasks.Add(temp);

            }
            else
            {
                var temp = TasksDone.SingleOrDefault(x => x.TaskName == ((Label)((Grid)((StackPanel)((Button)sender)
                                                              .Parent
                                                              .GetParentObject()).Children[0]).Children[0])
                                                          .Content.ToString());

                new DetailsView(ref temp).ShowDialog();
                TasksDone.Remove(temp);
                TasksDone.Add(temp);

            }

        }
        #endregion

        #region Events
        public void CheckLists()
        {
            LabelUndone.Visibility = Tasks.Count == 0 ? Visibility.Collapsed : Visibility.Visible;

            LabelDone.Visibility = TasksDone.Count == 0 ? Visibility.Collapsed : Visibility.Visible;

            if (LabelDone.Visibility == Visibility.Collapsed && LabelUndone.Visibility == Visibility.Collapsed)
            {
                LabelAllDone.Visibility = Visibility.Visible;
            }
            else
            {
                LabelAllDone.Visibility = Visibility.Collapsed;

            }
        }

        public void CheckSearch()
        {
            if(LabelZero !=null && Tasks.Count>0)
            LabelZero.Visibility = ScrollViewer.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

            if (LabelZeroDone != null && TasksDone.Count > 0)
            LabelZeroDone.Visibility = ScrollViewerDone.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}