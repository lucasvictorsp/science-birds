// SCIENCE BIRDS: A clone version of the Angry Birds game used for 
// research purposes
// 
// Copyright (C) 2016 - Lucas N. Ferreira - lucasnfe@gmail.com
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
//

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ABBird : ABCharacter {
	//private float m_angle;  // Angulo atual de lançamento

    public string typeBird { get; set; }
    /*public string typeBird {
        get {
            return this.typeBird;
        }
        set {
            this.typeBird = value;
        }
    }//*/

	public float _dragSpeed     = 1.0f;
    public float _dragRadius    = 1.0f;
    public float _launchGravity = 1.0f;

	public float _woodDamage  = 1.0f;
	public float _stoneDamage = 1.0f;
	public float _iceDamage   = 1.0f;

    public float _jumpForce   = 1.0f;
	public float _launchForce = 1.0f;

	public float _jumpTimer;
    public float _maxTimeToJump;

	public bool IsSelected      { get; set; }
	public bool IsFlying        { get; set; }
	public bool OutOfSlingShot  { get; set; }
	public bool JumpToSlingshot { get; set; }

	protected ABParticleSystem _trailParticles;

	protected override void Start () {
		base.Start ();

		if (!ABGameWorld.Instance._isSimulation) {
        	float nextJumpDelay = Random.Range(0.0f, _maxTimeToJump);
        	Invoke("IdleJump", nextJumpDelay + 1.0f);
    	}

		_trailParticles = gameObject.AddComponent<ABParticleSystem> ();
		_trailParticles._particleSprites = ABWorldAssets.TRAIL_PARTICLES;
		_trailParticles._shootingRate = 0.1f;
    }

    void IdleJump() {
		if (IsFlying || OutOfSlingShot)
			return;

        if(IsIdle() && _rigidBody.gravityScale > 0f) {

			_rigidBody.AddForce(Vector2.up * _jumpForce);

			if (Random.value < 0.5f) {

				int randomSfx = Random.Range ((int)OBJECTS_SFX.MISC1, (int)OBJECTS_SFX.MISC2 + 1);
				_audioSource.PlayOneShot (_clips [randomSfx]);
			}
		}

        float nextJumpDelay = Random.Range(0.0f, _maxTimeToJump);
        Invoke("IdleJump", nextJumpDelay + 1.0f);
    }

	private void CheckVelocityToDie() {
		if (_rigidBody.velocity.magnitude < 0.001f) {
			CancelInvoke ();
			Die ();
		}
	}

	// Used to move the camera towards the blocks only when bird is thrown to frontwards
	public bool IsInFrontOfSlingshot() {
		float slingXPos = ABGameWorld.Instance.Slingshot ().transform.position.x - ABConstants.SLING_SELECT_POS.x;
		return transform.position.x + _collider.bounds.size.x > slingXPos + _dragRadius * 2f;
	}

	public override void Die(bool withEffect = true) {
		ABGameWorld.Instance.KillBird (this);
		base.Die (withEffect);
	}

    public override void OnCollisionEnter2D(Collision2D collision) {
		if(OutOfSlingShot && !IsDying) {
			IsFlying = false;
			_trailParticles._shootParticles = false;

			ABGameWorld.Instance.RemoveLastTrajectoryParticle ();

			foreach (ABParticle part in _trailParticles.GetUsedParticles())
				ABGameWorld.Instance.AddTrajectoryParticle (part);

			InvokeRepeating("CheckVelocityToDie", 3f, 1f);
			_animator.Play("die", 0, 0f);

			IsDying = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
		// Bird got dragged
        if(collider.tag == "Slingshot")
        {
            if(JumpToSlingshot)
				ABGameWorld.Instance.SetSlingshotBaseActive(false);

			if(IsSelected && IsFlying)
				_audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.DRAGED]);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Slingshot")
        {
            if(JumpToSlingshot)
				ABGameWorld.Instance.SetSlingshotBaseActive(false);

            if(IsFlying)
            {
				OutOfSlingShot = true;

				Vector3 slingBasePos = ABGameWorld.Instance.Slingshot().transform.position - ABConstants.SLING_SELECT_POS;
                slingBasePos.z = transform.position.z + 0.5f;

				ABGameWorld.Instance.ChangeSlingshotBasePosition(slingBasePos);
				ABGameWorld.Instance.ChangeSlingshotBaseRotation (Quaternion.identity);
            }
        }
    }

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Slingshot")
		{
			if(IsSelected && !IsFlying)
				_audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.DRAGED]);

			if(!IsSelected && IsFlying)
				_audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.FLYING]);
		}
	}
	
    public void SelectBird()
    {
		if (IsFlying || IsDying)
			return;
		
		IsSelected = true;

		_audioSource.PlayOneShot (_clips[(int)OBJECTS_SFX.MISC1]);
        _animator.Play("selected", 0, 0f);

		ABGameWorld.Instance.SetSlingshotBaseActive(true);
    }

    public void SetBirdOnSlingshot()
    {
		Vector3 slingshotPos = ABGameWorld.Instance.Slingshot ().transform.position - ABConstants.SLING_SELECT_POS;
		transform.position = Vector3.MoveTowards(transform.position, slingshotPos, _dragSpeed * Time.deltaTime);

		if(Vector3.Distance(transform.position, slingshotPos) <= 0f)
		{
			JumpToSlingshot = false;
			OutOfSlingShot = false;
			_rigidBody.velocity = Vector2.zero;
		}
    }

	public void DragBird(Vector3 dragPosition)
	{		
		if (float.IsNaN(dragPosition.x) || float.IsNaN(dragPosition.y))
			return;
			
		dragPosition.z = transform.position.z;
		Vector3 slingshotPos = ABGameWorld.Instance.Slingshot ().transform.position - ABConstants.SLING_SELECT_POS;
		float deltaPosFromSlingshot = Vector2.Distance(dragPosition, slingshotPos);

        // Lock bird movement inside a circle
        if(deltaPosFromSlingshot > _dragRadius)
			dragPosition = (dragPosition - slingshotPos).normalized * _dragRadius + slingshotPos;

		transform.position = Vector3.Lerp(transform.position, dragPosition, _dragSpeed * Time.deltaTime);
		
		// Slingshot base look to slingshot
		Vector3 dist = ABGameWorld.Instance.DragDistance();
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
		ABGameWorld.Instance.ChangeSlingshotBaseRotation(Quaternion.AngleAxis(angle, Vector3.forward));

        // Slingshot base rotate around the selected point
		Collider2D col = _collider;
		ABGameWorld.Instance.ChangeSlingshotBasePosition ((transform.position - slingshotPos).normalized 
			* col.bounds.size.x / 2.25f + transform.position);
	}

