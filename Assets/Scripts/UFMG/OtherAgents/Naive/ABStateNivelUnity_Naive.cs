using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenState_Naive {
    public int numFunction_ml;
    public double angle;
    public ulong numTimesChoice;

    public ChildrenState_Naive(int numFunction_ml_ = 300, double angle_ = -200.0, ulong numTimesChoice_ = 0) {
        this.angle = angle_;
        this.numFunction_ml = numFunction_ml_;
        this.numTimesChoice = numTimesChoice_;
    }

    public ChildrenState_Naive(ChildrenState_Naive c) {
        this.numFunction_ml = c.numFunction_ml;
        this.angle = c.angle;
        this.numTimesChoice = c.numTimesChoice;
    }
}

//public class ABStateNivelUnity_Naive : MonoBehaviour {
public class ABStateNivelUnity_Naive {
    private static float velocidade = 10.0f;//9.68f;
    private static double x1 = -8.15f, y1 = -1.7f;
    private static readonly float sqr2Velociade = Mathf.Pow(velocidade, 2.0f), sqr4Velociade = Mathf.Pow(velocidade, 4.0f);

    public static int img_height = 480;
    public static int img_width = 840;
    public static int img_heightRNA = 106;      //132;   // 264;   // 300;
    public static int img_widthRNA = 190;       //237;    // 474;   // 500;
    public static int img_heightRNAFinal = 351;
    public static int img_widthRNABegin = 224;
    public static int img_heightRNABeginl = 87;
    public static int img_widthRNAFinal = 697;

    public int numChildren;
    public int highTree { get; set; }
    public int[] wayAngleOrFunction;
    public ulong numTimesChoiceMax;
    public int highTreeInverted;
    public List<ChildrenState_Naive> statChildren;
    public ChildrenState_Naive childrenChoice;
    public float[,,,] matrix;
    public float[,,] numBirds;
    public bool recortFrame { get; set; }

    public ABStateNivelUnity_Naive(int numChildren_ = 0,  int highTree_ = 0, ulong numTimesChoiceMax_ = 0, int highTreeInverted_ = -1, ChildrenState_Naive childrenChoice_ = null, bool recortFrame_ = false) {
        this.numChildren = numChildren_;
        this.highTree = highTree_;
        if (this.highTree > 0) {
            this.wayAngleOrFunction = new int[this.highTree];
        } else {
            this.wayAngleOrFunction = null;
        }
        this.numTimesChoiceMax = numTimesChoiceMax_;
        this.highTreeInverted = highTreeInverted_;
        this.statChildren = new List<ChildrenState_Naive>();
        this.childrenChoice = childrenChoice_;
        this.matrix = new float[1, img_heightRNA, img_widthRNA, 6];
        numBirds = new float[1, 5, 7];
        for (int j = 0; j < 5; j++) {
            for (int i = 0; i < 7; i++) {
                this.numBirds[0, j, i] = 0f;
            }
        }
        this.recortFrame = recortFrame_;
    }

    public ABStateNivelUnity_Naive(ABStateNivelUnity_Naive stat) {
        this.numChildren = stat.numChildren;
        this.highTree = stat.highTree;
        if (stat.highTree > 0) {
            this.wayAngleOrFunction = new int[stat.highTree];
            for (int i = 0; i < this.highTree; i++) {
                this.wayAngleOrFunction[i] = stat.wayAngleOrFunction[i];
            }
        } else {
            this.wayAngleOrFunction = null;
        }
        this.numTimesChoiceMax = stat.numTimesChoiceMax;
        this.highTreeInverted = stat.highTreeInverted;
        this.statChildren = new List<ChildrenState_Naive>();
        for (int i = 0; i < stat.statChildren.Count; i++) {
            this.statChildren.Add(new ChildrenState_Naive(stat.statChildren[i]));
        }
        this.childrenChoice = stat.childrenChoice;
        this.matrix = new float[1, img_heightRNA, img_widthRNA, 6];
        for (int i = 0; i < img_heightRNA; i++) {
            for (int j = 0; j < img_widthRNA; j++) {
                for (int k = 0; k < 6; k++) {
                    this.matrix[0, i, j, k] = stat.matrix[0, i, j, k];
                }
            }
        }
        numBirds = new float[1, 5, 7];
        for (int j = 0; j < 5; j++)
            for (int i = 0; i < 7; i++)
                this.numBirds[0, 0, i] = stat.numBirds[0, j, i];
        this.recortFrame = stat.recortFrame;
    }

