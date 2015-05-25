using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CustomChrome.Interface
{
    internal interface ICustomChromeControl
    {
        void SetCaptionButtonMargin(Thickness thickness);

        void ToggleMaxmizeIconVisibility(bool visibility);
    }
}
