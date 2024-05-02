using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform ArrowAnchor;
    [SerializeField] private Transform _stringMiddleBone;
    [SerializeField] private Transform _pullDummyTransform;
    [SerializeField] private Transform _stringHolder;
    [SerializeField] private Rigidbody _stringRb;

    public Arrow CurrentAttachedArrow { get; private set; }
    private float _stringForceMultiplier = 0.2f;

    public OVRControllerHelper ControllerinBound;
    public bool StringPulled { get; private set; }
    private void Start()
    {
        _stringHolder.gameObject.SetActive(false);
    }
    public void ArrowAttached(Arrow arrow)
    {
        CurrentAttachedArrow = arrow;  
        _stringHolder.gameObject.SetActive(true);   
    }
    private void Update()
    {

        if (ControllerinBound != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, ControllerinBound.m_controller)) 
        {
            StringPulled = true;
            _pullDummyTransform.position = ControllerinBound.transform.position;
        }
        if(StringPulled)
        {
            if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, ControllerinBound.m_controller))
            {
                _pullDummyTransform.position = ControllerinBound.transform.position;
                float difY = _pullDummyTransform.localPosition.y;
                difY = Mathf.Clamp(difY, -0.5f, -0.1f);
                _stringMiddleBone.transform.localPosition = new Vector3(0f, difY, 0f);
                _stringForceMultiplier = 0.25f / difY;
            }
            else
            {
                _stringMiddleBone.localPosition = new Vector3(0, -0.5f, 0);
                _pullDummyTransform.localPosition = new Vector3(0, -0.5f, 0);
                CurrentAttachedArrow?.FireArrow(_stringForceMultiplier);
                CurrentAttachedArrow = null;
                ControllerinBound = null;
                StringPulled = false;
                _stringHolder.gameObject.SetActive(false);
            }
        }
    }
}
