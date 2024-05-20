﻿using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Student
{
    public class Student : BaseModel
    {
        public String Name { get; set; }
        public DateTime birthDate { get; set; }
        public String Level { get; set; }
        public int ShirtSize { get; set; }
        public int SkirtSize { get; set; }
        public int ShoesSize { get; set; }
        public int GloveSize { get; set; }
        public ProjectPlan Project { get; set; }
    }
}
