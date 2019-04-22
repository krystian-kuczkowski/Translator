using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Translator.Desktop.Commands;
using Translator.Desktop.Helpers;

namespace Translator.Desktop.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private WindowState _windowState;
        private readonly int _outerMarginSize = 10;
        private readonly int _windowRadius = 0;
        private WindowDockPosition dockPosition = WindowDockPosition.Undocked;

        public double WindowMinimumWidth {
            get => 450;
        }

        public double WindowMinimumHeight {
            get => 300;
        }

        public int ResizeBorder {
            get => 6;
        }

        public int TitleBarHeight {
            get => 24;
        }

        public bool Borderless {
            get => _windowState == WindowState.Maximized || dockPosition != WindowDockPosition.Undocked;
        }

        public GridLength TitleBarHeightGridLength {
            get => new GridLength(TitleBarHeight + ResizeBorder);
        }

        public Thickness ResizeBorderThickness {
            get => new Thickness(ResizeBorder + OuterMarginSize);
        }

        public Thickness InnerContentPadding {
            get => new Thickness(ResizeBorder);
        }

        public Thickness OuterMarginSizeThickness {
            get => new Thickness(OuterMarginSize);
        }

        public CornerRadius WindowCornerRadius {
            get => new CornerRadius(WindowRadius);
        }

        public int OuterMarginSize {
            get => Borderless ? 0 : _outerMarginSize;
        }

        public int WindowRadius {
            get => Borderless ? 0 : _windowRadius;
        }

        public ICommand StateChangedCommand { get; private set; }
        public ICommand WindowLoadedCommand { get; private set; }
        public ICommand WindowMinimizeCommand { get; private set; }
        public ICommand WindowMaximizeCommand { get; private set; }
        public ICommand WindowCloseCommand { get; private set; }

        public ShellViewModel()
        {
            StateChangedCommand = new RelayCommand(param => {
                _windowState = (WindowState)param;
                OnWindowResized();
            });

            WindowLoadedCommand = new RelayCommand(param => {
                _windowState = ((Window)param).WindowState;
                var resizer = new WindowResizer((Window)param);
                resizer.WindowDockChanged += (dock) => {
                    dockPosition = dock;
                    OnWindowResized();
                };
                OnWindowResized();
            });

            WindowMinimizeCommand = new RelayCommand(param => {
                ((Window)param).WindowState = WindowState.Minimized;
                _windowState = ((Window)param).WindowState;
                OnWindowResized();
            });

            WindowMaximizeCommand = new RelayCommand(param => {
                ((Window)param).WindowState ^= WindowState.Maximized;
                _windowState = ((Window)param).WindowState;
                OnWindowResized();
            });

            WindowCloseCommand = new RelayCommand(param => {
                ((Window)param).Close();
            });
        }

        private void OnWindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
    }
}
