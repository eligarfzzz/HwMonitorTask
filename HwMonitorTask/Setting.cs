using Microsoft.Win32;
using System.ComponentModel;
using System.Timers;
using System.Windows.Forms;

namespace HwMonitorTask
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            this.Opacity = 0;
            this.ShowInTaskbar = false;
            SizeChanged += (s, e) =>
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    this.Opacity = 0;
                    this.ShowInTaskbar = false;
                }
            };

            checkBox_startUp.Checked = IsStartup();
            checkBox_startUp.CheckedChanged += (s, e) =>
            {
                try
                {
                    SetStartup(checkBox_startUp.Checked);
                }
                catch (Exception)
                {

                }
            };
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
                    icon.Click += (s, e) =>
                    {
                        this.ShowInTaskbar = true;
                        this.Opacity = 1;
                        this.WindowState = FormWindowState.Normal;
                    };
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
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private Dictionary<string, NotifyIcon> notifyIcons = new Dictionary<string, NotifyIcon>();

        private bool IsStartup()
        {
            RegistryKey? rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            return rk?.GetValue(Application.ProductName) != null;
        }

        private void SetStartup(bool startup)
        {

            RegistryKey? rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (startup)
            {
                rk?.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            else
            {
                rk?.DeleteValue(Application.ProductName ?? "", false);
            }

        }
    }
}
