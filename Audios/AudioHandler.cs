using System.IO;
using System;
using System.Media;

namespace TurnBased_RPG_Battle_System.Audios
{
    public class AudioHandler
    {
        // Player object
        private SoundPlayer SoundPlayer { get; set; }

        // Constructor
        public AudioHandler() { setAudioObj(new SoundPlayer()); }

        /// <summary>
        /// Responsable for playing an audio.
        /// </summary>
        /// <param name="file">Name of the file</param>
        public void PlaySound(string file)
        {
            // Path where the sound is located
            returnAudioObj().SoundLocation = Path.Combine(Environment.CurrentDirectory + "/music/", file);

            // Playing the audio [Looping]
            returnAudioObj().PlayLooping();
        }

        /// <summary>
        /// Responsable for playing an sfx 
        /// </summary>
        /// <param name="file">Name of the file</param>
        public void PlaySFX(string file)
        {
            // Path where the sfx is located
            returnAudioObj().SoundLocation = Path.Combine(Environment.CurrentDirectory + "/sfx/", file);

            // Playing the sfx
            returnAudioObj().Play();
        }

        /// <summary>
        /// Responsable for stoping the current playing song.
        /// </summary>
        public void StopSound()
        {
            // stopping the current audio
            returnAudioObj().Stop();
        }

        /// <summary>
        /// Encapsulated return of the audio obj
        /// </summary>
        /// <returns>Returns the audio object</returns>
        public SoundPlayer returnAudioObj() { return this.SoundPlayer; }

        /// <summary>
        /// Encapsulated audio object creation
        /// </summary>
        /// <param name="_soundObj">Audio object to be created.</param>
        public void setAudioObj(SoundPlayer _soundObj) { this.SoundPlayer = _soundObj; }

    }
}
