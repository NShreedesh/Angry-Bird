using UnityEngine;

public class LaunchBird : MonoBehaviour
{
    [Header("Controller Info")]
    [SerializeField] private PlayerController controller;

    [Header("Mouse Input Info")]
    private Camera _cam;
    private Vector2 _mousePosition;

    [Header("Bird Launch Info")]
    [SerializeField] private float force = 5.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float birdPositionOffset;
    [SerializeField] private float maxLength = 2;
    [SerializeField] private float maxBottomLength = 2;
    private bool _canLaunch;
    private Vector2 _dragStartPosition;
    private Vector2 _dragEndPosition;
    private Vector2 _dragForce;

    [Header("Catapult Stips Info")]
    [SerializeField] private LineRenderer frontStripLineRenderer;
    [SerializeField] private LineRenderer backStripLineRenderer;

    [Header("Trajectory Info")]
    [SerializeField] private GameObject trajectoryPoint;
    private GameObject[] _trajectoryPoints;
    [SerializeField] private int howManyTrajectoryPoints;
    [SerializeField] private Transform trajectoryPointsParent;

    [Header("Bird Ready Info")]
    [SerializeField] private int makeNextBirdReadyAfter = 1;

    private void Start()
    {
        _cam = Camera.main;

        ResetStrips();
        _trajectoryPoints = new GameObject[howManyTrajectoryPoints];

        trajectoryPointsParent.gameObject.SetActive(false);

        for (int i = 0; i < howManyTrajectoryPoints; i++)
        {
            _trajectoryPoints[i] = Instantiate(trajectoryPoint, transform.position, Quaternion.identity, trajectoryPointsParent);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Play) return;

        Launch();
    }

    private void Launch()
    {
        if (controller.inputControls.isLeftMousePressed)
        {
            _mousePosition = _cam.ScreenToWorldPoint(controller.inputControls.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(_mousePosition, Vector2.zero, playerLayer);

            if (hitInfo)
            {
                hitInfo.transform.TryGetComponent<Bird>(out controller.bird);
                if (controller.bird is null) return;
                controller.rb = controller.bird.GetComponent<Rigidbody2D>();
                if (!controller.bird.canBeLaunched) return;
                if (controller.bird.isLaunched) return;
                _dragStartPosition = hitInfo.transform.position;
                _canLaunch = true;
            }
        }

        if (controller.inputControls.leftMouseInput > 0 && _canLaunch)
        {
            _mousePosition = _cam.ScreenToWorldPoint(controller.inputControls.mousePosition);
            _dragEndPosition = _dragStartPosition + Vector2.ClampMagnitude(_mousePosition - _dragStartPosition, maxLength);
            _dragEndPosition.y = Mathf.Clamp(_dragEndPosition.y, maxBottomLength, 1000);

            _dragForce = (_dragEndPosition - _dragStartPosition) * -force;

            SetStrips(_dragEndPosition);

            for (int i = 0; i < howManyTrajectoryPoints; i++)
            {
                _trajectoryPoints[i].transform.position = CalculateTrajectoryPosition(i * 0.05f);
            }

        }

        else if (controller.inputControls.leftMouseInput <= 0 && _canLaunch)
        {
            controller.rb.isKinematic = false;
            controller.rb.velocity = Vector2.zero;
            controller.rb.velocity = _dragForce;
            controller.bird.canBeLaunched = false;
            controller.bird.isLaunched = true;

            ResetStrips();
            ResetValues();

            Invoke(nameof(MakeNextBirdReady), makeNextBirdReadyAfter);
        }
    }

    private void SetStrips(Vector2 position)
    {
        Vector2 dir = position - _dragStartPosition;
        Vector2 birdTransformPos = position + dir.normalized * birdPositionOffset;

        if (controller.bird != null)
        {
            controller.bird.transform.position = birdTransformPos;
        }

        frontStripLineRenderer.SetPosition(1, position);
        backStripLineRenderer.SetPosition(1, position);

        trajectoryPointsParent.gameObject.SetActive(true);
    }

    private void ResetStrips()
    {
        frontStripLineRenderer.SetPosition(0, frontStripLineRenderer.transform.position);
        frontStripLineRenderer.SetPosition(1, frontStripLineRenderer.transform.position);
        backStripLineRenderer.SetPosition(0, backStripLineRenderer.transform.position);
        backStripLineRenderer.SetPosition(1, backStripLineRenderer.transform.position);
    }

    private void ResetValues()
    {
        _dragStartPosition = Vector2.zero;
        _dragEndPosition = Vector2.zero;
        _dragForce = Vector2.zero;
        _canLaunch = false;
    }

    private Vector2 CalculateTrajectoryPosition(float time)
    {
        return (Vector2)controller.bird.transform.position + (_dragForce * time) + (0.5f * time * time * Physics2D.gravity);
    }

    private void MakeNextBirdReady()
    {
        controller.birds.RemoveAt(0);
        controller.MakeBirdReady();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}