using JobTimer.Model;

namespace JobTimer.Entity
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {

        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<TaskModelLocal> Tasks { get; set; }
    }
}