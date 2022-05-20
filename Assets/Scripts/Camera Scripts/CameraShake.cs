using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace UnityCore
{
    namespace Audio
    {
        public class CameraShake : MonoBehaviour
        {
            public float delayBetweenCameraShake;
            public ParticleSystem bricks;
            public ParticleSystem dust;
            public ParticleSystem cogwheel;

            public float offsetFromPlayer;

            Vector3 followPlayer;
            GameObject player;

            CinemachineImpulseSource impulse;
            AudioController audioController;
            private bool shakeIsTrue;
            // Start is called before the first frame update
            void Start()
            {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                impulse = transform.GetComponent<CinemachineImpulseSource>();
                shakeIsTrue = true;
                player = GameObject.FindGameObjectWithTag("Player");
            }

            // Update is called once per frame
            void Update()
            {

                if (shakeIsTrue)
                {
                    followPlayer = player.transform.position + player.transform.up * offsetFromPlayer;
                    Invoke("shake", delayBetweenCameraShake);
                    shakeIsTrue = false;
                }
            }

            void shake()
            {
                bricks = Instantiate(bricks, followPlayer, transform.rotation);
                cogwheel = Instantiate(cogwheel, followPlayer, transform.rotation);
                dust = Instantiate(dust, followPlayer, transform.rotation);
                impulse.GenerateImpulse();
                shakeIsTrue = true;
                audioController.PlayAudio(AudioType.SFX_BellSound, true);
                activateParticles();
            }

            void activateParticles()
            {

                bricks.Play();
                cogwheel.Play();
                dust.Play();
                Invoke(nameof(stopParticles), 3);
            }

            void stopParticles()
            {
                bricks.Stop();
                dust.Stop();
                cogwheel.Stop();
            }

        }
    }
}