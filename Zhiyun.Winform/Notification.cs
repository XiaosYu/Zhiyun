using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Winform
{
    public static class Notification
    {
        public static DialogResult Inform(string title, string content)
            => MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static DialogResult Error(string title, string content)
            => MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static DialogResult Confirm(string title, string content)
            => MessageBox.Show(content, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}
