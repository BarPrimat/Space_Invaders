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
    public class SoundSettings : GameService
    {
        private readonly ISoundManager r_SoundManager;

        public SoundSettings(Game i_Game) : base(i_Game)
        {
            r_SoundManager = i_Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ISoundSettings), this);
        }

        public void ToggleSound()
        {
            r_SoundManager.ToggleSound();
        }

        public void IncreaseSongSound(int i_NumberToIncrease)
        {
            //r_SoundManager.U
           // MediaPlayer.Volume = calculateNewValueOfVolume(MediaPlayer.Volume + i_NumberToIncrease);
        }

        public void IncreaseSoundEffectsSound(int i_NumberToIncrease)
        {
            /*
            foreach(SoundEffectInstance soundEffectInstance in r_SoundManager.SoundsEffects.Values)
            {
                soundEffectInstance.Volume = calculateNewValueOfVolume(soundEffectInstance.Volume - i_NumberToIncrease);
            }
            */
        }

        public void DecreaseSoundEffectsSound(int i_NumberToDecrease)
        {
            // MediaPlayer.Volume = calculateNewValueOfVolume(MediaPlayer.Volume - i_NumberToDecrease);
        }

        public void DecreaseSongSound(int i_NumberToDecrease)
        {
            /*
            foreach (SoundEffectInstance soundEffectInstance in r_SoundManager.SoundsEffects.Values)
            {
                soundEffectInstance.Volume = calculateNewValueOfVolume(soundEffectInstance.Volume - i_NumberToDecrease);
            }
            */
        }
    }
}