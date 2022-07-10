using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Audio_Namespace.Scripts
{
    public class MusicBehaviour : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> soundTracks;

        private int currentTrackIndex;
    
        public void Disable(float musicFadeTime)
        {
            StartCoroutine(FadeTrack(soundTracks[currentTrackIndex], 0, musicFadeTime));
        }
    
        public void Enable()
        {
            gameObject.SetActive(true);
        
            StartCoroutine(PlayNextTrack());
        }

        private void Awake()
        {
            currentTrackIndex = Random.Range(0, soundTracks.Count);
            StartCoroutine(PlayNextTrack());
        }

        private IEnumerator PlayNextTrack()
        {
            while (true)
            {
                if (currentTrackIndex >= soundTracks.Count - 1)
                {
                    currentTrackIndex = 0;
                }
                else
                {
                    currentTrackIndex++;
                }
                soundTracks[currentTrackIndex].Play();
                yield return new WaitForSeconds(soundTracks[currentTrackIndex].clip.length);
            }
        }
    
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