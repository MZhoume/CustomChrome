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
    public sealed partial class CustomChromeControl : ICustomChromeControl, INotifyPropertyChanged
    {
		public CustomChromeControl()
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

		private void btnMax_Click(object sender, RoutedEventArgs e)
		{
			Window w = Window.GetWindow(this);
			if (w?.WindowState == WindowState.Maximized)
				SystemCommands.RestoreWindow(w);
			else
				SystemCommands.MaximizeWindow(w);
        }

        public void SetCaptionButtonMargin(Thickness thickness)
        {
            CustomChrome.Margin = thickness;
        }

        /// <summary>
        /// The <see cref="BtnMaxVisibility" /> property's name.
        /// </summary>
        public const string BtnMaxVisibilityPropertyName = "BtnMaxVisibility";

        private Visibility _btnMaxVisibility = Visibility.Visible;

        /// <summary>
        /// Sets and gets the BtnMaxVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility BtnMaxVisibility
        {
            get
            {
                return _btnMaxVisibility;
            }

            set
            {
                if (_btnMaxVisibility == value)
                {
                    return;
                }

                _btnMaxVisibility = value;
                OnPropertyChanged(nameof(BtnMaxVisibility));
            }
        }

        /// <summary>
        /// The <see cref="BtnResVisibility" /> property's name.
        /// </summary>
        public const string BtnResVisibilityPropertyName = "BtnResVisibility";

        private Visibility _btnResVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the BtnResVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility BtnResVisibility
        {
            get
            {
                return _btnResVisibility;
            }

            set
            {
                if (_btnResVisibility == value)
                {
                    return;
                }

                _btnResVisibility = value;
                OnPropertyChanged(nameof(BtnResVisibility));
            }
        }
        
        public void ToggleMaxmizeIconVisibility(bool visibility)
        {
            if (visibility)
            {
                BtnMaxVisibility = Visibility.Visible;
                BtnResVisibility = Visibility.Collapsed;
            }
            else
            {
                BtnMaxVisibility = Visibility.Collapsed;
                BtnResVisibility = Visibility.Visible;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
