using NSubstitute;
using NUnit.Framework;
using Runtime.Common.Domain;
using Runtime.Common.Presentation;
using Runtime.Common.View;
using UnityEngine;

namespace Tests.Editor
{
    [TestFixture]
    public class SettingsUIPresenterTests
    {
        private SettingsUIView _view;
        private SettingsData _model;
        private SettingsUIPresenter _presenter;
        
        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<SettingsUIView>();
            _model = ScriptableObject.CreateInstance<SettingsData>();
            _presenter = new SettingsUIPresenter(_view, _model);
            
            _model.MusicVolume = 0.5f;
            _model.SfxVolume = 0.5f;
        }
        
        [Test]
        public void CheckInitialValue()
        {
            Assert.AreEqual(0.5f, _model.MusicVolume);
            Assert.AreEqual(0.5f, _model.SfxVolume);
        }
        
        [Test]
        public void WhenMusicVolumeChangesViewIsUpdated()
        {
            _presenter.SetMusicVolume(0.75f);
            _view.Received(1).SetViewMusicVolume(0.75f);
        }
        
        [Test]
        public void WhenSfxVolumeChangesViewIsUpdated()
        {
            _presenter.SetSfxVolume(0.3f);
            _view.Received(1).SetViewSfxVolume(0.3f);
        }
        
        [Test]
        public void WhenMusicVolumeChangesModelIsUpdated()
        {
            _presenter.SetMusicVolume(0.75f);
            Assert.AreEqual(0.75f, _model.MusicVolume);
        }
        
        [Test]
        public void WhenSfxVolumeChangesModelIsUpdated()
        {
            _presenter.SetSfxVolume(0.3f);
            Assert.AreEqual(0.3f, _model.SfxVolume);
        }
        
        [Test]
        public void WhenMusicVolumeChagesEqualModelAndPresenter()
        {
            _model.MusicVolume = 0.31f;
            Assert.AreEqual(_presenter.SettingsData.MusicVolume, _model.MusicVolume);
        }
        
    }
}
