using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Planet
{
    public class Planet : MonoBehaviour
    {
        public enum PlanetStatus {Predicting, Orbiting, Crashed};
        public float ParticleDelay = 0.3f;
        public float ParticleLifeSpan = 3f;

        private PlanetStatus planetStatus;
        private Rigidbody2D rigbody2D;
        private SpriteRenderer spriteRenderer;
        private DistanceJoint2D joint;
        private Collider2D collider2D;
        private float lastPredictedParticleTime;

        void Start()
        {
            joint = GetComponent<DistanceJoint2D>();
            rigbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            this.collider2D = GetComponent<Collider2D>();
            lastPredictedParticleTime = Time.time;
        }

        public void PredictTrayectory(Vector3 gravityPull, float force)
        {
            //this.GetComponentInChildren<PlanetTrayectoryPredictor>().Predict(gravityPull, force);             
        }

        public void ReleasePlanet(Vector3 gravityPull, float force)
        {            
            foreach(var col in GetComponents<Collider2D>())
            {
                col.enabled = true;
            }
            if(this.rigbody2D != null)
            {
                this.rigbody2D.AddForce(gravityPull.normalized * force
                , ForceMode2D.Force);
                var particles = GetComponentsInChildren<PlanetTrayectoryPredictor>();
                foreach (var p in particles)
                {
                    DestroyImmediate(p.gameObject);
                }
            }            
        }

        public void OnGravitationalPulseForceApplied(Vector3 pullSource, float gravityPull, float radius)
        {
            //get the offset between the objects
            Vector3 pullDirection = pullSource - transform.position;
            //we're doing 2d physics, so don't want to try and apply z forces!
            pullDirection.z = 0;

            //get the squared distance between the objects
            float magsqr = pullDirection.sqrMagnitude;

            //check distance is more than 0 (to avoid division by 0) and then apply a gravitational force to the object
            //note the force is applied as an acceleration, as acceleration created by gravity is independent of the mass of the object

            if (magsqr <= radius)
            {
                spriteRenderer.enabled = false;
                rigbody2D.isKinematic = true;
                var gravitySources = FindObjectsOfType<GravitySource>();
                foreach (var gr in gravitySources)
                {
                    gr.OnGravityPulse -= OnGravitationalPulseForceApplied;
                }
                foreach (var col in GetComponents<CircleCollider2D>())
                {
                    col.radius = 0.01f;
                }
                Destroy(this, 0.5f);
            }                

            if (magsqr > 0.01f)
                rigbody2D.AddForce(gravityPull * pullDirection.normalized / magsqr, ForceMode2D.Force);
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            GravitySource source = coll.gameObject.GetComponent<GravitySource>();
            Planet planet = coll.gameObject.GetComponent<Planet>();
            if (source != null)
            {                
                if(source != null)
                {
                    source.OnGravityPulse -= OnGravitationalPulseForceApplied;
                    source.OnGravityPulse += OnGravitationalPulseForceApplied; 
                }
            }
            else if(planet != null)
            {
                AttachPlanets(planet);
            }
        }

        private void AttachPlanets(Planet p)
        {
            GameObject parent = null;
            var currentVelocity = this.rigbody2D.velocity;
            var collidingVelocity = p.rigbody2D.velocity;
            
            this.joint.connectedBody = p.rigbody2D;
               
            this.joint.enabled = true;
            this.rigbody2D.velocity = currentVelocity;
            p.rigbody2D.velocity = currentVelocity;

        }

        void OnTriggerExit2D(Collider2D coll)
        {
            GravitySource source = coll.gameObject.GetComponent<GravitySource>();
            if (source != null)
            {
                if (source != null)
                {
                    source.OnGravityPulse -= OnGravitationalPulseForceApplied;
                }
            }
        }
    }
}
