using UnityEngine;

namespace Spatialminds.Platformer
{
    public interface ICollidable
    {
        public void OnCollided(GameObject other);
    }
}
