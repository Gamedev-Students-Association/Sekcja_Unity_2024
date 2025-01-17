using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public float followspeed = 2f;
	public float minY = 0;
	public Transform target;

	private void Start() {
		if (!target) {
			Debug.LogWarning("No target set for CameraFollow", this);
			enabled = false;
		}
	}

	// Update is called once per frame
	void Update() {
		Vector3 newPose = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minY, float.PositiveInfinity), -10f);
		transform.position = Vector3.Slerp(transform.position, newPose, followspeed * Time.deltaTime);
	}
}