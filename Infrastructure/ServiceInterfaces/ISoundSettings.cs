using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundSettings
    {
        void ToggleSound();

        void IncreaseSongSound(int i_NumberToIncrease);

        void IncreaseSoundEffectsSound(int i_NumberToIncrease);

        void DecreaseSoundEffectsSound(int i_NumberToDecrease);

        void DecreaseSongSound(int i_NumberToDecrease);
    }
}