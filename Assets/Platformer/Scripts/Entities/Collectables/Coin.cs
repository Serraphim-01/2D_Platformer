using Unity.VisualScripting;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class Coin : MonoBehaviour, ICollectable, ICollidable
    {
        string who;
        private CoinCollectorService coinCollectorService;
        private AudioPlayerService audioPlayerService;

        void Start()
        {
            coinCollectorService = ServiceLocator.instance.GetService<CoinCollectorService>();
            audioPlayerService = ServiceLocator.instance.GetService<AudioPlayerService>();
        }


        public void OnCollected()
        {
            Debug.Log($"Collected the coin by {who}");
            coinCollectorService.CollectCoin();
            audioPlayerService.PlayAudio("coin");
            Destroy(gameObject);
        }

        public void OnCollided(GameObject other)
        {
            who = other.name;
            OnCollected();
        }
    }
}
