using UnityEngine;

namespace DefaultNamespace.util.unity
{
    public class ProjectileSpawnUtil
    {
        public static void spawnProjectile(UnityProjectile unityProjectileObject)
        {
            Object.Instantiate(unityProjectileObject);
        }
    }
}