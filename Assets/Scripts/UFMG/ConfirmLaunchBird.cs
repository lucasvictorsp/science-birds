using System.Collections;
using UnityEngine;
using System.IO;

using System.Collections.Generic;

public class ConfirmLaunchBird : MonoBehaviour {
    private static bool beginEvaluateNivel;
    private static bool firstExecute = true;

    private static TextAsset anglesCorrect;

    protected FileInfo theSourceFile = null;
    //private static StreamReader arqRead = null;

    // Use this for initialization
    void Start () {
        beginEvaluateNivel = true;
        if (firstExecute) {
            firstExecute = false;
            /*theSourceFile = new FileInfo("F:/ml-Agentes-TF-Unity/SB-ml-Agents-OH-P-106x190x6-VB-H_LTS-Destroy-ModelFunction/Assets/Resources/UFMG/bootstrapUFMG/BootstrapAnglesLine.txt");
            arqRead = theSourceFile.OpenText();
            string text = "";
            for () {
                if (text != null) {
                    text = arqRead.ReadLine();
                    //Console.WriteLine(text);
                    print(text);
                }
            }//*/


            anglesCorrect = Resources.Load<TextAsset>("bootstrapUFMG/BootstrapAnglesLine.txt");
        }
    }


    private IEnumerator Sample_traj() {
        Debug.Log("anglesCorrect =\n" + anglesCorrect);
        Debug.Log("anglesCorrect.text =\n" + anglesCorrect.text);

        yield return null;
    }

	// Update is called once per frame
	//void Update () {
		
	//}

    void FixedUpdate() {
        //if (beginEvaluateNivel && (ABGameWorld.Instance.levelStable) && (ABGameWorld.Instance.getWonGame() == 0)) {
        //if (beginEvaluateNivel && (ABGameWorld.Instance.getWonGame() == 0)) {
        if (beginEvaluateNivel) {
            beginEvaluateNivel = false;
            StartCoroutine(Sample_traj());
        }
    }

}
