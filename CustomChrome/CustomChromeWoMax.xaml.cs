using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CustomChrome.Annotations;
using CustomChrome.Interface;
using Microsoft.Windows.Shell;

namespace CustomChrome
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class CustomChromeControlWoMax : ICustomChromeControl
	{
		public CustomChromeControlWoMax()
		{
			InitializeComponent();
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			SystemCommands.CloseWindow(Window.GetWindow(this));
		}

		private void btnMin_Click(object sender, RoutedEventArgs e)
		{
			SystemCommands.MinimizeWindow(Window.GetWindow(this));
		}

        public void SetCaptionButtonMargin(Thickness thickness)
        {
            CustomChromeWoMax.Margin = thickness;
        }

        public void ToggleMaxmizeIconVisibility(bool visibility)
        {
        }
	}
}
