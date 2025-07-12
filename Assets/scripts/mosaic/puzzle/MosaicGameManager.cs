using UnityEngine;
using TMPro;

public class MosaicGameManager : MonoBehaviour
{
    public static MosaicGameManager Instance;

    public GameObject victoryMessage;
    public GameObject completeMosaic;  

    private int piecesPlaced = 0;

    void Awake()
    {
        Instance = this;
        victoryMessage.SetActive(false);
    }

    public void PiecePlaced()
    {
        piecesPlaced++;
        if (piecesPlaced == 4)
        {
            OnPuzzleComplete();
        }
    }

    private void OnPuzzleComplete()
    {
        victoryMessage?.SetActive(true);

        if (completeMosaic != null)
        {
            completeMosaic.SetActive(true);
            Animator animator = completeMosaic.GetComponent<Animator>();
            if (animator != null)
                animator.Play("MosaicZoom", 0, 0f);
            else
                Debug.LogWarning("Animator not found on completeMosaic.");
        }
        else
        {
            Debug.LogWarning("completeMosaic is not assigned.");
        }

        Debug.Log("Puzzle Solved!");
    }



}