public void LaunchBird() {
        this._collider.enabled = true;
        this._rigidBody.isKinematic = false;
        //this.alreadyShot = true;
        //ABGameWorld.Instance.setContLaunchBirds();

        IsFlying = true;
        IsSelected = false;

        _animator.Play("flying", 0, 0f);

                // The bird starts with no gravity, so we must set it
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.gravityScale = _launchGravity;

        //ABGameWorld.Instance.setContLaunchBirds();
		Vector3 slingshotPos = ABGameWorld.Instance.Slingshot ().transform.position - ABConstants.SLING_SELECT_POS;
		Vector2 deltaPosFromSlingshot = (transform.position - slingshotPos);



		Vector2 f = -deltaPosFromSlingshot * _launchForce;

		_rigidBody.AddForce(f, ForceMode2D.Impulse);

		if(!ABGameWorld.Instance._isSimulation)
			_trailParticles._shootParticles = true;

		_audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.SHOT]);
	}

    public void LaunchBird(double angloCNN) {
        //this.GetComponent<Renderer>().enabled = true;
        this._collider.enabled = true;
        this._rigidBody.isKinematic = false;
        //alreadyShot = true;
        //ABGameWorld.Instance.setContLaunchBirds();

        IsFlying = true;
        IsSelected = false;

        _animator.Play("flying", 0, 0f);

        
        // The bird starts with no gravity, so we must set it
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.gravityScale = _launchGravity;
        //Debug.Log("_launchForce = " + _launchForce);
        //Debug.Log("velocity = " + this._rigidBody.velocity);
        //Debug.Log("magnitude = " + this._rigidBody.velocity.magnitude);
        //Debug.Log("sqrMagnitude = " + this._rigidBody.velocity.sqrMagnitude);
        Vector2 f = new Vector2((Mathf.Cos((((float)angloCNN) * Mathf.PI) / 180.0f) * _launchForce), (Mathf.Sin((((float)angloCNN) * Mathf.PI) / 180.0f) * _launchForce));
        _rigidBody.AddForce(f, ForceMode2D.Impulse);
        //Debug.Log("velocity-1 = " + this._rigidBody.velocity);
        //Debug.Log("magnitude-1 = " + this._rigidBody.velocity.magnitude);
        //Debug.Log("sqrMagnitude-1 = " + this._rigidBody.velocity.sqrMagnitude);
        //Debug.Log("Impulse = " + ForceMode2D.Impulse);
        OutOfSlingShot = true;
        if (!ABGameWorld.Instance._isSimulation)
            _trailParticles._shootParticles = true;

        _audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.SHOT]);
    }

    //identica a função de cima, mas o paramêtro é um double e não um float
    public void LaunchBird(float angloCNN) {
        //this.GetComponent<Renderer>().enabled = true;
        this._collider.enabled = true;
        this._rigidBody.isKinematic = false;
        //alreadyShot = true;
        
        //ABGameWorld.Instance.setContLaunchBirds();

        _animator.Play("flying", 0, 0f);

        IsFlying = true;
        IsSelected = false;

        // The bird starts with no gravity, so we must set it
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.gravityScale = _launchGravity;
        Vector2 f = new Vector2((Mathf.Cos((angloCNN * Mathf.PI) / 180.0f) * _launchForce), (Mathf.Sin((angloCNN * Mathf.PI) / 180.0f) * _launchForce));
        _rigidBody.AddForce(f, ForceMode2D.Impulse);
        OutOfSlingShot = true;

        if (!ABGameWorld.Instance._isSimulation)
            _trailParticles._shootParticles = true;

        _audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.SHOT]);
    }

    public IEnumerator __LaunchBird_(float time) {
        //this.GetComponent<Renderer>().enabled = true;
        this._collider.enabled = true;
        this._rigidBody.isKinematic = false;
        //alreadyShot = true;
        
        //ABGameWorld.Instance.setContLaunchBirds();

        _animator.Play("flying", 0, 0f);

        IsFlying = true;
        IsSelected = false;

        // The bird starts with no gravity, so we must set it
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.gravityScale = _launchGravity;
        Vector2 f = new Vector2((Mathf.Cos((64 * Mathf.PI) / 180.0f) * _launchForce), (Mathf.Sin((64 * Mathf.PI) / 180.0f) * _launchForce));
        _rigidBody.AddForce(f, ForceMode2D.Impulse);
        //Debug.Log("timeLaunchAbility = " + timeLaunchAbility);
        //timeAtivateAbility = Time.time;

        //Invoke("SpecialAttack", 2.5f);
        //Vector2 rrr = new Vector2();
        //yield return new WaitForSeconds(time);
        //Debug.Log("---->  = " + (Time.time - timeAtivateAbility));
        //this.SendMessage("SpecialAttack", SendMessageOptions.DontRequireReceiver);
        //yield return new WaitUntil(() => (Time.time > (timeAtivateAbility + 2.5f)));
        //bApAgAr = false;
        //Debug.Log("---->  = ");
        //rrr = this.transform.position;
        //rrr.y = 9f;
        //ABGameWorld.Instance._plataforms[0].transform.position = rrr;

        //bApAgAr = true;

        if (!ABGameWorld.Instance._isSimulation)
            _trailParticles._shootParticles = true;

        _audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.SHOT]);

        yield return null;
    }


    /*BACKUP FUNCTION LaunchBird
    public void LaunchBird(float angloCNN){
        _collider.enabled = true;
        _rigidBody.isKinematic = false;
        alreadyShot = true;
        //ABGameWorld.Instance.setContLaunchBirds();
        Debug.Log("CCCCCCCCCCCCCCCCC");
        //Vector3 slingshotPos = ABGameWorld.Instance.Slingshot().transform.position - ABConstants.SLING_SELECT_POS;
        //Vector2 deltaPosFromSlingshot = (transform.position - slingshotPos);
        _animator.Play("flying", 0, 0f);
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDD");
        IsFlying = true;
        IsSelected = false;
        Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEE");
        // The bird starts with no gravity, so we must set it
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.gravityScale = _launchGravity;
        Debug.Log("FFFFFFFFFFFFFFFFFFFFFFFFFF");
        Vector2 f = new Vector2((Mathf.Cos((angloCNN * Mathf.PI) / 180.0f) * _launchForce), (Mathf.Sin((angloCNN * Mathf.PI) / 180.0f) * _launchForce));
        _rigidBody.AddForce(f, ForceMode2D.Impulse);
        Debug.Log("f = " + f);
        Debug.Log("f.magnitude = " + f.magnitude);
        //Debug.Log("_rigidBody.velocity = " + _rigidBody.velocity);
        Debug.Log("_rigidBody.velocity.magnitude = " + _rigidBody.velocity.magnitude);
        //Debug.Log("CCCCCCCCCCCCCCCCC");
        if (!ABGameWorld.Instance._isSimulation)
            _trailParticles._shootParticles = true;

        _audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.SHOT]);
    }//*/


    /*//public void LaunchBird(Vector2 forceCNN){
    public void LaunchBird(float angloCNN){

        Vector3 slingshotPos = ABGameWorld.Instance.Slingshot ().transform.position - ABConstants.SLING_SELECT_POS;
		Vector2 deltaPosFromSlingshot = (transform.position - slingshotPos);
		_animator.Play("flying", 0, 0f);

        //JumpToSlingshot = true;
		IsFlying = true;
		IsSelected = false;

		// The bird starts with no gravity, so we must set it
		_rigidBody.velocity = Vector2.zero;
		_rigidBody.gravityScale = _launchGravity;

        //Debug.Log("_rigidBody.velocity-0 = " + _rigidBody.velocity);
        //Debug.Log("_rigidBody.gravityScale = " + _rigidBody.gravityScale);

        //Vector2 f = -deltaPosFromSlingshot * _launchForce;
        //Vector2 f = forceCNN;
        //Debug.Log("f = " + f.magnitude);

        //_rigidBody.AddForce(f, ForceMode2D.Impulse);
        //Debug.Log("forceCNN = " + forceCNN);
        //Debug.Log("forceCNN.magnitude = " + forceCNN.magnitude);

        Debug.Log("angloCNN: " + angloCNN);
        Debug.Log("Mathf.Rad2Deg: " + Mathf.Rad2Deg);
        Debug.Log("Mathf.PI: " + Mathf.PI);
        Debug.Log("Mathf.Cos(angloCNN): " + Mathf.Cos((angloCNN * Mathf.PI) / 180.0f));
        Debug.Log("Mathf.Sin(angloCNN): " + Mathf.Sin((angloCNN * Mathf.PI) / 180.0f));

        float forceX, forceY;
        forceX = 0.43011623f * Mathf.Cos((angloCNN * Mathf.PI) / 180.0f);
        forceY = 0.43011623f * Mathf.Sin((angloCNN * Mathf.PI) / 180.0f);

        Vector2 f = new Vector2((forceX * _launchForce), (forceY * _launchForce));
        //f = f *_launchForce;
        _rigidBody.AddForce(f, ForceMode2D.Impulse);
        Debug.Log("f = " + f);
        Debug.Log("f.magnitude = " + f.magnitude);

        float m_angle;

        m_angle = (Mathf.Atan2(f.y, f.x) * Mathf.Rad2Deg);

        Debug.Log("m_angle-0: " + m_angle);

        m_angle = (Mathf.Atan2(_rigidBody.velocity.y, _rigidBody.velocity.x) * Mathf.Rad2Deg);
        Debug.Log("m_angle-1: " + m_angle);

        Debug.Log("_rigidBody.velocity-1 = " + _rigidBody.velocity);
        Debug.Log("_rigidBody.velocity-2 = " + _rigidBody.velocity.magnitude);
        Debug.Log("\n---------------------------------------\n\nFIMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\n\n---------------------------------------\n\n");

        if (!ABGameWorld.Instance._isSimulation)
			_trailParticles._shootParticles = true;

		_audioSource.PlayOneShot(_clips[(int)OBJECTS_SFX.SHOT]);
	}//*/

    //void Update() {
    //    timer += Time.deltaTime;
    //    if ((this.IsFlying) && (bApAgAr) && (timer >= (timeAtivateAbility + 2.5f))) {
    //        bApAgAr = false;
    //        Debug.Log("***---->  = ");
    //        Vector2 rrr = new Vector2();
    //        rrr = this.transform.position;
    //        rrr.y = 9f;
    //        ABGameWorld.Instance._plataforms[0].transform.position = rrr;
    //    }
        //if ((this.IsFlying) && (bApAgAr) && (Time.time > (timeAtivateAbility + 2.5f))) {
        //    bApAgAr = false;
        //    Debug.Log("---->  = ");
        //    Vector2 rrr = new Vector2();
        //    rrr = this.transform.position;
        //    rrr.y = 9f;
        //    ABGameWorld.Instance._plataforms[0].transform.position = rrr;
        //}
        /*if (Time.timeScale == 1) {
            Debug.Log("this._rigidBody.velocity.magnitude = " + this._rigidBody.velocity.magnitude);
        }//*/
}
