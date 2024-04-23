using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour
{
    [SerializeField] private LayerMask npcLayer;
    public IInteractable _interact;
    [SerializeField] private GameObject _keyGuideCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (npcLayer.value == (npcLayer.value | 1 << collision.gameObject.layer))
        {
            _interact = collision.GetComponent<IInteractable>();
            _keyGuideCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (npcLayer.value == (npcLayer.value | 1 << collision.gameObject.layer))
        {
            _interact = null;
            _keyGuideCanvas.SetActive(false);
        }
    }
}
