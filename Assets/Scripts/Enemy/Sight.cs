using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{

    public float distance_;
    public LayerMask sensor_layer_;
    public LayerMask obstacles_layer_;

    public Collider detected_object_;

    // Start is called before the first frame update
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance_, sensor_layer_);

        detected_object_ = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider single_collider = colliders[i];

            Vector3 dir_to_collider = Vector3.Normalize(single_collider.bounds.center - transform.position);


            if(!Physics.Linecast(transform.position, single_collider.bounds.center, out RaycastHit hit, obstacles_layer_))
            {
                Debug.DrawLine(transform.position, single_collider.bounds.center, Color.green);
                detected_object_ = single_collider;
                break;
            }
            else
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }

        }

    }

    private void OnDrawGizmos()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, distance_);
    }
}
