using UnityEngine;

public class HighlightableObject : MonoBehaviour
{
    public Material hoverMaterial;
    public Material selectedMaterial;
    private Material originalMaterial;

    [SerializeField] private Renderer objectRenderer;

    void Start()
    {
        originalMaterial = objectRenderer.material;
    }

    void OnMouseEnter()
    {
        objectRenderer.material = hoverMaterial;
    }

    void OnMouseExit()
    {
        objectRenderer.material = originalMaterial;
    }

    void OnMouseUp()
    {
        objectRenderer.material = selectedMaterial;
    }


}
