using UnityEngine;

public class LaunchBird : MonoBehaviour
{
    [Header("Controller Info")]
    [SerializeField] private PlayerController controller;

    [Header("Mouse Input Info")]
    [SerializeField] private Camera cam;
    private Vector2 _mousePosition;

    [Header("Bird Launch Info")]
    [SerializeField] private float force;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float birdPositionOffset;
    private Bird _bird;
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

    private void Start()
    {
        ResetStrips();
        _trajectoryPoints = new GameObject[howManyTrajectoryPoints];

        for(int i = 0; i < howManyTrajectoryPoints; i++)
        {
            _trajectoryPoints[i] = Instantiate(trajectoryPoint, transform.position, Quaternion.identity, trajectoryPointsParent);
        }
    }

    private void Update()
    {
        Launch();
    }

    private void Launch()
    {
        // TODO: Launch only on certain distance...

        if (controller.inputControls.isLeftMousePressed)
        {
            _mousePosition = cam.ScreenToWorldPoint(controller.inputControls.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(_mousePosition, Vector2.zero, playerLayer);

            if (hitInfo)
            {
                hitInfo.transform.TryGetComponent<Bird>(out _bird);
                if (_bird.isLaunched) return;
                _dragStartPosition = hitInfo.transform.position;
                _canLaunch = true;
            }
        }

        if (controller.inputControls.leftMouseInput > 0 && _canLaunch)
        {
            _mousePosition = cam.ScreenToWorldPoint(controller.inputControls.mousePosition);
            _dragEndPosition = _mousePosition;
            _dragForce = (_dragEndPosition - _dragStartPosition) * -force;
            SetStrips(_mousePosition);

            for (int i = 0; i < howManyTrajectoryPoints; i++)
            {
                _trajectoryPoints[i].transform.position = CalculateTrajectoryPosition(i * 0.05f);
            }
        }

        else if (controller.inputControls.leftMouseInput <= 0 && _canLaunch)
        {
            controller.rb.isKinematic = false;
            controller.rb.velocity = _dragForce;
            _bird.isLaunched = true;
            _canLaunch = false;
        }
    }

    private void SetStrips(Vector2 position)
    {
        Vector2 dir = position - _dragStartPosition;
        Vector2 birdTransformPos = position + dir.normalized * birdPositionOffset;

        if(_bird != null)
        {
            _bird.transform.position = birdTransformPos;
            _bird.transform.right = -dir.normalized;
        }

        frontStripLineRenderer.SetPosition(1, position);
        backStripLineRenderer.SetPosition(1, position);
    }

    private void ResetStrips()
    {
        frontStripLineRenderer.SetPosition(0, frontStripLineRenderer.transform.position);
        frontStripLineRenderer.SetPosition(1, frontStripLineRenderer.transform.position);
        backStripLineRenderer.SetPosition(0, backStripLineRenderer.transform.position);
        backStripLineRenderer.SetPosition(1, backStripLineRenderer.transform.position);
    }

    private Vector2 CalculateTrajectoryPosition(float time)
    {
        return (Vector2)controller.bird.transform.position + (_dragForce * time) + (0.5f * Physics2D.gravity * time * time);
    }
}