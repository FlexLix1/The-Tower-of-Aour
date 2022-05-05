using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAndDeactivateFloors : MonoBehaviour
{
    public GameObject[] floors;
    public int currentfloor;
    int floorChecker;

    // Start is called before the first frame update
    void Start()
    {
        floorChecker = currentfloor;
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

    public void IncreaseCurrentFloor()
    {
        currentfloor++;
        floorChecker = currentfloor;
    }

    public void DecreaseCurrentFloor()
    {
        currentfloor--;

        if(currentfloor == floorChecker)
        {
            currentfloor++;
        }

    }
}
