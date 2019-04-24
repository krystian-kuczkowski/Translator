using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Translator.Desktop.Helpers
{
    public class DockHandler
    {
        private readonly Window _window;

        public DockHandler(Window window)
        {
            this._window = window;
        }

        public bool IsWindowDocked()
        {
            return _window.Top == 0 && (_window.Top + _window.Height) >= _window.Height;
        }
    }
}
