using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirtSplatterEff : MonoBehaviour {
    public ParticleSystem _dirtParts;

   

    public Transform _lineCast;

    private RaycastHit2D _hit;

    void Update()
    {
        _hit = Physics2D.Linecast(_lineCast.position, _lineCast.position + new Vector3(0, -10, 0));
        if (_hit.collider != null && !_hit.collider.isTrigger)
        {
            _dirtParts.Play();
        }
        // else _dirtParts.Stop();
     
    }

}
