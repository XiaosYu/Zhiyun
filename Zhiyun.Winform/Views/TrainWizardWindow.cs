using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhiyun.Winform.Views
{
    public partial class TrainWizardWindow : Form
    {
        public TrainWizardWindow()
        {
            InitializeComponent();

            Pages.AddRange([
                new TrainWizardBaseMessagePage(WizardPanel),
                new TrainWizardSelectTrainPlateformPage(WizardPanel),
                new TrainWizardModuleMessagePage(WizardPanel),
                new TrainWizardTrainOptionsPage(WizardPanel)
            ]);
        }

        private readonly WizardPageCollection Pages = [];

        private int currentPageIndex = 0;

        public int CurrentPageIndex
        {
            get => currentPageIndex;
            set
            {
                if ((value - currentPageIndex) > 0 && !Pages[CurrentPageIndex].CanMoveNextPage())
                {
                    LabelCurrentPage.Invoke(() =>
                    {
                        Task.Factory.StartNew(async () =>
                        {
                            var lastText = LabelCurrentPage.Text;
                            var lastColor = LabelCurrentPage.ForeColor;
                            LabelCurrentPage.Text = "当前界面无法跳转，请完成信息填写";
                            LabelCurrentPage.ForeColor = Color.Red;
                            await Task.Delay(2000);
                            LabelCurrentPage.Text = lastText;
                            LabelCurrentPage.ForeColor = lastColor;
                        });
                    });
                    return;
                }

                if (value < 0) value = 0;
                if (value > Pages.Count - 1) value = Pages.Count - 1;

                Pages[CurrentPageIndex].Hide();
                currentPageIndex = value;
                Pages[CurrentPageIndex].Show();

                OnPageChanged(currentPageIndex);
            }
        }

        private void OnPageChanged(int currentPage)
        {
            LabelCurrentPage.Text = $"{currentPage + 1}/{Pages.Count}";
        }

        private void TrainWizardWindow_Load(object sender, EventArgs e)
        {
            LabelCurrentPage.Text = $"1/{Pages.Count}";
            CurrentPageIndex = 0;
        }




        private void BtNextStep_Click(object sender, EventArgs e)
        {
            CurrentPageIndex++;
        }

        private void BtLastStep_Click(object sender, EventArgs e)
        {
            CurrentPageIndex--;
        }
    }
}
