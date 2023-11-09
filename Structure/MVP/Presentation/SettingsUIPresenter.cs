using Runtime.Common.Domain;
using Runtime.Common.View;

namespace Runtime.Common.Presentation
{
    public class SettingsUIPresenter
    {
        private readonly SettingsUIView _settingsUIView;
        private readonly SettingsData _settingsData;
        
        public SettingsData SettingsData => _settingsData;

        public SettingsUIPresenter(SettingsUIView settingsUIView, SettingsData settingsData)
        {
            _settingsUIView = settingsUIView;
            _settingsData = settingsData;
            
            _settingsUIView.SetViewMusicVolume(_settingsData.MusicVolume);
            _settingsUIView.SetViewSfxVolume(_settingsData.SfxVolume);
        }

        public void SetMusicVolume(float volume)
        {
            _settingsData.MusicVolume = volume;
            _settingsUIView.SetViewMusicVolume(_settingsData.MusicVolume);
        }
        
        public void SetSfxVolume(float volume)
        {
            _settingsData.SfxVolume = volume;
            _settingsUIView.SetViewSfxVolume(_settingsData.SfxVolume);
        }
    }
}