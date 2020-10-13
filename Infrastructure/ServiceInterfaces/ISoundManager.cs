using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundManager
    {
        Dictionary<string, Song> Songs { get; }

        Dictionary<string, SoundEffect> SoundsEffects { get; }

        bool AreAllSoundsMuted { get; }

        int CurrentValueOfSongs { get; }

        int CurrentValueOfSoundEffects { get; }

        void PlaySoundEffect(string i_SoundEffectName);

        void AddSong(string i_SongName, Song i_Song);

        void AddSoundEffect(string i_SoundName, SoundEffect i_SoundEffect);

        void RemoveSongFormDictionary(string i_SongName);

        void RemoveSoundEffectFormDictionary(string i_SoundName);

        void UpdateSoundEffectMasterVolume(int i_NewValueOfSoundEffectMasterVolume);

        void UpdateMediaPlayerVolume(int i_NewValueOfSoundEffectMasterVolume);

        void ToggleSound();
    }
}