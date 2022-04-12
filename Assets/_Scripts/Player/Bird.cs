using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isLaunched;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
}
