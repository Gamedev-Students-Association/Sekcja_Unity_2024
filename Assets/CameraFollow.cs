using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
public float followspeed = 2f;
public float yoff = 1f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
     Vector3 newPose = new Vector3(target.position.x,target.position.y + yoff, -10f);
        transform.position = Vector3.Slerp(transform.position, newPose, followspeed * Time.deltaTime);
    }
}
