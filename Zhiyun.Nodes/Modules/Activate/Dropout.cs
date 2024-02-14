using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Nodes.Modules.Activate
{
    [Name("丢弃层")]
    public class Dropout: Activate
    {
        private float dropRate = 0.51f;

        [STNodeProperty("丢弃率", "")]
        [Property]
        public float DropRate
        {
            get => dropRate;
            set { dropRate = value; Flush(); }
        }


        protected override void OnFlushComponentPart()
        {
            UpdateText("Feature", $"丢弃率:{dropRate*100:f1}%");
        }

        protected override void OnInitializeProperty()
        {
            AutoSize = false;
            AddTextBlockControl("Feature", "");
        }
    }
}
