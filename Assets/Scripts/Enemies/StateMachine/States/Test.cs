using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed;
    public GameObject target;
    private Rigidbody rb;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Vector3 velocity = dir * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        
        time += Time.fixedDeltaTime;
        if (time >= 3f) GetComponent<Animator>().SetBool("Chasing", true);
        
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
