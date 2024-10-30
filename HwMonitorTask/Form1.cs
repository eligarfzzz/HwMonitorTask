using System.Windows.Forms;
using System.Windows.Threading;

namespace HwMonitorTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
                icon.Icon = this.Icon;
            }
        }

        private System.ComponentModel.IContainer notifyIconsContainer = new System.ComponentModel.Container();
        private HardwareMonitor hardwareMonitor = new HardwareMonitor();
        private DispatcherTimer timer = new DispatcherTimer();
        private Dictionary<string, NotifyIcon> notifyIcons = new Dictionary<string, NotifyIcon>();

    }
}
