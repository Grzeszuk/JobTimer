using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JobTimer.Annotations;

namespace JobTimer.Model
{
    public class TaskModelLocal : INotifyPropertyChanged
    {
        #region Variables
        public int ID { get; set; }
        public string TaskName { get; set; }

        [Column(TypeName = "DateTime2")]
        public virtual DateTime StartTime { get; set; }

        [Column(TypeName = "DateTime2")]
        private DateTime _endTime;

        [Column(TypeName = "DateTime2")]
        public virtual DateTime EndTime { get => _endTime; set { _endTime = value;OnPropertyChanged(); } }

        private TimeSpan _totalTime;
        public virtual TimeSpan TotalTime { get => _totalTime;  set { _totalTime = value;OnPropertyChanged(); }}

        public string Description { get; set; } = "";
        #endregion

        #region Methods

        public TaskModelLocal()
        {
            StartTime = DateTime.Now;
        }
        public void EndTask()
        {
            EndTime = DateTime.Now;
            TotalTime = EndTime.Subtract(StartTime);
        }
        #endregion

        #region ImplementInterface
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
