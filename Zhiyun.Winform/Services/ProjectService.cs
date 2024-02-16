using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Winform.Models;

namespace Zhiyun.Winform.Services
{
    public class ProjectService
    {
        public async Task<bool> SaveProjectAsync(string projectPath, Func<string, Task> saveActionAsync)
        {
            if (!Directory.Exists(projectPath))
                Directory.CreateDirectory(projectPath);
           
            try
            {
                await saveActionAsync(projectPath);
                return true;
            }
            catch (Exception ex)
            {
                Notification.Error("在保存时遇到错误", ex.Message);
                return false;
            }
                
            

        }

        public async Task<bool> SaveProjectAsync(Func<Task> saveActionAsync)
        {
            try
            {
                await saveActionAsync();
                return true;
            }
            catch (Exception ex)
            {
                Notification.Error("在保存时遇到错误", ex.Message);
                return false;
            }



        }
        public async Task<bool> LoadProjectAsync(string projectPath, Func<Project, Task> loadActionAsync)
        {
            if (!File.Exists(projectPath))
            {
                Notification.Error("载入项目失败", "路径不存在");
                return false;
            }

           
            try
            {
                var project = JsonConvert.DeserializeObject<Project>(await File.ReadAllTextAsync(projectPath)) ?? throw new Exception("项目文件损坏");
                await loadActionAsync(project);
                return true;
            }
            catch (Exception ex)
            {
                Notification.Error("在载入时遇到错误", ex.Message);
                return false;
            }


        }
    }
}