    //public void inicializaChildernProbability_1(float[,] resultEvatuate_) {
    //    for (uint i = 0; i < numChildren; i++) {
    //        this.statChildren.Add(new ChildrenState_Naive(resultEvatuate_[0, i], i));
    //    }
    //}

    /*public int choiceNumFunction_ml() {
        int choicePosStatChilden = UnityEngine.Random.Range(0, this.statChildren.Count);
        this.statChildren[choicePosStatChilden].numTimesChoice++;

        int auxReturn = this.statChildren[choicePosStatChilden].numFunction_ml;

        //double auxReturn = this.statChildren[choicePosStatChilden].angle;

        if (this.statChildren[choicePosStatChilden].numTimesChoice == this.numTimesChoiceMax) {
            this.statChildren.Remove(this.statChildren[choicePosStatChilden]);
            //            Debug.Log("#Removeeeeee#");
        }

        return auxReturn;
    }//*/

    public bool addChildenAngleLaunch(Vector2 target) {
        float gravity = 0.48f * Physics.gravity.magnitude; // _rigidBody.gravityScale (do pássaro no ABBird) = 0.48 ---> float gravity = _rigidBody.gravityScale * Physics.gravity.magnitude;
        //double deltaX = target.transform.position.x - x1;
        //double deltaY = target.transform.position.y - y1;
        double deltaX = target.x - x1;
        double deltaY = target.y - y1;

        double aux = Mathf.Pow((float)deltaX, 2.00f);

        aux *= gravity;

        double aux1 = 2 * deltaY * sqr2Velociade;

        aux += aux1;
        aux *= gravity;
        aux = sqr4Velociade - aux;
        aux = Mathf.Sqrt((float)aux);

        double denominador = gravity * deltaX;

        double resp;
        resp = sqr2Velociade + aux;
        resp /= denominador;

        resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;

        if (!Double.IsNaN(resp))
            this.statChildren.Add(new ChildrenState_Naive(numChildren++, resp));
        else {
            resp = sqr2Velociade - aux;
            resp /= denominador;
            resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;

            if(!Double.IsNaN(resp)) {
                this.statChildren.Add(new ChildrenState_Naive(numChildren++, resp));

                return true;
            }  else
                return false;

        }

        resp = sqr2Velociade - aux;
        resp /= denominador;
        resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;

        if(!Double.IsNaN(resp))
            this.statChildren.Add(new ChildrenState_Naive(numChildren++, resp));

        return true;
    }

    public void setNumTimesChoice(uint times) {
        this.childrenChoice.numTimesChoice += times;
        if (this.childrenChoice.numTimesChoice == this.numTimesChoiceMax) {
            this.statChildren.Remove(this.childrenChoice);
        }
    }

    public void ChildrenState_Naive_ml() {
        int choicePosStatChilden = UnityEngine.Random.Range(0, this.statChildren.Count);

        //Debug.Log("choicePosStatChilden = " + choicePosStatChilden);
        //Debug.Log("this.statChildren.Count = " + this.statChildren.Count);

        this.childrenChoice = this.statChildren[choicePosStatChilden];
        this.statChildren[choicePosStatChilden].numTimesChoice++;

        if (this.statChildren[choicePosStatChilden].numTimesChoice == this.numTimesChoiceMax) {
            this.statChildren.Remove(this.statChildren[choicePosStatChilden]);
        }
        //this.statChildren[choicePosStatChilden].numTimesChoice++;

        //int auxReturn = this.statChildren[choicePosStatChilden].numFunction_ml;

        //double auxReturn = this.statChildren[choicePosStatChilden].angle;

        //if (this.statChildren[i].numTimesChoice == this.numTimesChoiceMax) {
        //    this.sumProbabilityChildren -= this.statChildren[i].probabilityChildren;
        //    this.statChildren.Remove(this.statChildren[i]);
        //y}
    }//*/

