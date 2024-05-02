using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerSpeed;
    private Rigidbody _rb;
    private int _fps;
    private int _score;

    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private OVRCameraRig _cameraRig;
    private float _hudRefreshRate = 1f;
    private float _timer = 0;

    public delegate void ScoreUpdateAction(int value);
    public ScoreUpdateAction OnScoreUpdated;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        OnScoreUpdated = UpdateScore;
    }

    private void UpdateScore(int value)
    {
        _score += value;
        _scoreText.text = "Score: " + _score.ToString();
    }
    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            _fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "fps: " + _fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
    void FixedUpdate()
    {
        Vector2 moveAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        _rb.velocity = (transform.right * moveAxis.x + transform.forward * moveAxis.y) * Time.deltaTime * PlayerSpeed;


        float rotateY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        transform.Rotate(transform.up, rotateY);
    }
}
