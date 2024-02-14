using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Modules.Activate
{
    [Name("线性激活层")]
    public class Factor: Activate
    {
        private bool learnable = false;
        private float initialValue = 0.5f;

        public override int ParametersNumber => Learnable ? InputDim.ValueCount : 0;

        [STNodeProperty("是否可学习", "")]
        [Property]
        public bool Learnable
        {
            get => learnable;
            set { learnable = value; Flush(); }
        }

        [STNodeProperty("初始值", "")]
        [Property]
        public float InitialValue
        {
            get => initialValue;
            set { initialValue = value; Flush(); }
        }


        protected override void OnFlushComponentPart()
        {
            UpdateText("Feature", $"因子:{InitialValue:f2}");
        }

        protected override void OnInitializeProperty()
        {
            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