    public bool isStatChildrenEmpty() {
        return this.statChildren.Count == 0;
    }

    public void setHighTree(int _highTree_, int numMaxBird) {
        this.highTree = _highTree_;

        if (_highTree_ > 0)
            this.wayAngleOrFunction = new int[this.highTree];
        else
            this.wayAngleOrFunction = null;

        this.highTreeInverted = numMaxBird - this.highTree;
        this.numTimesChoiceMax = ((ulong)Mathf.Pow(((ulong)this.statChildren.Count), (this.highTreeInverted - 1)));
    }

    public IEnumerator RecordFramePrint() {
        ///////////////////////////////////numBirds[0, 0, ABGameWorld.Instance.numBirdsRed] = 1f;
        //numBirds[0, 1, ABGameWorld.Instance.numBirdsBlue] = 1f;
        //numBirds[0, 2, ABGameWorld.Instance.numBirdsBlack] = 1f;
        //numBirds[0, 3, ABGameWorld.Instance.numBirdsYellow] = 1f;
        //numBirds[0, 4, ABGameWorld.Instance.numBirdsWhite] = 1f;

        Texture2D inputTextures;

        //ABGameWorld.Instance.RenderEnabledSlingShot(true);
        for (int i = 0; i < ABGameWorld.Instance._birds.Count; i++) {
            ABGameWorld.Instance._birds[i].GetComponent<Renderer>().enabled = false;
        }

        for (int i = 0; i < ABGameWorld.Instance._pigs.Count; i++) {
            ABGameWorld.Instance._pigs[i].GetComponent<Renderer>().enabled = false;
        }

        for (int i = 0; i < ABGameWorld.Instance._blocks.Count; i++) {
            ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = false;
        }

        for (int i = 0; i < ABGameWorld.Instance._tnts.Count; i++) {
            ABGameWorld.Instance._tnts[i].GetComponent<Renderer>().enabled = false;
        }

        for (int i = 0; i < ABGameWorld.Instance._plataforms.Count; i++) {
            ABGameWorld.Instance._plataforms[i].GetComponent<Renderer>().enabled = true;
        }

        yield return new WaitForEndOfFrame();
        inputTextures = ScreenCapture.CaptureScreenshotAsTexture();

        int row = 0, col;
        for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl;) {
            col = 0;
            for (int j = img_widthRNABegin; j < img_widthRNAFinal;) {
                if (inputTextures.GetPixel(j, i).r != 0){ 
                    matrix[0, row, col, 5] = 1;
                } else {
                    matrix[0, row, col, 5] = 0;
                }
                if (col % 2 == 0) {
                    j += 2;
                } else {
                    j += 3;
                }
                col++;
            }
            if (row % 2 == 0){
                i -= 2;
            } else {
                i -= 3;
            }
            row++;
        }

        for (int i = 0; i < ABGameWorld.Instance._plataforms.Count; i++) {
            ABGameWorld.Instance._plataforms[i].GetComponent<Renderer>().enabled = false;
        }

        for (int i = 0; i < ABGameWorld.Instance._pigs.Count; i++) {
            ABGameWorld.Instance._pigs[i].GetComponent<Renderer>().enabled = true;
        }

