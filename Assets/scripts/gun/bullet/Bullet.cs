using System.Collections.Generic;
using player.decal;
using player.health;
using UnityEngine;
using util;

namespace gun.bullet
{
    public class Bullet : MonoBehaviour, PersonDamager
    {
        private Rigidbody rigidbody;
        private float wobble;
        private float loft;
        private float initialVel;
        private Vector2 initialDirectionOffset;
        private bool wobbleDirection = false;
        [SerializeField] private float PERSON_OUCH_AMOUNT = 42;


        private void Awake()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rigidbody.velocity = initialVel * transform.forward;
            rigidbody.AddForce(loft * transform.up);
        }

        private void FixedUpdate()
        {
            Transform t = transform;
            if (wobbleDirection)
            {
                rigidbody.AddForce(t.right * wobble);
                wobbleDirection = false;
            }
            else
            {
                rigidbody.AddForce(-t.right * wobble);
                wobbleDirection = true;
            }

            RaycastHit hit;
            bool didhit = Physics.Raycast(
                t.position, 
                t.forward, 
                out hit,
                (rigidbody.velocity * Time.fixedDeltaTime).magnitude, 
                Physics.AllLayers,
                QueryTriggerInteraction.Collide);
            
            if (didhit)
            {
                var bulletDecalSpawners = new List<BulletDecalSpawner>();
                InterfaceUtility.GetInterfaces(out bulletDecalSpawners, hit.transform.gameObject);
                foreach (BulletDecalSpawner spawner in bulletDecalSpawners)
                {
                    spawner.spawnDecal(hit.point, hit.normal);
                }
                GameObject.Destroy(transform.gameObject);
            }
            Debug.DrawRay(t.position, t.up);
        }

        public Bullet setWobble(float wobble)
        {
            this.wobble = wobble;
            return this;
        }

        public Bullet setLoft(float loft)
        {
            this.loft = loft;
            return this;
        }

        public Bullet setInitialVel(float initialVel)
        {
            this.initialVel = initialVel;
            return this;
        }

        public Bullet setDirectionOffset(Vector2 offset)
        {
            this.initialDirectionOffset = offset;
            return this;
        }

        public float getDamage()
        {
            return PERSON_OUCH_AMOUNT;
        }
    }
}