using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class CollisionDetector : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            ICollidable collider = other.GetComponent<ICollidable>();
            if(!collider.Equals(null))
            {
                collider.OnCollided(gameObject);
            }
        }
    }
}
