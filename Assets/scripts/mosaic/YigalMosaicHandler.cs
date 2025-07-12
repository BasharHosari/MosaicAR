using UnityEngine;
using Vuforia;
using TMPro;
using System.Collections;

public class YigalMosaicHandler : MonoBehaviour
{
    public GameObject yigalPrefab;
    public TextMeshProUGUI statusText;
    public GameObject skipPanel;
    public TextMeshProUGUI subtitlesText;

    private bool hasTriggered = false;

    void Start()
    {
        var observerEventHandler = GetComponent<DefaultObserverEventHandler>();
        if (observerEventHandler != null)
        {
            observerEventHandler.OnTargetFound.AddListener(OnTargetFound);
        }
    }

    void OnTargetFound()
    {
        if (hasTriggered) return;
        hasTriggered = true;

        Debug.Log("Mosaic detected");

        if (statusText != null)
        {
            statusText.text = "Mosaic located";
        }

        StartCoroutine(DelayAndSpawnYigal(3f));
    }

    private IEnumerator DelayAndSpawnYigal(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (statusText != null)
            statusText.gameObject.SetActive(false);

        skipPanel.SetActive(true);

        // Get the current camera position to calculate facing direction
        Vector3 cameraPosition = Camera.main.transform.position;

        Vector3 mosaicPosition = transform.position;

        Vector3 lateralOffset = transform.right * 0.25f;     // still to the side
        Vector3 backwardOffset = -transform.forward * 0.25f; // behind the mosaic
        Vector3 verticalOffset = Vector3.up * 0.05f;

        Vector3 yigalPosition = mosaicPosition + lateralOffset + backwardOffset + verticalOffset;



        // Make Yigal look at the camera, only on horizontal plane
        Vector3 lookDirection = cameraPosition - yigalPosition;
        lookDirection.y = 0;
        Quaternion yigalRotation = Quaternion.LookRotation(lookDirection);

        //storing incomplete mosaic size
        Vector3 mosaicSize = Vector3.one;

        var imageTarget = GetComponent<ImageTargetBehaviour>();
        if (imageTarget != null)
        {
            Vector2 size2D = imageTarget.GetSize(); // Width & Height in meters
            mosaicSize = new Vector3(size2D.x, 1f, size2D.y);
        }

        // Instantiate and configure Yigal
        GameObject yigal = Instantiate(yigalPrefab, yigalPosition, yigalRotation);
        yigal.transform.localScale = new Vector3(0.25f, 0.2f, 0.25f);


        Debug.Log("Yigal spawned at: " + yigalPosition);

        // Start speech
        var speech = yigal.GetComponent<YigalSpeech>();
        if (speech != null)
        {
            speech.setSubTitleComp(subtitlesText);
            speech.mosaicTransform = transform;
            speech.mosaicSize = mosaicSize;
            speech.StartYigalSpeech();
        }
    }

}
