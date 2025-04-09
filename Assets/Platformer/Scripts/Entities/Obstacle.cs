using UnityEngine;
using Spatialminds.Platformer;

public class Obstacle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }    
}
