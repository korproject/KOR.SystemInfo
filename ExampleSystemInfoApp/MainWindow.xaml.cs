using KOR.SystemInfo.Helpers;
using KOR.SystemInfo.OEM;
using KOR.SystemInfo.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExampleSystemInfoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var baseboardInfo = OEM.GetBaseBoardInfo();

                if (baseboardInfo != null)
                {
                    baseboard.ItemsSource = new List<ListItems>
                    {
                        new ListItems {
                            Item = "BaseBoardId",
                            Value = baseboardInfo.BaseBoardId
                        },
                        new ListItems{
                            Item = "Manufacturer",
                            Value = baseboardInfo.Manufacturer
                        },
                        new ListItems
                        {
                            Item = "Model",
                            Value = baseboardInfo.Model
                        },
                        new ListItems
                        {
                            Item = "Name",
                            Value = baseboardInfo.Name
                        }
                    };
                }

                var cpuInfo = OEM.GetCPUInfo();

                if (cpuInfo != null)
                {
                    cpu.ItemsSource = new List<ListItems>
                    {
                        new ListItems {
                            Item = "CPUId",
                            Value = cpuInfo.CPUId
                        },
                        new ListItems{
                            Item = "Manufacturer",
                            Value = cpuInfo.Manufacturer
                        },
                        new ListItems
                        {
                            Item = "Family",
                            Value = cpuInfo.Family.ToString()
                        },
                        new ListItems
                        {
                            Item = "Name",
                            Value = cpuInfo.Name
                        }
                    };
                }

                var gpuInfo = OEM.GetGPUInfo();

                if (gpuInfo != null)
                {
                    gpu.ItemsSource = new List<ListItems>
                    {
                        new ListItems {
                            Item = "AdapterDACType",
                            Value = gpuInfo.AdapterDACType
                        },
                        new ListItems{
                            Item = "VideoProcessor",
                            Value = gpuInfo.VideoProcessor
                        },
                        new ListItems
                        {
                            Item = "Ram",
                            Value = gpuInfo.Ram.ToString()
                        },
                        new ListItems
                        {
                            Item = "Name",
                            Value = gpuInfo.Name
                        }
                    };
                }

                var hardDriveInfo = OEM.GetPrimaryHardDiskInfo();

                if (hardDriveInfo != null)
                {
                    hardDrive.ItemsSource = new List<ListItems>
                    {
                        new ListItems {
                            Item = "HardDiskId",
                            Value = hardDriveInfo.HardDiskId
                        },
                        new ListItems{
                            Item = "Manufacturer",
                            Value = hardDriveInfo.Manufacturer
                        },
                        new ListItems
                        {
                            Item = "Size",
                            Value = hardDriveInfo.Size.ToString()
                        },
                        new ListItems
                        {
                            Item = "Name",
                            Value = hardDriveInfo.Name
                        }
                    };
                }

                var memoryInfo = OEM.GetMemoryInfo();

                if (memoryInfo != null)
                {
                    memory.ItemsSource = new List<ListItems>
                    {
                        new ListItems {
                            Item = "Model",
                            Value = memoryInfo.Model
                        },
                        new ListItems{
                            Item = "Manufacturer",
                            Value = memoryInfo.Manufacturer
                        },
                        new ListItems
                        {
                            Item = "Size",
                            Value = memoryInfo.Size.ToString()
                        },
                        new ListItems
                        {
                            Item = "Name",
                            Value = memoryInfo.Name
                        }
                    };
                }

                var monitorInfo = OEM.GetMonitorInfo();

                if (monitorInfo != null)
                {
                    monitor.ItemsSource = new List<ListItems>
                    {
                        new ListItems {
                            Item = "MonitorType",
                            Value = monitorInfo.MonitorType
                        },
                        new ListItems{
                            Item = "ScreenInchSize",
                            Value = monitorInfo.ScreenInchSize.ToString()
                        },
                        new ListItems
                        {
                            Item = "Size",
                            Value = monitorInfo.ScreenHeight + "x" + monitorInfo.ScreenWidth
                        },
                        new ListItems
                        {
                            Item = "Name",
                            Value = monitorInfo.Name
                        }
                    };
                }


                var osInfo = SystemInfo.GetOSInfo();

                system.ItemsSource = new List<ListItems>
                {
                    new ListItems {
                        Item = "Operating Info",
                        Value = osInfo.OSName + osInfo.MajorMinorVersion.Major.ToString()
                    },
                    new ListItems{
                        Item = "Build",
                        Value = osInfo.Build.ToString()
                    },
                    new ListItems{
                        Item = "ServicePack",
                        Value = osInfo.ServicePack
                    },
                    new ListItems{
                        Item = "Bit",
                        Value = osInfo.OSBit.ToString()
                    }
                };

                var driveList = OEM.GetDriveInfo();

                drives.ItemsSource = new List<ListItems>
                {
                    new ListItems {
                        Item = "Letter",
                        Value = driveList[0].Letter
                    },
                    new ListItems{
                        Item = "Size",
                        Value = driveList[0].TotalSize.ToSize(CommonHelpers.SizeUnits.GB) + " GB"
                    },
                    new ListItems{
                        Item = "Usage",
                        Value = driveList[0].Usage.ToSize(CommonHelpers.SizeUnits.GB) + " GB" + " - %" + driveList[0].UsagePercent.ToString().Substring(0, 3)
                    },
                    new ListItems{
                        Item = "AvailableSpace",
                        Value = driveList[0].AvailableSpace.ToSize(CommonHelpers.SizeUnits.GB) + " GB" + " - %" + driveList[0].AvailableSpacePercent.ToString().Substring(0, 3)
                    }
                };

                var biosInfo = OEM.GetBiosInfo();

                bios.ItemsSource = new List<ListItems>
                {
                    new ListItems {
                        Item = "BIOSVersion",
                        Value = biosInfo.Version
                    },
                    new ListItems{
                        Item = "BuildNumber",
                        Value = biosInfo.BuildNumber
                    },
                    new ListItems{
                        Item = "CurrentLanguage",
                        Value = biosInfo.CurrentLanguage
                    },
                    new ListItems{
                        Item = "Manufacturer",
                        Value = biosInfo.Manufacturer
                    }
                };
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
                throw;
            }
        }
    }

    class ListItems
    {
        public string Item { get; set; }

        public string Value { get; set; }
    }
}
