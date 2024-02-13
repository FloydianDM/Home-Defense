using UnityEngine;

namespace HomeDefense
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _towerSFX;
        [SerializeField] private AudioClip _killSFX;
        [SerializeField] private AudioClip _stealSFX;

        private AudioSource _audioPlayer;

        private void Start()
        {
            _audioPlayer = GetComponent<AudioSource>();
        }

        public void PlayCreateTowerSFX()
        {
            _audioPlayer.PlayOneShot(_towerSFX);
        }

        public void PlayKillSFX()
        {
            if (_audioPlayer.isPlaying)
            {
                _audioPlayer.Stop();
            }

            _audioPlayer.PlayOneShot(_killSFX);
        }

        public void PlayStealSFX()
        {
            if (_audioPlayer.isPlaying)
            {
                _audioPlayer.Stop();
            }
            
            _audioPlayer.PlayOneShot(_stealSFX);
        }
    }
}

