using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhiyun.Winform.Components
{
    public partial class WizardPage : UserControl
    {
        public WizardPage()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        public virtual bool CanMoveNextPage() => true;
        public virtual WizardPage? GetNextWizardPage() => null;
    }



    public class WizardPageCollection : List<WizardPage>
    {
        public TPage? GetPage<TPage>() where TPage : WizardPage => Find(s => s is TPage) as TPage;
    }
}
