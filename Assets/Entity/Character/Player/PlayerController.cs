using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask knobMask = 0;
    public float pullForce = 60;
    public float pullEndRange = 1.5f;
    public float pullEndDrag = 20;
    public float pullEndSpeed = 0.5f;
    public float pullStartDragTime = 1;
    public float pullStartDrag = 5;

    private Rigidbody2D _rigidbody;
    private GameObject _pulling;
    private IPullable _pullable;
    private float _defaultDrag;
    private float _pullStartTimer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _defaultDrag = _rigidbody.drag;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _PullEnd();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 999, knobMask);
            if (hit.collider)
            {
                _PullStart(hit.collider.gameObject);
                _pullStartTimer = pullStartDragTime;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (_pulling != null)
            {
                Vector2 diff = _pulling.transform.position - transform.position;
                if (diff.sqrMagnitude < pullEndRange * pullEndRange)
                {
                    _rigidbody.drag = pullEndDrag;
                    _rigidbody.AddForce(pullEndSpeed * pullForce * Time.deltaTime * diff.normalized);
                }
                else if (_pullStartTimer > 0)
                {
                    _rigidbody.drag = pullStartDrag;
                    _rigidbody.AddForce(pullForce * Time.deltaTime * diff.normalized);
                }
                else
                {
                    _rigidbody.drag = _defaultDrag;
                    _rigidbody.AddForce(pullForce * Time.deltaTime * diff.normalized);
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            _PullEnd();
        }

        _pullStartTimer -= Time.deltaTime;
    }

    private void _PullStart(GameObject pulling)
    {
        _pullable = null;
        if (pulling != null)
        {
            _pullable = pulling.GetComponent<IPullable>();
            if (_pullable != null)
            {
                _pullable.PullStart(this);
            }
        }
        _pulling = pulling;
    }

    private void _PullEnd()
    {
        if (_pullable != null)
        {
            _pullable.PullEnd(this);
        }
        _pulling = null;
        _pullable = null;
    }
}
