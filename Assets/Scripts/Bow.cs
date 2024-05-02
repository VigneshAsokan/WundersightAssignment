using TMPro;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform ArrowAnchor;
    [SerializeField] public TextMeshProUGUI DebugText;
    [SerializeField] private Transform _stringMiddleBone;
    [SerializeField] private Transform _stringHolder;
    [SerializeField] private Rigidbody _stringRb;

    public Arrow CurrentAttachedArrow { get; private set; }
    private float _stringForceMultiplier = 0.2f;
    private float _defaultForce = 1500f;

    public OVRControllerHelper ControllerinBound;
    public bool StringPulled { get; private set; }
    private float _initZposition = 0;
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
            _initZposition = ControllerinBound.transform.position.z;
            DebugText.text = "String Pulled Down init" + _initZposition;
        }
        if(StringPulled)
        {
            if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, ControllerinBound.m_controller))
            {
                float difY = -0.5f - (ControllerinBound.transform.position.z - _initZposition);
                DebugText.text = "String Pulled Down DifY: " + difY;
                difY = Mathf.Clamp(difY, -0.5f, -0.1f);
                _stringMiddleBone.transform.localPosition = new Vector3(0f, difY, 0f);
                _stringForceMultiplier =  0.25f/difY;
            }
            else
            {
                DebugText.text = "String let go with force" + _stringForceMultiplier;
                _stringMiddleBone.localPosition = new Vector3(0, -0.5f, 0);
                CurrentAttachedArrow?.FireArrow(_stringForceMultiplier);
                CurrentAttachedArrow = null;
                ControllerinBound = null;
                StringPulled = false;
            }
        }
    }
}
