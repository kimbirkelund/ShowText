using System;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Timer = System.Timers.Timer;

namespace ShowText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IDisposable
    {
        private readonly Timer _timer;

        public string Text
        {
            get => Label.Content is string t
                       ? t
                       : $"{Label.Content}";
            set => Label.Content = value;
        }

        public MainWindow()
        {
            InitializeComponent();

            _timer = new Timer(TimeSpan.FromSeconds(30)
                                       .TotalMilliseconds);
            _timer.Elapsed += _timer_Elapsed;

            Text = string.Join(" ",
                               Environment.GetCommandLineArgs()
                                          .Skip(1));
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(Close));
        }

        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Left = Screen.PrimaryScreen.Bounds.Width - Width - 50;
            _timer.Start();
        }
    }
}
