using UnityEngine;

public class HologramReveal : MonoBehaviour
{
    [SerializeField] private float revealDuration = 2f;

    private MaterialPropertyBlock propertyBlock;
    private Renderer hologramRenderer;
    private float currentThreshold = 0f;
    private float revealSpeed;
    private bool revealing = true;

    void Start()
    {
        hologramRenderer = GetComponent<Renderer>();
        if (hologramRenderer == null)
        {
            Debug.LogWarning("No Renderer found on this object.");
            return;
        }

        propertyBlock = new MaterialPropertyBlock();
        revealSpeed = 1f / revealDuration;

        // Initialize dissolve
        propertyBlock.SetFloat("_DissolveThreshold", 0f);
        hologramRenderer.SetPropertyBlock(propertyBlock);
    }

    void Update()
    {
        if (!revealing || hologramRenderer == null)
            return;

        currentThreshold += Time.deltaTime * revealSpeed;

        if (currentThreshold >= 1f)
        {
            currentThreshold = 1f;
            revealing = false;
        }

        propertyBlock.SetFloat("_DissolveThreshold", currentThreshold);
        hologramRenderer.SetPropertyBlock(propertyBlock);
    }
}
