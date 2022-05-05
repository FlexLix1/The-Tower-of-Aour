using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAndDeactivateFloors : MonoBehaviour
{
    public GameObject[] floors;
    public int currentfloor;

    // Start is called before the first frame update
    void Start()
    {
        currentfloor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentFloor();
    }

    private void CheckCurrentFloor()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            if (i == currentfloor)
            {
                floors[i].SetActive(true);

                if (i == 0)
                    return;

                floors[i - 1].SetActive(true);
            }
            else
            {
                floors[i].SetActive(false);
            }
        }
    }
}
