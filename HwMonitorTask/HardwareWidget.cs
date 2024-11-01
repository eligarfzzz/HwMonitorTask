using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HwMonitorTask
{
    public partial class HardwareWidget : UserControl
    {
        public HardwareWidget()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TempLabel_Click(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int h = Height * Rate / 100;
            int y = Height - h;
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), 0, 0, Width, Height);
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, 175, 175)), 1, y, Width - 1, h);

            if (Temperature.HasValue)
            {
                e.Graphics.DrawString(Temperature.ToString(), new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(2, 2, this.Width, this.Height));
            }
        }

        private int rate = 0;
        private int? temperature = null;
        public int Rate
        {
            get => rate;
            set
            {
                rate = value;
                Refresh();
            }
        }
        public int? Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                Refresh();
            }
        }
    }
}
