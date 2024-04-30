using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed;
    private Rigidbody rb;
    private int fps;

    [SerializeField] private TextMeshProUGUI _fpsText;
    private float _hudRefreshRate = 1f;
    private float _timer = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "fps: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
    void FixedUpdate()
    {
        Vector2 moveAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        rb.velocity =  new Vector3(moveAxis.x, 0, moveAxis.y) * Time.deltaTime * PlayerSpeed;


        float rotateY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        transform.Rotate(transform.up, rotateY);
    }
}
