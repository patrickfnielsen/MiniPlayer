using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MiniPlayer.Helper;
using MiniPlayer.Objects;

namespace MiniPlayer.View
{
    public partial class PlayerControl : UserControl
    {
        public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(IPlayer), typeof(PlayerControl), new PropertyMetadata(PlayerValueChanged));

        public PlayerControl()
        {
            InitializeComponent();

            ButtonPrevius.MouseLeftButtonDown += PreviusOnMouseLeftButtonDown;
            ButtonPlay.MouseLeftButtonDown += PlayOnMouseLeftButtonDown;
            ButtonPause.MouseLeftButtonDown += PauseOnMouseLeftButtonDown;
            ButtonNext.MouseLeftButtonDown += NextOnMouseLeftButtonDown;
        }

        public IPlayer Player
        {
            get { return (IPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        private static void PlayerValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (PlayerControl)sender;
            control.Player.OnPlayStateChange += control.OnPlayStateChange;

            if (control.Player.IsPlaying)
                control.ShowPauseButton();
        }

        private void ShowPlayButton()
        {
            ButtonPlay.Visibility = Visibility.Visible;
            ButtonPause.Visibility = Visibility.Hidden;
        }

        private void ShowPauseButton()
        {
            ButtonPlay.Visibility = Visibility.Hidden;
            ButtonPause.Visibility = Visibility.Visible;
        }

        private void OnPlayStateChange(object sender, PlayStateEventArgs e)
        {
            if (e.Playing)
            {
                Execute.OnUIThread(ShowPauseButton);
            }
            else
            {
                Execute.OnUIThread(ShowPlayButton);
            }
        }

        private void NextOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Player.Next();
        }

        private void PlayOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ShowPauseButton();
            Player.Play();
        }

        private void PauseOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ShowPlayButton();
            Player.Pause();
        }

        private void PreviusOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Player.Previous();
        }
    }
}
