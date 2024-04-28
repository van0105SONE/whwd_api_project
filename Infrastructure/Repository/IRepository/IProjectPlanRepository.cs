using Infrastructure.DataBaseContext;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.IRepository
{
    public  interface IProjectPlanRepository
    {
        void create(ProjectPlan projectParam);
        void update(ProjectPlan projectParam);
        void delete(Guid Id);
    }
}
