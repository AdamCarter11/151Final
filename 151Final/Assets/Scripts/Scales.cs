using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was copied from a video to be used a reference
public class Scales : AudioEvents {

    public Vector3 beatScale;
	public Vector3 restScale;
    [SerializeField] GameObject objectToSpawn, spawnPoint, targetPos;

	private IEnumerator MoveToScale(Vector3 _target)
	{
		Vector3 _curr = transform.localScale;
		Vector3 _initial = _curr;
		float _timer = 0;

		while (_curr != _target)
		{
			_curr = Vector3.Lerp(_initial, _target, _timer / beatTime);
			_timer += Time.deltaTime;

			transform.localScale = _curr;

			yield return null;
		}

		beat = false;
	}

	public override void UpdateEffects()
	{
		base.UpdateEffects();

		if (beat) return;

		transform.localScale = Vector3.Lerp(transform.localScale, restScale, smoothTime * Time.deltaTime);
	}

	public override void OnBeat()
	{
		base.OnBeat();
        if(BossScript.dist < BossScript.activeDist){

            //calculates bullet angle
            Vector2 dirToTarget =  targetPos.transform.position - spawnPoint.transform.position;
            print(dirToTarget);
            float angle = Vector3.Angle(Vector3.right, dirToTarget);
            if(targetPos.transform.position.y < spawnPoint.transform.position.y) { angle *= -1; }
            Quaternion bulletRot = Quaternion.AngleAxis(angle, Vector3.forward);

            //spawns bullet, adds force, then destroys after set time
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.transform.position, bulletRot);
            //spawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(-700,transform.position.y,0));
            //spawnedObject.GetComponent<Rigidbody2D>().AddForce(Vector3.forward * 1000);
            //spawnedObject.transform.Translate(transform.right * 10 * Time.deltaTime, Space.World);
            Destroy(spawnedObject,2f);
        }

        //Player.movePoints++;
        
		StopCoroutine("MoveToScale");
		StartCoroutine("MoveToScale", beatScale);
	}
}
