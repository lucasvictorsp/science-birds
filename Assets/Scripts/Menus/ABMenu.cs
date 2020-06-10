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

public class ABMenu : MonoBehaviour {

    public static int cont;
    public static bool flag =  true;

    void Awake() {
        cont = 0;
    }

    public void LoadNextScene(string sceneName) {
        //Debug.Log("1111 ----> sceneName = " + sceneName);
        ABSceneManager.Instance.LoadScene(sceneName);
    }

    public void LoadNextScene(string sceneName, bool loadTransition, ABSceneManager.ActionBetweenScenes action){
        //Debug.Log("2222 --> sceneName = " + sceneName + ", loadTransition = " + loadTransition + ", action = " + action);
        ABSceneManager.Instance.LoadScene(sceneName, loadTransition, action);
    }

    /*void Update() {
        if(flag){
            flag = false;
            LoadNextScene("LevelSelectMenu");
        }// else {
         //   ABSceneManager.Instance.LoadScene("GameWorld", true, "ABSceneManager+ActionBetweenScenes");
        //}
        //LoadNextScene("LevelSelectMenu");
    }//*/
}
