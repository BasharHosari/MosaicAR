using UnityEngine;
using UnityEngine.SceneManagement;


public class SpeechUIHelper : MonoBehaviour
{
    public void SkipSpeech()
    {
        if (YigalSpeech.Instance != null && YigalSpeech.Instance.gameObject.activeInHierarchy)
        {
            YigalSpeech.Instance.SkipToSecondClip();
        }
        else
        {
            Debug.LogWarning("YigalSpeech instance is null or inactive.");
        }
    }

    public void LoadPuzzleScene()
    {
        SceneManager.LoadScene("MosaicPuzzleScene");
    }
}
