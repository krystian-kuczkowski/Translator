using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly int _outerMarginSize = 10;
        private readonly int _windowRadius = 0;
        private readonly int _contentPadding = 20;

        private WindowState _windowState;
        private DockHandler _dockHandler;

        public double WindowMinimumWidth
        {
            get => 450;
        }

        public double WindowMinimumHeight
        {
            get => 300;
        }

        public int ResizeBorder
        {
            get => 6;
        }

        public int TitleBarHeight
        {
            get => 24;
        }

        public Thickness MenuBarPadding
        {
            get => new Thickness(0, TitleBarHeightGridLength.Value, 0, 0);
        }

        public bool Borderless
        {
            get
            {
                if (_windowState != WindowState.Maximized && _dockHandler == null)
                    return false;

                return _windowState == WindowState.Maximized || _dockHandler.IsWindowDocked();
            }
        }

        public GridLength TitleBarHeightGridLength
        {
            get => new GridLength(TitleBarHeight + ResizeBorder);
        }

        public Thickness ResizeBorderThickness
        {
            get => new Thickness(ResizeBorder + OuterMarginSize);
        }

        public Thickness InnerContentPadding
        {
            get => new Thickness(_contentPadding);
        }

        public Thickness OuterMarginSizeThickness
        {
            get => new Thickness(OuterMarginSize);
        }

        public CornerRadius WindowCornerRadius
        {
            get => new CornerRadius(WindowRadius);
        }

        public int OuterMarginSize
        {
            get => Borderless ? 0 : _outerMarginSize;
        }

        public int WindowRadius
        {
            get => Borderless ? 0 : _windowRadius;
        }

        public ICommand WindowLoadedCommand { get; private set; }
        public ICommand StateChangedCommand { get; private set; }
        public ICommand SizeChangedCommand { get; private set; }
        public ICommand WindowMinimizeCommand { get; private set; }
        public ICommand WindowMaximizeCommand { get; private set; }
        public ICommand WindowCloseCommand { get; private set; }
        public ICommand TranslationsViewCommand { get; private set; }
        public ICommand FilesViewCommand { get; private set; }
        public ICommand SettingsViewCommand { get; private set; }

        private readonly TranslationsViewModel _translationsViewModel;
        private readonly FilesViewModel _filesViewModel;
        private readonly SettingsViewModel _settingsViewModel;

        public BaseViewModel ViewModel { get; set; }

        public ShellViewModel(TranslationsViewModel translationsViewModel, FilesViewModel filesViewModel, SettingsViewModel settingsViewModel)
        {
            InitCommands();

            _translationsViewModel = translationsViewModel;
            _filesViewModel = filesViewModel;
            _settingsViewModel = settingsViewModel;

            ViewModel = _translationsViewModel;
        }

        private void InitCommands()
        {
            WindowLoadedCommand = new RelayCommand(param => {
                _windowState = ((Window)param).WindowState;
                _dockHandler = new DockHandler((Window)param);
                OnWindowResized();
            });

            StateChangedCommand = new RelayCommand(param => {
                _windowState = ((Window)param).WindowState;
                OnWindowResized();
            });

            SizeChangedCommand = new RelayCommand(param => {
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

            TranslationsViewCommand = new RelayCommand(param => {
                ViewModel = _translationsViewModel;
                OnPropertyChanged(nameof(ViewModel));
            });

            FilesViewCommand = new RelayCommand(param => {
                ViewModel = _filesViewModel;
                OnPropertyChanged(nameof(ViewModel));
            });

            SettingsViewCommand = new RelayCommand(param => {
                ViewModel = _settingsViewModel;
                OnPropertyChanged(nameof(ViewModel));
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
