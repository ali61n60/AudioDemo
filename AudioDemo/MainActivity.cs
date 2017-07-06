using Android.App;
using Android.Media;
using Android.Widget;
using Android.OS;

namespace AudioDemo
{
    [Activity(Label = "AudioDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _buttonStart;
        private Button _buttonPause;
        private MediaPlayer _mediaPlayer;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            initFields();
            
        }

        private void initFields()
        {
            _mediaPlayer = MediaPlayer.Create(this, Resource.Raw.rip);

            _buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            _buttonStart.Click += _buttonStart_Click;

            _buttonPause = FindViewById<Button>(Resource.Id.buttonPause);
            _buttonPause.Click += _buttonPause_Click;
        }

        private void _buttonPause_Click(object sender, System.EventArgs e)
        {
            _mediaPlayer.Pause();
        }

        private void _buttonStart_Click(object sender, System.EventArgs e)
        {
            _mediaPlayer.Start();
        }
    }
}

