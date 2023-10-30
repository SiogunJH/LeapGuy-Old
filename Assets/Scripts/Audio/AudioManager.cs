using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AudioManagerLib
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] static Dictionary<string, AudioPlayer> audioPlayer;
        private static AudioManager Instance { get; set; }

        private void Awake()
        {
            // Dont destroy on load
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // On time functions
            CreateAudioDictionary();
        }
        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.StartsWith("Level "))
            {
                PlaySound("Level Theme");
            }
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
                Debug.LogError($"Sound [{soundName}] is not present in [Audio Player Dictionary]! Check child object's names of [Audio Manager] for verification, stupid.");
                return;
            }

            // Play sound
            audioPlayer[soundName].PlaySound();
        }
    }
}