﻿using Android.App;
using Android.Content;
using Android.Media;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Threading;


namespace AudioDemo
{
    [Activity(Label = "AudioDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _buttonStart;
        private Button _buttonPause;
        private SeekBar _seekBarVolumeControl;
        private SeekBar _seekBarPlayer;
        private MediaPlayer _mediaPlayer;
        private AudioManager _audioManager;
        private Timer _timer;
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
            
            _audioManager = (AudioManager) GetSystemService(Context.AudioService);
            int maxVolume = _audioManager.GetStreamMaxVolume(Stream.Music);
            int currentVolume = _audioManager.GetStreamVolume(Stream.Music);


            _seekBarVolumeControl = FindViewById<SeekBar>(Resource.Id.seekBarVolumeControl);
            _seekBarVolumeControl.Max = maxVolume;
            _seekBarVolumeControl.Progress = currentVolume;
            _seekBarVolumeControl.ProgressChanged += _seekBarVolumeControl_ProgressChanged;

            _seekBarPlayer = FindViewById<SeekBar>(Resource.Id.seekBarPlayer);
            _seekBarPlayer.Max = _mediaPlayer.Duration;
            _seekBarPlayer.ProgressChanged += _seekBarPlayer_ProgressChanged;
        }

        private void _seekBarPlayer_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            _mediaPlayer.SeekTo(e.Progress);
        }

        private void _seekBarVolumeControl_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            Log.Info("Seekbar Value", e.Progress.ToString());
            _audioManager.SetStreamVolume(Stream.Music, e.Progress,VolumeNotificationFlags.ShowUi);
        }

        private void _buttonPause_Click(object sender, System.EventArgs e)
        {
            _mediaPlayer.Pause();
            _timer?.Dispose();
        }

        private void _buttonStart_Click(object sender, System.EventArgs e)
        {
            _mediaPlayer.Start();
            _timer =new Timer(state =>
            {
                new Thread(() =>
                {
                    if (!_mediaPlayer.IsPlaying)
                        _timer.Dispose();

                    _seekBarPlayer.Progress = _mediaPlayer.CurrentPosition;
                }).Start();
                
            },null,0,1000);
        }
    }
}

