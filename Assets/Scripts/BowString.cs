using UnityEngine;

public class BowString : MonoBehaviour
{
    private Bow _bow;

    private void Awake()
    {
        _bow = GetComponentInParent<Bow>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Controller"))
        {
            Debug.Log("Controller Hit");
            if (collider.GetComponentInChildren<Arrow>() != null)
            {
                collider.GetComponentInChildren<Arrow>().SnapArrowToBow(_bow);
                _bow.ControllerinBound = collider.GetComponent<OVRControllerHelper>();
            }
            else if(_bow.ControllerinBound == null)
            {
                _bow.ControllerinBound = collider.GetComponent<OVRControllerHelper>();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Controller") && !_bow.StringPulled)
        {
            _bow.ControllerinBound = null;
        }
    }
}
