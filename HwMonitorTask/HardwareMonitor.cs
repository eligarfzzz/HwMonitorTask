using LibreHardwareMonitor.Hardware;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwMonitorTask
{
    public class HardwareRateTemprature
    {
        public enum HardwareType
        {
            Cpu, Gpu
        }
        public HardwareType Type { get; set; }
        public int? Rate { get; set; } = null;
        public int? Temperature { get; set; } = null;
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";

    }
    public class HardwareMonitor : IDisposable
    {
        public HardwareMonitor()
        {
            computer.Open();
        }

        LibreHardwareMonitor.Hardware.Computer computer = new LibreHardwareMonitor.Hardware.Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
        };
        class Visitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }

            public void VisitParameter(IParameter parameter)
            {
            }

            public void VisitSensor(ISensor sensor)
            {
            }
        }
        public List<HardwareRateTemprature> Update()
        {
            computer.Accept(new Visitor());
            List<HardwareRateTemprature> result = new List<HardwareRateTemprature>();
            foreach (IHardware hardware in computer.Hardware)
            {
                HardwareRateTemprature item = new HardwareRateTemprature();
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    item.Type = HardwareRateTemprature.HardwareType.Cpu;
                    result.Add(item);


                    item.Rate = (int?)hardware.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Load)?.Value;
                    item.Temperature = (int?)hardware.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Temperature)?.Value;

                    //if(item.Rate == null)
                    //{
                    //    var loads = hardware.SubHardware
                    //        .Select(x => x.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Load)?.Value)
                    //        .Where(x => x != null)
                    //        .Select(x => (float)x!)
                    //        .ToArray();
                    //    item.Rate = loads.Any() ? (int)loads.Average() : null;

                    //    var temps  = hardware.SubHardware
                    //        .Select(x => x.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Temperature)?.Value)
                    //        .Where(x => x != null)
                    //        .Select(x => (float)x!)
                    //        .ToArray();

                    //    item.Temperature = temps.Any() ? (int)temps.Average() : null;
                    //}

                }
                else if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAmd)
                {
                    item.Type = HardwareRateTemprature.HardwareType.Gpu;
                    result.Add(item);
                }
                else
                {
                    continue;
                }
                item.Name = hardware.Name;
                item.Id = hardware.Identifier.ToString();
                item.Rate = (int?)hardware.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Load)?.Value;
                item.Temperature = (int?)hardware.Sensors.FirstOrDefault(x => x.SensorType == SensorType.Temperature)?.Value;
            }
            return result;
        }


        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged resources here.
                computer.Close();
                disposed = true;
            }
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        ~HardwareMonitor()
        {
            Dispose(disposing: false);
        }
    }
}
