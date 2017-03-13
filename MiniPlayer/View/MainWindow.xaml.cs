using System.Windows;
using System.Windows.Input;
using MiniPlayer.Objects;

namespace MiniPlayer.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = FindResource("Locator") as ViewModelLocator;
            var context = dataContext?.Main as IClosing;
            if (context != null)
            {
                e.Cancel = !context.OnClosing();
            }
        }
    }
}