        yield return new WaitForEndOfFrame();
        inputTextures = ScreenCapture.CaptureScreenshotAsTexture();
        row = 0;
        for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl;) {     // for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl; i--) {
            col = 0;
            for (int j = img_widthRNABegin; j < img_widthRNAFinal;) {   // for (int j = img_widthRNABegin; j < img_widthRNAFinal; j++) {
                if (inputTextures.GetPixel(j, i).g != 0)
                    matrix[0, row, col, 0] = 1;
                else
                    matrix[0, row, col, 0] = 0;
                if (col % 2 == 0)
                    j += 2;
                else
                    j += 3;
                col++;
            }
            if (row % 2 == 0)
                i -= 2;
            else
                i -= 3;
            row++;
        }

        for (int i = 0; i < ABGameWorld.Instance._pigs.Count; i++) {
            ABGameWorld.Instance._pigs[i].GetComponent<Renderer>().enabled = false;
        }

        for (int i = 0; i < ABGameWorld.Instance._tnts.Count; i++)
            ABGameWorld.Instance._tnts[i].GetComponent<Renderer>().enabled = true;

        yield return new WaitForEndOfFrame();
        inputTextures = ScreenCapture.CaptureScreenshotAsTexture();
        row = 0;
        for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl;) {     // for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl; i--) {
            col = 0;
            for (int j = img_widthRNABegin; j < img_widthRNAFinal;) {   // for (int j = img_widthRNABegin; j < img_widthRNAFinal; j++) {
                if (inputTextures.GetPixel(j, i).g != 0)
                    matrix[0, row, col, 4] = 1;
                else
                    matrix[0, row, col, 4] = 0;
                if (col % 2 == 0)
                    j += 2;
                else
                    j += 3;
                col++;
            }
            if (row % 2 == 0)
                i -= 2;
            else
                i -= 3;
            row++;
        }

        for (int i = 0; i < ABGameWorld.Instance._tnts.Count; i++)
            ABGameWorld.Instance._tnts[i].GetComponent<Renderer>().enabled = false;

        for (int i = 0; i < ABGameWorld.Instance._blocks.Count; i++)
            if (ABGameWorld.Instance._blocks[i]._material == MATERIALS.ice)
                ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = true;

        yield return new WaitForEndOfFrame();
        inputTextures = ScreenCapture.CaptureScreenshotAsTexture();

        row = 0;
        for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl;) {
            col = 0;
            for (int j = img_widthRNABegin; j < img_widthRNAFinal;) {
                if (inputTextures.GetPixel(j, i).b != 0){ 
                    matrix[0, row, col, 1] = 1;
                } else {
                    matrix[0, row, col, 1] = 0;
                }
                if (col % 2 == 0) {
                    j += 2;
                } else {
                    j += 3;
                }
                col++;
            }
            if (row % 2 == 0){
                i -= 2;
            } else {
                i -= 3;
            }
            row++;
        }

        for (int i = 0; i < ABGameWorld.Instance._blocks.Count; i++) {
            if(ABGameWorld.Instance._blocks[i]._material == MATERIALS.ice)
                ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = false;
            else if(ABGameWorld.Instance._blocks[i]._material == MATERIALS.wood)
                ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = true;
        }

        yield return new WaitForEndOfFrame();
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/UFMG/Testes/printTesteWOOD.png");
        inputTextures = ScreenCapture.CaptureScreenshotAsTexture();

        row = 0;
        for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl;) {
            col = 0;
            for (int j = img_widthRNABegin; j < img_widthRNAFinal;) {
                if (inputTextures.GetPixel(j, i).r != 0){ 
                    matrix[0, row, col, 2] = 1;
                } else {
                    matrix[0, row, col, 2] = 0;
                }
                if (col % 2 == 0) {
                    j += 2;
                } else {
                    j += 3;
                }
                col++;
            }
            if (row % 2 == 0){
                i -= 2;
            } else {
                i -= 3;
            }
            row++;
        }

        for (int i = 0; i < ABGameWorld.Instance._blocks.Count; i++) {
            if(ABGameWorld.Instance._blocks[i]._material == MATERIALS.wood)
                ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = false;
            else if(ABGameWorld.Instance._blocks[i]._material == MATERIALS.stone)
                ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = true;
        }

        yield return new WaitForEndOfFrame();
        inputTextures = ScreenCapture.CaptureScreenshotAsTexture();

        row = 0;
        for (int i = (img_heightRNAFinal - 1); i >= img_heightRNABeginl;) {
            col = 0;
            for (int j = img_widthRNABegin; j < img_widthRNAFinal;) {
                if (inputTextures.GetPixel(j, i).r != 0){ 
                    matrix[0, row, col, 3] = 1;
                } else {
                    matrix[0, row, col, 3] = 0;
                }
                if (col % 2 == 0) {
                    j += 2;
                } else {
                    j += 3;
                }
                col++;
            }
            if (row % 2 == 0){
                i -= 2;
            } else {
                i -= 3;
            }
            row++;
        }

        this.recortFrame = true;
    }
}