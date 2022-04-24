using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobController : MonoBehaviour, IPullable
{
    public Material pullingMaterial;

    private Material _defaultMaterial;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPullable.PullStart(PlayerController player)
    {
        _renderer.sharedMaterial = pullingMaterial;
    }

    void IPullable.PullEnd(PlayerController player)
    {
        _renderer.sharedMaterial = _defaultMaterial;
    }
}
