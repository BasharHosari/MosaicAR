using UnityEngine;

public class HologramSpinner : MonoBehaviour
{
    public float rotationSpeed = 20f;
    private bool isHolding = false;

    void Update()
    {
        // Detect touch on mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                isHolding = true;
            }
            else
            {
                isHolding = false;
            }
        }
        else
        {
            isHolding = false;
        }


        if (!isHolding)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // For debug purposes
        Debug.Log("isHolding = " + isHolding);
    }
}
