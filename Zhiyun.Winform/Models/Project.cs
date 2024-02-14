using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Winform.Models
{
    public class Project
    {
        public string ProjectName { set; get; }
        public DateTime LastModified { set; get; }
        public List<string> FileNames { set; get; }
    }
}
