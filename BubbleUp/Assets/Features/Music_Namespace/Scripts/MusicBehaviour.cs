using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Audio_Namespace.Scripts
{
    /**
     * Handles playing and stopping background music.
     */
    public class MusicBehaviour : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> soundTracks;

        private int _currentTrackIndex;
        
        /**
         * Starts the FadeTrack routine to disable current playing track
         * @param musicFadeTime the time it takes to fade the current track
         */
        public void Disable(float musicFadeTime)
        {
            StartCoroutine(FadeTrack(soundTracks[_currentTrackIndex], 0, musicFadeTime));
        }
        
        /**
         * Starts the PlayNextTrack routine to enable/play next track
         */
        public void Enable()
        {
            gameObject.SetActive(true);
        
            StartCoroutine(PlayNextTrack());
        }

        private void Awake()
        {
            foreach (AudioSource track in soundTracks)
            {
                track.volume = 0.01f;
            }
            _currentTrackIndex = Random.Range(0, soundTracks.Count);
            StartCoroutine(PlayNextTrack());
        }
        
        /**
         * Plays the next track in the list based on the index
         */
        private IEnumerator PlayNextTrack()
        {
            while (true)
            {
                if (_currentTrackIndex >= soundTracks.Count - 1)
                {
                    _currentTrackIndex = 0;
                }
                else
                {
                    _currentTrackIndex++;
                }
                soundTracks[_currentTrackIndex].Play();
                yield return new WaitForSeconds(soundTracks[_currentTrackIndex].clip.length);
            }
        }
        
        /**
         * Fades the currently playing track, slowly decreasing volume over a certain duration
         * @param audioSource the selected audio source to fade
         * @param toVal the value to bring the volume down to
         * @param duration the duration over which the fade happens
         */
        private IEnumerator FadeTrack(AudioSource audioSource, float toVal, float duration)
        {
            float counter = 0f;
            float startVolume = audioSource.volume;

            while (counter < duration)
            {
                if (Time.timeScale == 0)
                    counter += Time.unscaledDeltaTime;
                else
                    counter += Time.deltaTime;
            
                audioSource.volume = Mathf.Lerp(startVolume, toVal, counter / duration);
                yield return null;
            }
        
            gameObject.SetActive(false);
            audioSource.volume = startVolume;
            StopAllCoroutines();
        }
    }
}