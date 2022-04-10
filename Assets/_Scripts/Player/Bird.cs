using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isLaunched;

    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        if (!isLaunched) return;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
