using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace AudioManagerLib
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] static Dictionary<string, AudioPlayer> audioPlayer;
        private static AudioManager Instance { get; set; }

        private void Awake()
        {
            // Set dont destroy on load
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // destory self
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // On time setup functions
            CreateAudioDictionary();
        }

        void CreateAudioDictionary()
        {
            audioPlayer = new();
            foreach (Transform child in transform)
            {
                audioPlayer.Add(child.gameObject.name, child.gameObject.GetComponent<AudioPlayer>());
            }
        }

        public static void PlaySound(string soundName)
        {
            // If the sound is not in dictionary
            if (!audioPlayer.ContainsKey(soundName))
            {
                Debug.LogError($"Sound {soundName} is not present in Audio Player Dictionary! Check child object's names of Audio Manager for verification, stupid.");
                return;
            }

            // Play sound
            audioPlayer[soundName].PlaySound();
        }
    }
}