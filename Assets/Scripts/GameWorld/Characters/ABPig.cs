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

public class ABPig : ABCharacter {

	public override void Die(bool withEffect = true) {
        //if(!ABGameWorld.Instance._isSimulation)
		    ScoreHud.Instance.SpawnScorePoint(50, transform.position);
		ABGameWorld.Instance.KillPig(this);

		base.Die(withEffect);
	}

    /*public void Update() {
        //Debug.Log("(" + this.transform.position.x + this.transform.position.y + "), _currentLife" + _currentLife);
        //Debug.Log(this._collider.bounds.size.y);
    }//*/

    /*public override void DieNotDestroy() {
        ScoreHud.Instance.SpawnScorePoint(50, transform.position);
        //ABGameWorld.Instance.KillPigNotDestroy(this);
        ABGameWorld.Instance.KillPigNoDestroy(this);
        //ABGameWorld.Instance.KillPigNoDestroy2(this);
        base.DieNotDestroy();
    }//*/
}
