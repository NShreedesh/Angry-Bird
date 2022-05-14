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
    [SerializeField] private float minDragDistance;

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
        GameManager.OnVictoryState += RestLaunchOnVictory;

        //_trajectoryPoints = new GameObject[howManyTrajectoryPoints];
        //trajectoryPointsParent.gameObject.SetActive(false);
        //for (int i = 0; i < howManyTrajectoryPoints; i++)
        //{
        //    _trajectoryPoints[i] = Instantiate(trajectoryPoint, transform.position, Quaternion.identity, trajectoryPointsParent);
        //}
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
            if (controller.previousBirdAbility != null && 
                !controller.previousBirdAbility.canLaunchNextBird) return;

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
        }

        else if (controller.inputControls.leftMouseInput <= 0 && _canLaunch)
        {
            float dragDistance = (_dragEndPosition - _dragStartPosition).magnitude;

            ResetStrips();
            ResetValues();

            if (dragDistance <= minDragDistance)
            {
                controller.bird.transform.position = controller.birdLaunchTransform.position;
                controller.bird = null;
                controller.rb = null;
                return;
            }

            controller.rb.isKinematic = false;
            controller.rb.velocity = _dragForce;
            controller.bird.canBeLaunched = false;
            controller.bird.isLaunched = true;
            controller.bird.transform.parent = null;
            if(controller.bird.TryGetComponent<BirdAbility>(out BirdAbility birdAbility))
            {
                controller.previousBirdAbility = birdAbility;
            }

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
        _canLaunch = false;
    }

    private void MakeNextBirdReady()
    {
        controller.birds.RemoveAt(0);
        controller.MakeBirdReady();
    }

    private void OnDisable()
    {
        CancelInvoke();

        GameManager.OnVictoryState -= RestLaunchOnVictory;
    }

    private void RestLaunchOnVictory()
    {
        if (controller.bird.isLaunched) return;

        controller.bird.transform.position = controller.birdLaunchTransform.position;
        ResetStrips();
        ResetValues();
    }
}