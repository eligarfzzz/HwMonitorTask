using System.Windows.Forms;

namespace HwMonitorTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var cpuNotifyIcon = new NotifyIcon(notifyIconsContainer);
            cpuNotifyIcon.Text = "CPU";
            cpuNotifyIcon.Visible = true;
            cpuNotifyIcon.Icon = this.Icon;
            notifyIcons.Add(cpuNotifyIcon);


            var gpuNotifyIcon = new NotifyIcon(notifyIconsContainer);
            gpuNotifyIcon.Text = "GPU";
            gpuNotifyIcon.Visible = true;
            gpuNotifyIcon.Icon = this.Icon;
            notifyIcons.Add(gpuNotifyIcon);

            HardwareMonitor.Update();
        }

        private  System.ComponentModel.IContainer notifyIconsContainer = new System.ComponentModel.Container();
        private List<NotifyIcon> notifyIcons = new List<NotifyIcon>();

    }
}
