using System.Collections;
using UnityEngine;
using TMPro;

public class YigalSpeech : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip clip1;
    public AudioClip clip2;

    private AudioSource audioSource;
    public static YigalSpeech Instance;
    private TextMeshProUGUI subtitlesText;
    public GameObject hologramPrefab;
    public Transform hologramSpawnPoint;

    [HideInInspector]
    public Transform mosaicTransform;
    [HideInInspector]
    public Vector3 mosaicSize;


    private string[] subtitles = new string[]
{
        "That is a part of the mosaic in Hoku.",
        "For those who don't know Hoku, which is right here, archeological excavations have been        going on for several years.",
        "In this specific mosaic, you see a section of Samson carrying the gates of Gaza.",
        "In fact, until the excavations in Hoku were finished and the large space was not secured, they actually brought some inside here that people can see.",
        "The archeologists who are excavating there say that these are the most impressive mosaics found to date in the country.",
        "More than an alphanumeric character, more than a bird",
        "Who are going to uncover mosaics that are the most beautiful ever found in the country.",
        "And are also the most comprehensive in terms of the stories of creation and the creation of man that have been found to date in the country.",
        "So this is actually a taste of the mosaic that connects to all the excavations that are happening."
};

    private float[] timings = new float[] { 0f, 1f, 8f, 12f, 19f, 25f, 30f, 35f, 42f };


    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void setSubTitleComp(TextMeshProUGUI comp) {
        this.subtitlesText = comp;
    }

    //used to initiate Yigal speech
    public void StartYigalSpeech()
    {
        if (clip1 == null || clip2 == null)
        {
            Debug.LogWarning("One or both audio clips are missing.");
            return;
        }

        StartCoroutine(PlayFirstClip());
    }

    private IEnumerator PlayFirstClip()
    {
        audioSource.clip = clip1;
        audioSource.Play();
        Debug.Log("Playing clip 1");

        StartCoroutine(PlaySubtitles());

        yield return new WaitForSeconds(clip1.length + 2f); // Wait clip1 + 2 sec pause

        StartCoroutine(PlaySecondClip());
    }

    private IEnumerator PlaySecondClip()
    {
        audioSource.clip = clip2;
        audioSource.Play();
        Debug.Log("Playing clip 2");

        subtitlesText.text = "Here you can see a visualization of the mosaic in its fully restored form.";

        yield return new WaitForSeconds(clip2.length);

        OnYigalSpeechDone();
    }

    private void OnYigalSpeechDone()
    {
        Debug.Log("Yigal finished talking. Ready for next action.");
        subtitlesText.gameObject.SetActive(false);

        if (hologramPrefab != null)
        {
            Vector3 spawnPos;
            if (hologramSpawnPoint != null)
                spawnPos = hologramSpawnPoint.position;
            else if (mosaicTransform != null)
                spawnPos = mosaicTransform.position + new Vector3(0, 0.25f, 0); // spawn above mosaic
            else
                spawnPos = transform.position + new Vector3(0, 0.25f, 0); // fallback
            Vector3 toCamera = Camera.main.transform.position - spawnPos;
            toCamera.y = 0;
            Quaternion rotation = Quaternion.LookRotation(toCamera.normalized);
            Instantiate(hologramPrefab, spawnPos, rotation);
        }
        else
        {
            Debug.LogWarning("Hologram prefab not assigned.");
        }

    }

    public void SkipToSecondClip()
    {
        Debug.Log("Skipping to second clip");
        StopAllCoroutines(); // Stop any ongoing audio
        StartCoroutine(PlaySecondClip());
    }

    private IEnumerator PlaySubtitles()
    {
        subtitlesText.gameObject.SetActive(true);
        for (int i = 0; i < timings.Length && i < subtitles.Length; i++)
        {
            yield return new WaitForSeconds(i == 0 ? timings[i] : timings[i] - timings[i - 1]);
            subtitlesText.text = subtitles[i];
        }

        yield return new WaitForSeconds(3f);
        subtitlesText.text = "";
    }

}
