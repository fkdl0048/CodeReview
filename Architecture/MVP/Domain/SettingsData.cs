using System;
using UnityEngine;

namespace Runtime.Common.Domain
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObject/SettingsData", order = 0)]
    public class SettingsData : ScriptableObject
    {
        [SerializeField][Range(0, 1)] private float musicVolume;
        [SerializeField][Range(0, 1)] private float sfxVolume;
        
        public float MusicVolume
        {
            get => musicVolume;
            set { Math.Clamp(value, 0, 1); musicVolume = value; }
        }
        
        public float SfxVolume
        {
            get => sfxVolume;
            set { Math.Clamp(value, 0, 1); sfxVolume = value; }
        }
    }
}