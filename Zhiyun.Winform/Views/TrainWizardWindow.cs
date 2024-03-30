using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform.Views
{
    public partial class TrainWizardWindow : Form
    {
        public TrainWizardWindow()
        {
            InitializeComponent();

            _currentPage = new TrainWizardBaseMessagePage();
            SetPage(_currentPage);
        }

        private readonly WizardPageCollection MemoryPages = [];
        private Dictionary<string, string> PreviousPageRelations = [];

        private WizardPage _currentPage;
        private WizardPage? _previousPage;

        public WizardPage CurrentPage
        {
            get => _currentPage;
            set => SetPage(value);
        }

        private void SetPage(WizardPage value)
        {
            _previousPage = _currentPage;
            _previousPage?.Hide();
            MemoryPages.AddPage(value.GetClassName(), value);

            _currentPage = value;
            _currentPage.Parent = _previousPage?.Parent ?? WizardPanel;
            _currentPage.Show();
        }

        private void TrainWizardWindow_Load(object sender, EventArgs e)
        {
           
        }




        private void BtNextStep_Click(object sender, EventArgs e)
        {
            var nextPage = CurrentPage.GetNextWizardPage();
            if (nextPage != null) CurrentPage = nextPage;
        }

        private void BtLastStep_Click(object sender, EventArgs e)
        {
            
        }
    }
}
