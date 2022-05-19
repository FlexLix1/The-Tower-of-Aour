using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace UnityCore{
    namespace Audio
    {
        public class CameraShake : MonoBehaviour
        {
            public float delayBetweenCameraShake;
            CinemachineImpulseSource impulse;
            AudioController audioController;
            private bool shakeIsTrue;
            // Start is called before the first frame update
            void Start()
            {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                impulse = transform.GetComponent<CinemachineImpulseSource>();

                shakeIsTrue = true;
            }

            // Update is called once per frame
            void Update()
            {
                if (shakeIsTrue)
                {
                    Invoke("shake", delayBetweenCameraShake);
                    shakeIsTrue = false;
                }
            }

            void shake()
            {
                impulse.GenerateImpulse();
                shakeIsTrue = true;
                audioController.PlayAudio(AudioType.SFX_BellSound, true);
            }
        }
    }
}
  