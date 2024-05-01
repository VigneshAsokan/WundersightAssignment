using TMPro;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform ArrowAnchor;
    [SerializeField]private TextMeshProUGUI _debugText;
    [SerializeField]private Transform _stringMiddleBone;
    [SerializeField]private Transform _stringHolder;

    private Arrow _currentAttachedArrow;
    private float stringForce;
    private OVRControllerHelper _controllerinBound;
    private float _minZ = -0.5f;
    private float _maxZ = 0f;

    public void ArrowAttached(Arrow arrow)
    {
        _currentAttachedArrow = arrow;  
        _stringHolder.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (_controllerinBound != null && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, _controllerinBound.m_controller)) 
        {
            _debugText.text = "Controller Grabbing String!!";
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Controller"))
        {
            _debugText.text = "Controller Touching String!!";
            if (collider.GetComponentInChildren<Arrow>() != null)
            {
                collider.GetComponentInChildren<Arrow>().SnapArrowToBow(this);
            }
            else if(_currentAttachedArrow != null)
            {
                _controllerinBound = collider.GetComponent<OVRControllerHelper>();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Controller"))
        {
            _controllerinBound = null;
        }
    }
}
