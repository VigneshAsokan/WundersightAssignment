using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool IsGrabbed { get; private set; }
    public bool IsSnappedToBow { get; private set; }
    private Rigidbody _rb;
    private BoxCollider _collider;
    private OVRControllerHelper _controller;
    private float _force = 1000f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (_controller != null && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, _controller.m_controller) && IsGrabbed)
        {
            SetArrowGrabbed(false);
        }
    }
    public void FireArrow(float stringforce = -0.25f)
    {
        SetArrowGrabbed(false);
        _rb.AddForce(transform.up * stringforce * _force, ForceMode.Force);
    }
    public void SetArrowGrabbed(bool value, OVRControllerHelper controller = null)
    {
        gameObject.SetActive(true);
        IsGrabbed = value;
        _rb.isKinematic = IsGrabbed;
        _rb.useGravity = !IsGrabbed;
        _collider.isTrigger = IsGrabbed;
        _controller = controller;
        IsSnappedToBow = false;

        if ( controller != null )
        {
            transform.SetParent(_controller.transform, false);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        }
        else
        {
            transform.SetParent(null, true);
        }
    }

    public void SnapArrowToBow(Bow bow)
    {
        transform.SetParent(bow.ArrowAnchor, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        IsSnappedToBow = true;
        IsGrabbed = false;
        bow.ArrowAttached(this);
    }
}
