using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ButtonTrigger : MonoBehaviour
{
    Button button;
    bool isTouch = false;
    InputDevice RightControllerDevice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isTouch) return;
        if(other.CompareTag("Hand"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (3 < rb.linearVelocity.magnitude)
            {
                isTouch = true;
                button.onClick.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
            isTouch = false;
    }
}
