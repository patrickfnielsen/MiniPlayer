using System.Windows;
using System.Windows.Controls;

namespace MiniPlayer.View
{
    public partial class PlayBar : UserControl
    {
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(double), typeof(PlayBar), new PropertyMetadata(TimeValueChanged));
        public static readonly DependencyProperty TotalTimeProperty = DependencyProperty.Register("TotalTime", typeof(double), typeof(PlayBar), new PropertyMetadata(TotalTimeValueChanged));

        public PlayBar()
        {
            InitializeComponent();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            ValueChanged(Time);
        }

        public double TotalTime
        {
            get { return (double)GetValue(TotalTimeProperty); }
            set { SetValue(TotalTimeProperty, value); }
        }

        public double Time
        {
            get { return (double)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        private static void TimeValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((PlayBar)sender).ValueChanged(e.NewValue);
        }

        private static void TotalTimeValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((PlayBar)sender).TotalValueChanged();
        }

        private void ValueChanged(object parameter)
        {
            var totalTime = (double)GetValue(TotalTimeProperty);
            var currentTime = (double)parameter;
            if (currentTime < 0) return;

            Bar.Width = totalTime != 0 ? (currentTime * ActualWidth) / totalTime : 0;
        }

        private void TotalValueChanged()
        {
            Bar.Width = 0;
        }
    }
}
