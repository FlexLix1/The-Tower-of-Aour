using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public bool pressed;
    public float closeDelay = 5;
    public Vector3 pressedOffset = new Vector3(0, 0.02f, 0);
    public DynamicDoor doorScript;

    Vector3 pressedPosition;
    Transform plateTransform;

    [SerializeField]
    UnityEvent onStandingOnPressurePlate = null;

    [SerializeField]
    UnityEvent onResettingPressurePlate = null;

    void Start()
    {
        pressed = false;
        plateTransform = transform.GetChild(0).transform;
        pressedPosition = plateTransform.position - pressedOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            CancelInvoke(nameof(Unpressed));
            plateTransform.position = pressedPosition;
            if (!pressed)
            {
                onStandingOnPressurePlate.Invoke();
            }
            pressed = true;
            doorScript.openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);

            foreach (var item in hitColliders)
            {
                if (item.gameObject.CompareTag("Player") || item.gameObject.CompareTag("Box"))
                {
                    return;
                }
            }
            Invoke(nameof(Unpressed), closeDelay);
        }
    }
    private void Unpressed()
    {
        pressed = false;
        doorScript.openDoor = false;
        plateTransform.position = pressedPosition + pressedOffset * 2;
        onResettingPressurePlate.Invoke();
    }
}

