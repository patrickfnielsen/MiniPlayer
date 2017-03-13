using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MiniPlayer.View
{
    public class Marquee : UserControl
    {
        private bool _isLoaded;
        private readonly Storyboard _storyboard = new Storyboard();
        private readonly DoubleAnimationUsingKeyFrames _animation = new DoubleAnimationUsingKeyFrames();
        private FrameworkElement _contentPart;

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(double), typeof(Marquee), new PropertyMetadata(DurationValueChanged));
        public static readonly DependencyProperty InitialPauseProperty = DependencyProperty.Register("InitialPause", typeof(double), typeof(Marquee), new PropertyMetadata(0d));
        public static readonly DependencyProperty FinalPauseProperty = DependencyProperty.Register("FinalPause", typeof(double), typeof(Marquee), new PropertyMetadata(0d));

        public Marquee()
        {
            DefaultStyleKey = typeof(Marquee);
            Loaded += MarqueeLoaded;
        }

        public double InitialPause
        {
            get { return (double)GetValue(InitialPauseProperty); }
            set { SetValue(InitialPauseProperty, value); }
        }

        public double FinalPause
        {
            get { return (double)GetValue(FinalPauseProperty); }
            set { SetValue(FinalPauseProperty, value); }
        }

        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        static void DurationValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Marquee)sender).RestartAnimation();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPart = GetTemplateChild("PART_Content") as FrameworkElement;
            SizeChanged += MarqueeSizeChanged;
            if (_contentPart != null)
                _contentPart.SizeChanged += MarqueeSizeChanged;
        }

        private void MarqueeLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            RestartAnimation();
            MouseEnter += MarqueeMouseEnter;
            MouseLeave += MarqueeMouseLeave;
        }

        private void MarqueeSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RestartAnimation();
        }

        private void MarqueeMouseLeave(object sender, MouseEventArgs e)
        {
            _storyboard.Resume();
        }

        private void MarqueeMouseEnter(object sender, MouseEventArgs e)
        {
            _storyboard.Pause();
        }

        private void RestartAnimation()
        {
            if (ActualHeight == 0)
            {
                _storyboard?.Stop();
                return;
            }

            if (_contentPart == null || !_isLoaded) return;
            _storyboard.Stop();

            var value = InitialPause + Duration * _contentPart.ActualWidth / ActualWidth;
            _animation.Duration = new Duration(TimeSpan.FromSeconds(value));

            _animation.KeyFrames.Clear();
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame()
            {
                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))
            });

            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame()
            {
                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(InitialPause))
            });

            _animation.KeyFrames.Add(new SplineDoubleKeyFrame()
            {
                Value = Math.Min(ActualWidth - _contentPart.ActualWidth - 12, 0),
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(value)),
                KeySpline = new KeySpline()
                {
                    ControlPoint1 = new Point(0, 0),
                    ControlPoint2 = new Point(1, 1)
                }
            });
            _storyboard.Duration = new Duration(TimeSpan.FromSeconds(value + FinalPause));

            if (_storyboard.Children.Count == 0)
            {
                _storyboard.Children.Add(_animation);
                Storyboard.SetTargetProperty(_animation, new PropertyPath("(Canvas.Left)"));
                Storyboard.SetTarget(_animation, _contentPart);

                _storyboard.RepeatBehavior = RepeatBehavior.Forever;
            }

            _storyboard.Begin();
        }
    }
}
