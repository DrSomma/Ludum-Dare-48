using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public GameObject soundNodePrefab;
        public AudioClip uiClick;
        public AudioClip drillSound;
        public AudioClip breakSound;


        [Header("For Background Music")]
        [SerializeField] private bool mute;
        [SerializeField] private float soundVolume;
        public AudioSource musicSource;
        public AudioClip ambientSound;
        public AudioClip musicStart;
        public AudioClip musicLoop;

        private AudioSource _ambientSoundAudioSource;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
        }

        public void Start()
        {
            if (musicStart != null)
            {
                musicSource.PlayOneShot(musicStart);
            }

            if (musicLoop != null)
            {
                musicSource.clip = musicLoop;
                musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
                musicSource.loop = true;
            }

            if (ambientSound != null)
            {
                GameObject soundNode = Instantiate(soundNodePrefab);

                _ambientSoundAudioSource = soundNode.GetComponent<AudioSource>();

                _ambientSoundAudioSource.clip = ambientSound;
                _ambientSoundAudioSource.Play();
            }

            // StartCoroutine(routine: PlayBirdSounds());



            //if (!MuteMusic)
            //{
            //    musicSource.Play();
            //}
            //musicSource.loop = true;
        }

        public void Update()
        {
            if (mute)
            {
                musicSource.mute = true;
            }
            else
            {
                musicSource.mute = false;
            }

            if (_ambientSoundAudioSource != null)
            {
                _ambientSoundAudioSource.mute = mute;
            }
        }

        // private IEnumerator PlayBirdSounds()
        // {
        //     // for(;;)
        //     // {
        //     //     PlayRandomBirdSound();
        //     //     yield return new WaitForSeconds(seconds: Random.Range( min: 5f, max: 15f));
        //     // }
        // }

        public void PlaySoundUiClick()
        {
            PlaySound(audioClip: uiClick);
        }

        GameObject drillNode;
        public void PlayDrillSound()
        {
            drillNode = PlaySound(audioClip: drillSound,0.5f);
            Debug.Log("Play drill");
            
        }

        public void StopDrillSound()
        {
            if(drillNode != null)
            {
                Destroy(drillNode);
                Debug.Log("Stop drill");
            }
        }

        public void PlayBreakSound()
        {
            PlaySound(audioClip: breakSound);
            Debug.Log("Play break");
        }

        private GameObject PlaySound(AudioClip audioClip, float p=1f, bool doLoop=false)
        {
            if (mute)
            {
                return null;
            }

            float audioClipLength = audioClip.length;

            GameObject soundNode = Instantiate(soundNodePrefab);

            AudioSource audioSource = soundNode.GetComponent<AudioSource>();

            audioSource.volume = soundVolume * p;
            audioSource.loop = doLoop;
            if(!doLoop)
                Destroy(obj: soundNode, t: audioClipLength);

            audioSource.clip = audioClip;
            audioSource.Play();

            return soundNode;
        }

        public void SwitchMute()
        {
            mute = !mute;
        }

        public void SetMusicVolume(float volume)
        {
            this.musicSource.volume = volume;
        }

        public void SetSoundVolume(float volume)
        {
            this.soundVolume = volume;
        }
    }
}