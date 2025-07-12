using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingSceneHandlers : MonoBehaviour
{
    public void LoadScanScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
