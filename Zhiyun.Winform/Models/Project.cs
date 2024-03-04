#nullable disable

namespace Zhiyun.Winform.Models
{
    public class Project
    {
        public string ProjectName { set; get; }
        public DateTime LastModified { set; get; }
        public List<string> FileNames { set; get; }
    }
}
