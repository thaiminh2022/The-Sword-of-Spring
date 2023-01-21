using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TheSwordOfSpring.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public List<MusicClip> musicClips = new List<MusicClip>();
        private List<GameObject> musicObjs = new List<GameObject>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void PlayMusic(string clipName, bool dontDestroy = false)
        {

            MusicClip clip = musicClips.Find(x => x.name == clipName);
            if (clip != null)
            {
                Transform parent = dontDestroy ? transform : null;
                GameObject go = new GameObject("4560-Clip-" + clipName);

                go.transform.parent = parent;
                musicObjs.Add(go);

                AudioSource src = go.AddComponent<AudioSource>();
                src.clip = clip.clip;
                src.volume = clip.volume;
                src.Play();
            }
            else
            {
                Debug.LogWarning("Cannot find clip");
            }
        }

        public void StopAllMusic()
        {
            for (int i = 0; i < musicObjs.Count; i++)
            {
                Destroy(musicObjs[i]);
            }
        }
        public void StopMusic(string clipName)
        {
            var clipObjs = musicObjs.FindAll(item => item.name == $"4560-Clip-{clipName}");
            for (int i = 0; i < clipObjs.Count; i++)
            {
                Destroy(clipObjs[i]);
            }
        }
    }

    [System.Serializable]
    public class MusicClip
    {
        public string name;
        public AudioClip clip;

        [Range(0, 1)]
        public float volume;
    }
}