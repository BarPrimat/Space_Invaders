using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Infrastructure.Managers
{
    public class SoundManager : GameService , ISoundManager
    {
        private const float k_MaxValueOfVolume = 1f;
        private const float k_MinValueOfVolume = 0f;
        private const float k_DivideOrMultipleValueByOneHundred = 100f;
        private readonly Dictionary<string, Song> r_Songs = new Dictionary<string, Song>();
        private readonly Dictionary<string, SoundEffect> r_SoundsEffects = new Dictionary<string, SoundEffect>();
        private int m_CurrentValueOfSongs = 100;
        private int m_CurrentValueOfSoundEffects = 100;
        private bool m_IsSongsMute = false;
        private bool m_IsSoundsEffectsMute = false;
        private bool m_AreAllSoundsMuted = false;

        public SoundManager(Game i_Game) : base(i_Game)
        {
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ISoundManager), this);
        }

        public void PlaySong(string i_SongName, bool i_IsRepeating)
        {
            if(!m_IsSongsMute)
            {
                MediaPlayer.Play(r_Songs[i_SongName]);
                MediaPlayer.IsRepeating = i_IsRepeating;
            }
        }

        public void PlaySoundEffect(string i_SoundEffectName)
        {
            if(!m_IsSoundsEffectsMute)
            {
                r_SoundsEffects[i_SoundEffectName].CreateInstance().Play();
            }
        }

        public void AddSong(string i_SongName, Song i_Song)
        {
            r_Songs.Add(i_SongName, i_Song);
        }

        public void AddSoundEffect(string i_SoundName, SoundEffect i_SoundEffect)
        {
            r_SoundsEffects.Add(i_SoundName, i_SoundEffect);
        }

        public void RemoveSongFormDictionary(string i_SongName)
        {
            r_Songs.Remove(i_SongName);
        }

        public void RemoveSoundEffectFormDictionary(string i_SoundName)
        {
            r_SoundsEffects.Remove(i_SoundName);
        }

        public void UpdateSoundEffectMasterVolume(int i_NewValueOfSoundEffectMasterVolume)
        {
            SoundEffect.MasterVolume = calculateNewValueOfVolume(i_NewValueOfSoundEffectMasterVolume / k_DivideOrMultipleValueByOneHundred);
            m_CurrentValueOfSoundEffects = (int) (SoundEffect.MasterVolume * k_DivideOrMultipleValueByOneHundred);
        }

        public void UpdateMediaPlayerVolume(int i_NewValueOfSoundEffectMasterVolume)
        {
            MediaPlayer.Volume = calculateNewValueOfVolume(i_NewValueOfSoundEffectMasterVolume / k_DivideOrMultipleValueByOneHundred);
            m_CurrentValueOfSongs = (int) (MediaPlayer.Volume * k_DivideOrMultipleValueByOneHundred);
        }

        private static float calculateNewValueOfVolume(float i_NewValue)
        {
            return MathHelper.Clamp(i_NewValue, k_MinValueOfVolume, k_MaxValueOfVolume);
        }

        public void ToggleSound()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            m_IsSongsMute = MediaPlayer.IsMuted;
            m_IsSoundsEffectsMute = MediaPlayer.IsMuted;
            m_AreAllSoundsMuted = MediaPlayer.IsMuted;
        }

        public bool IsSoundsEffectsMute
        {
            get => m_IsSoundsEffectsMute;
            set => m_IsSoundsEffectsMute = value;
        }

        public bool IsSongsMute
        {
            get => m_IsSongsMute;
            set => m_IsSongsMute = value;
        }

        public Dictionary<string, Song> Songs => r_Songs;

        public Dictionary<string, SoundEffect> SoundsEffects => r_SoundsEffects;

        public bool AreAllSoundsMuted => m_AreAllSoundsMuted;

        public int CurrentValueOfSongs => m_CurrentValueOfSongs;

        public int CurrentValueOfSoundEffects => m_CurrentValueOfSoundEffects;
    }
}