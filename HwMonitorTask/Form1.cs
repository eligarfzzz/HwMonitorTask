using System.Windows.Forms;
using System.Windows.Threading;

namespace HwMonitorTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var hardwares = hardwareMonitor.Update();

            foreach (var item in hardwares)
            {
                NotifyIcon icon;
                if (notifyIcons.ContainsKey(item.Id))
                {
                    icon = notifyIcons[item.Id];
                }
                else
                {
                    icon = new NotifyIcon(notifyIconsContainer);
                    notifyIcons[item.Id] = icon;
                }
                icon.Text = $"{item.Name} {item.Rate}%, {item.Temperature}°„C";
                icon.Visible = true;

                var h = new HardwareWidget();
                h.Rate = item.Rate ?? 0;
                if (item.Temperature.HasValue)
                {
                    h.Temperature = item.Temperature.Value;
                }
                using Bitmap bitmap = new Bitmap(h.Width, h.Height);
                h.DrawToBitmap(bitmap, new Rectangle(0, 0, h.Width, h.Height));
                IntPtr Hicon = bitmap.GetHicon();
                Icon newIcon = Icon.FromHandle(Hicon);
                icon.Icon = newIcon;
            }
        }

        private System.ComponentModel.IContainer notifyIconsContainer = new System.ComponentModel.Container();
        private HardwareMonitor hardwareMonitor = new HardwareMonitor();
        private DispatcherTimer timer = new DispatcherTimer();
        private Dictionary<string, NotifyIcon> notifyIcons = new Dictionary<string, NotifyIcon>();

    }
}
