using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class CompositeShadowCasterFromCollider : MonoBehaviour
{
    CompositeCollider2D collider;
    CompositeShadowCaster2D caster;
    List<ShadowCaster2D> casters;

    // Start is called before the first frame update
    void Start()
    {
        int count = collider.pathCount;
        //casters[0].
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
