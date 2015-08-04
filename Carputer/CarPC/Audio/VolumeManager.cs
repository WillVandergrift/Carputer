using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreAudioApi;

namespace CarPC.Audio
{
    public enum MuteCommand
    {
        Mute,
        UnMute,
        Toggle
    }

    /// <summary>
    /// This class provides information and control over the system's master volume
    /// </summary>
    public static class VolumeManager
    {
        private static MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
        private static MMDevice systemVolume;

        //Default Constructor
        static VolumeManager()
        {
            //Get the master volume device
            systemVolume = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        }

        /// <summary>
        /// Mute the system volume
        /// </summary>
        public static void Mute(MuteCommand cmd)
        {
            switch (cmd)
            {
                case MuteCommand.Mute:
                    systemVolume.AudioEndpointVolume.Mute = true;
                    break;
                case MuteCommand.UnMute:
                    systemVolume.AudioEndpointVolume.Mute = false;
                    break;
                case MuteCommand.Toggle:
                    systemVolume.AudioEndpointVolume.Mute = !systemVolume.AudioEndpointVolume.Mute;
                    break;
            }
        }

        /// <summary>
        /// Get the scaled master volume level
        /// </summary>
        /// <returns></returns>
        public static float MasterVolumeLevel()
        {
            return systemVolume.AudioEndpointVolume.MasterVolumeLevelScalar;
        }

        /// <summary>
        /// Set the master volume level
        /// </summary>
        public static void SetLevel(float level)
        {
            //If we're muted, unmute if the volume is being raised
            if (systemVolume.AudioEndpointVolume.Mute)
            {
                if (level > systemVolume.AudioEndpointVolume.MasterVolumeLevel)
                {
                    systemVolume.AudioEndpointVolume.Mute = false;
                }
            }

            //Set the volume
            systemVolume.AudioEndpointVolume.MasterVolumeLevelScalar = level;
        }
    }
}
