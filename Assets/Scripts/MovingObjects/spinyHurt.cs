using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore{
    namespace Audio
    {
        public class spinyHurt : MonoBehaviour
        {

            spinscript spining;
            HurtPlayer hurtPlayer;
            // Start is called before the first frame update
            void Start()
            {
                spining = GetComponent<spinscript>();
                hurtPlayer = GetComponent<HurtPlayer>();
            }

            // Update is called once per frame
            void Update()
            {
                if (!spining.timeFroze)
                {
                    FindObjectOfType<HealthManager>().HurtPlayer(1);
                }
            }
        }
    }
}
