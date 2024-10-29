using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwMonitorTask
{
    public class HardwareMonitor
    {
        Computer computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
        };
        public HardwareMonitor() 
        {
            computer.Open();
        }

        public void Update()
        {

            computer.Open(); // move this

            foreach (IHardware hardware in computer.Hardware)
            {
                Console.WriteLine("Hardware: {0}", hardware.Name);

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    Console.WriteLine("\tSubhardware: {0}", subhardware.Name);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                }
            }

            computer.Close(); // move this
        }
    }
}
