using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float offsetY;
    public float offsetX;
    public float angleOffset;
    Vector3 angleOffsetY;
    private void Start()
    {
        angleOffsetY = new Vector3(0, angleOffset, 0);
    }
    void Update()
    {
        //transform.position = target.position + target.up * offsetY + target.right * -offsetX;

        //transform.forward = target.transform.right;
        //transform.rotation = Quaternion.Euler(angleOffset, transform.rotation.y,transform.rotation.z);
        transform.LookAt(target.position);

        //Vector3 relativePos = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos + angleOffsetY, Vector3.forward);
        //transform.rotation = Quaternion.Euler(angleOffset, target.rotation.y,rotation.z);
    }
}
