using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using CustomChrome.Interface;
using Microsoft.Windows.Shell;

namespace CustomChrome
{
    public class CustomChromeWindow : Window
    {
        private readonly WindowChrome _windowChrome;
        private ICustomChromeControl _customChromeControl;

        protected CustomChromeWindow()
            : this(new Thickness(0), 21, new CornerRadius(6), new Thickness(0))
        { }

        /// <summary>
        /// Ctor of the CustomChromeWindow
        /// </summary>
        /// <param name="resizeBorderThickness">The size of the control by which one can resize the window</param>
        /// <param name="captionHeight">The height of the window caption</param>
        /// <param name="cornerRadius">The corner radius of the window</param>
        /// <param name="glassFrameThickness">the thickness of the glass frame</param>
        /// <param name="hasMaxBtn">if this window has max button in the chrome</param>
        protected CustomChromeWindow(
            Thickness resizeBorderThickness,
            double captionHeight,
            CornerRadius cornerRadius,
            Thickness glassFrameThickness)
        {
            _windowChrome = new WindowChrome()
            {
                ResizeBorderThickness = resizeBorderThickness,
                CaptionHeight = captionHeight,
                CornerRadius = cornerRadius,
                GlassFrameThickness = glassFrameThickness,
            };

            WindowChrome.SetWindowChrome(this, _windowChrome);

            BtnColor = "#2D2D30";
            BtnCloseColor = "#3C2D30";
            HasMaxBtn = true;

            Loaded += CustomChromeWindow_Loaded;
        }

        private void CustomChromeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var source = PresentationSource.FromVisual(this) as HwndSource;
            source?.AddHook(WndProc);

            Grid grid = new Grid();

            FrameworkElement uc = Content as FrameworkElement;
            DependencyObject parent = uc?.Parent;

            parent?.SetValue(ContentPresenter.ContentProperty, null);
            if (uc != null)
            {
                uc.Margin = new Thickness(uc.Margin.Left, uc.Margin.Top + _windowChrome.CaptionHeight,
                    uc.Margin.Right, uc.Margin.Bottom);
                grid.Children.Add(uc);
            }

            _customChromeControl = HasMaxBtn
                ? new CustomChromeControl()
                {
                    Width = 108,
                    Height = 20,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                }
                : new CustomChromeControlWoMax()
                {
                    Width = 80,
                    Height = 20,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                } as ICustomChromeControl;

            grid.Children.Add((UserControl) _customChromeControl);

            Content = grid;
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            // TODO: get the msg info 

            handled = false;
            var i = (IntPtr) 2;
            if (wparam == i)
            {
                handled = true;
            }
            return IntPtr.Zero;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Maximized)
            {
                _customChromeControl.SetCaptionButtonMargin(new Thickness(0, 0, 0, 0));
                _customChromeControl.ToggleMaxmizeIconVisibility(false);
            }
            else
            {
                _customChromeControl.SetCaptionButtonMargin(new Thickness(0, 0, 0, 0));
                _customChromeControl.ToggleMaxmizeIconVisibility(true);
            }
        }
        
        public string BtnColor
        {
            get { return (string)GetValue(BtnColorProperty); }
            set { SetValue(BtnColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnColorProperty =
            DependencyProperty.Register("BtnColor", typeof(string), typeof(CustomChromeWindow));
        
        public string BtnCloseColor
        {
            get { return (string)GetValue(BtnCloseColorProperty); }
            set { SetValue(BtnCloseColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnCloseColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnCloseColorProperty =
            DependencyProperty.Register("BtnCloseColor", typeof(string), typeof(CustomChromeWindow));
        
        public bool HasMaxBtn
        {
            get { return (bool)GetValue(HasMaxBtnProperty); }
            set { SetValue(HasMaxBtnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasMaxBtn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasMaxBtnProperty =
            DependencyProperty.Register("HasMaxBtn", typeof(bool), typeof(CustomChromeWindow));
    }
}