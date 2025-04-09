using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Spatialminds.Platformer
{
    public partial class AudioPlayerService : MonoBehaviour
    {
        public enum AudioType
        {
            SFX,
            Ambient,
            Background
        }

        IObjectPool<AudioSource> audioSourcePool;

        private Dictionary<string, AudioClip> audioClips;

        [SerializeField] private AudioObject[] audioObjects;
        public bool collectionChecks = true;
        public int maxPoolSize = 10;

        public IObjectPool<AudioSource> Pool
        {
            get
            {
                if(audioSourcePool == null)
                {
                    audioSourcePool = new ObjectPool<AudioSource>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
                }
                return audioSourcePool;
            }

            
        }

        void Awake()
        {
            audioClips = new Dictionary<string, AudioClip>();

            foreach (var audioObject in audioObjects)
            {
                audioClips.TryAdd(audioObject.audioName, audioObject.audioClip);
            }
        }

        AudioSource CreatePooledItem()
        {
            var audioObject = new GameObject("Pooled-Audio");
            audioObject.transform.SetParent(transform);
            var audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.Stop();

            //Return the object to the pool once the 

            return audioSource;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(AudioSource system)
        {
            system.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(AudioSource system)
        {
            system.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(AudioSource system)
        {
            Destroy(system.gameObject);
        }
        

        public void PlayAudio(string audioClipName)
        {
            var audio = Pool.Get();
            if(audioClips.TryGetValue(audioClipName, out AudioClip clip))
            {
                audio.PlayOneShot(clip);
                Release(audio, clip.length);
            }
            
        }

        public void Release(AudioSource audioSource)
        {
            audioSourcePool.Release(audioSource);
        }

        public void Release(AudioSource audioSource, float time)
        {
            StartCoroutine(INUReleaseAfterTime(audioSource, time));
        }
        
        IEnumerator INUReleaseAfterTime(AudioSource audioSource, float time)
        {
            yield return new WaitForSeconds(time);
            Release(audioSource);
        }


        [System.Serializable]
        public class AudioObject
        {
            public string audioName;
            public AudioClip audioClip;
        }
    }
}
