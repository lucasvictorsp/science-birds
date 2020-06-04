using UnityEngine;
using System.Collections;

public class ABBirdBlack : ABBird {

	public float _explosionArea = 1f;
	public float _explosionPower = 1f;
	public float _explosionDamage = 1f;

	void SpecialAttack() {
        //this.abilityAtiva = true;
        Explode ();
	}

	// Called via frame event
	void Explode() {
		//Debug.Log("BLACK-Explode-0 = " +  Time.time);
        ABTNT.Explode (transform.position, _explosionArea, _explosionPower, _explosionDamage, gameObject);
        //Debug.Log("BLACK-Explode-1 = " +  Time.time);
		Die (true);
		//Debug.Log("BLACK-Explode-2");
        //DieNotDestroy();
	}

	public override void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("BLACK-OnCollisionEnter2D-0 = " + Time.time);
        _trailParticles._shootParticles = false;

		if(OutOfSlingShot && IsFlying){
			this.IsFlying = false;
			//Debug.Log("BLACK-OnCollisionEnter2D-1");
			Invoke("Explode", 0.59f);
            //_animator.Play ("explode");

            IsDying = true;
        }
        //Debug.Log("BLACK-OnCollisionEnter2D-2");
	}
}
