using KOR.SystemInfo.OEM;
using KOR.SystemInfo.System;
using System.Windows;

namespace Example
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			SystemInfo.GetOSInfo();
			OEM.GetOEMInfo();
		}
	}
}
