using Android.App;
using Android.Media;
using Android.Widget;
using Android.OS;

namespace AudioDemo
{
    [Activity(Label = "AudioDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            MediaPlayer mediaPlayer=MediaPlayer.Create(this,Resource.Raw.rip);
            mediaPlayer.Start();
        }
    }
}

