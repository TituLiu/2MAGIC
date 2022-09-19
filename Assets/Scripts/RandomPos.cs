using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] protected Transform pos1, pos2;
    Vector3 targetDir;
    float speed = 10;
    float dist = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeTargetDir();
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        if (Vector3.Distance(transform.position, targetDir) < dist)
        {
            ChangeTargetDir();
        }
    }
    public void ChangeTargetDir()
    {
        targetDir = new Vector3(Random.Range(pos1.position.x, pos2.position.x), transform.position.y, Random.Range(pos1.position.z, pos2.position.z));
        var newDir = targetDir - transform.position;
        transform.forward = newDir;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        ChangeTargetDir();
    }
}
