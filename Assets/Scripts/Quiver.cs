using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private int _arrowCount = 10;
    [SerializeField] private TextMeshProUGUI _debugText;
    private bool _isControllerInside = false;
    private bool _arrowGrabbed = false;
    [SerializeField] private OVRControllerHelper _controller;
    [SerializeField] private List<Arrow> _arrowsPool = new List<Arrow>();
    private int _arrowPoolIdx = 0;
    private void Start()
    {
        for (int i = 0; i < _arrowCount; i++)
        {
            Arrow arrow = Instantiate(_arrowPrefab, transform);
            arrow.transform.localPosition = Vector3.zero;
            arrow.gameObject.SetActive(false);
            _arrowsPool.Add(arrow);
        }
    }
    private void Update()
    {
        if(_isControllerInside && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, _controller.m_controller) && !_arrowGrabbed)
        {
            GrabNextArrow();
        }
        if(_controller != null && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, _controller.m_controller) && _arrowGrabbed)
        {
            _arrowGrabbed = false;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            GrabNextArrow();
        }
    }
    private void GrabNextArrow()
    {
        _debugText.text = "arrow Grabbed!!";
        Arrow arrow = _arrowsPool[_arrowPoolIdx];
        arrow.SetArrowGrabbed(true, _controller);
        _arrowPoolIdx++;
        if (_arrowPoolIdx >= _arrowCount)
        {
            _arrowPoolIdx = 0;
        }
        _arrowGrabbed = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Controller"))
        {
            Debug.Log("Controller Inside");
            _controller = collider.GetComponent<OVRControllerHelper>();
            _isControllerInside = true;
            _arrowGrabbed = false;
            _debugText.text = "Controller inside Trigger!!";
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Controller"))
        {
            Debug.Log("Controller Inside");
            _controller = null;
            _isControllerInside = false;
            _debugText.text = "Controller outside Trigger!!";
        }
    }
}
