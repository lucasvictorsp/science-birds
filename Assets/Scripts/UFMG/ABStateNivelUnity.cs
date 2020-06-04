using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineLaunch {
    private int _angle;
    private float _timeAttackAbility;

    public DefineLaunch(int _angle_ = -36, float _timeAttackAbility_ = 0.0f) {
        this._angle = _angle_;
        this._timeAttackAbility = _timeAttackAbility_;
    }

    public DefineLaunch(ref DefineLaunch w) {
        this._angle = w._angle;
        this._timeAttackAbility = w._timeAttackAbility;
    }

    public int get_angle() {
        return this._angle;
    }

    public float get_timeAttackAbility() {
        return this._timeAttackAbility;
    }

    public void set_angle(ref int _angle_) {
        this._angle = _angle_;
    }

    public void set_timeAttackAbility(ref float _timeAttackAbility_) {
        this._timeAttackAbility = _timeAttackAbility_;
    }

    public bool SameObject(ref DefineLaunch d) {
        if (this._angle != d.get_angle()) {
            return false;
        } else if(this._timeAttackAbility != d.get_timeAttackAbility()) {
            return false;
        }

        return true;
    }

}

public class ChildrenState {
    public double probabilityChildren;
    //public int angle;
    //public float timeAttackAbility;
    public DefineLaunch dLauch;
    public ulong numTimesChoice;

    public ChildrenState(double probabilityChildren_ = 0.0, int angle_ = -36, float timeAttackAbility_ = 0.0f, ulong numTimesChoice_ = 0) {
        this.probabilityChildren = probabilityChildren_;
        //this.angle = angle_;
        //this.timeAttackAbility = timeAttackAbility_;
        dLauch = new DefineLaunch();
        dLauch.set_angle(ref angle_);
        dLauch.set_timeAttackAbility(ref timeAttackAbility_);
        this.numTimesChoice = numTimesChoice_;
    }

    public ChildrenState(ChildrenState c) {
        this.probabilityChildren = c.probabilityChildren;
        //this.angle = c.angle;
        //this.timeAttackAbility = c.timeAttackAbility;
        this.dLauch = new DefineLaunch(ref c.dLauch);
        this.numTimesChoice = c.numTimesChoice;
    }
}

//public class ABStateNivelUnity : MonoBehaviour {
public class ABStateNivelUnity {
    private static int numChildrenAngle = 82;
    private static int numChildrenTime = 22;

    public static int img_height = 480;
    public static int img_width = 840;
    public static int img_heightRNA = 106;      //132;   // 264;   // 300;
    public static int img_widthRNA = 190;       //237;    // 474;   // 500;
    public static int img_heightRNAFinal = 351;
    public static int img_widthRNABegin = 224;
    public static int img_heightRNABeginl = 87;
    public static int img_widthRNAFinal = 697;

    //public static int numChildren = 32;
    //public static int numChildren = 124;
    //public static int numChildren = 127;
    //public static int numChildren = 254;
    //public static int numChildren = 256;
    public static int numChildren = 1804; //82*22
    //public static int numChildren = 75;

    public int highTree { get; set; }
    public DefineLaunch [] wayAngleOrFunction;
    public ulong numTimesChoiceMax;
    public int highTreeInverted;
    public double probability;
    public List<ChildrenState> statChildren;
    public double sumProbabilityChildren;
    public float[,,,] matrix;
    public float[,,] numBirds;
    public bool recortFrame { get; set; }
    private static ABRandomGenerator rdm;

    public ABStateNivelUnity(int highTree_ = 0, ulong numTimesChoiceMax_ = 0, int highTreeInverted_ = -1, double probability_ = 0.0, double sumProbabilityChildren_ = 1.0, ABStateNivelUnity father_ = null, bool recortFrame_ = false, int numFunction_ml_ = -1) {
        this.highTree = highTree_;
        if (this.highTree > 0) {
            //this.wayAngleOrFunction = new int[this.highTree];
            this.wayAngleOrFunction = new DefineLaunch[this.highTree];
        } else {
            this.wayAngleOrFunction = null;
        }
        this.numTimesChoiceMax = numTimesChoiceMax_;
        this.highTreeInverted = highTreeInverted_;
        this.probability = probability_;
        this.statChildren = new List<ChildrenState>();    //this.statChildren = new ChildrenState[numChildren];
        this.sumProbabilityChildren = sumProbabilityChildren_;
        this.matrix = new float[1, img_heightRNA, img_widthRNA, 6];
        numBirds = new float[1, 10, 5];  // new float[1, 5, 7];
        for (int j = 0; j < 10; j++){
            for (int i = 0; i < 5; i++) {
                this.numBirds[0, j, i] = 0f;
            }
        }

        //numBirds = new float[1, 5, 7];  // new float[1, 5, 7];
        //for (int j = 0; j < 5; j++){
        //    for (int i = 0; i < 7; i++) {
        //        this.numBirds[0, j, i] = 0f;
        //    }
        //}
        this.recortFrame = recortFrame_;
        rdm = new ABRandomGenerator();
    }

    public ABStateNivelUnity(ABStateNivelUnity stat) {
        this.highTree = stat.highTree;
        if (stat.highTree > 0) {
            //this.wayAngleOrFunction = new int[stat.highTree];
            this.wayAngleOrFunction = new DefineLaunch[stat.highTree];
            for (int i = 0; i < this.highTree; i++) {
                this.wayAngleOrFunction[i] = stat.wayAngleOrFunction[i];
            }
        } else {
            this.wayAngleOrFunction = null;
        }
        this.numTimesChoiceMax = stat.numTimesChoiceMax;
        this.highTreeInverted = stat.highTreeInverted;
        this.probability = stat.probability;
        this.statChildren = new List<ChildrenState>();
        for (int i = 0; i < numChildren; i++) {
            this.statChildren.Add(new ChildrenState(stat.statChildren[i]));
        }
        this.sumProbabilityChildren = stat.sumProbabilityChildren;
        this.matrix = new float[1, img_heightRNA, img_widthRNA, 6];
        for (int i = 0; i < img_heightRNA; i++) {
            for (int j = 0; j < img_widthRNA; j++) {
                for (int k = 0; k < 6; k++) {
                    this.matrix[0, i, j, k] = stat.matrix[0, i, j, k];
                }
            }
        }
        numBirds = new float[1, 10, 5];  //  new float[1, 5, 7];
        for (int j = 0; j < 10; j++)
            for (int i = 0; i < 5; i++)
                this.numBirds[0, 0, i] = stat.numBirds[0, j, i];

        //numBirds = new float[1, 5, 7];  //  new float[1, 5, 7];
        //for (int j = 0; j < 5; j++)
        //    for (int i = 0; i < 7; i++)
        //        this.numBirds[0, 0, i] = stat.numBirds[0, j, i];
        this.recortFrame = stat.recortFrame;
        rdm = new ABRandomGenerator();
    }

    public void inicializaChildernProbability(ref float [,] resultEvatuateAngle_, ref float[,] resultEvatuateTime_) {
        this.statChildren.Clear();

        //for (int j = 0; j < numChildrenTime; j++) {
        //    for (int i = 0; i < numChildrenAngle; i++) {
        //        //this.statChildren.Add(new ChildrenState(resultEvatuate_[0, i], (-35 +  i)));
        //        this.statChildren.Add(new ChildrenState((resultEvatuateAngle_[0, i] * resultEvatuateTime_[0, j]), (-6 + i), (0.5f + (j * 0.1f))));
        //    }
        //}

        //float yyy = 0;

        for (int i = 0; i < numChildrenAngle; i++) {
	        for (int j = 0; j < numChildrenTime; j++) {
                if (j > 0) {
                    //this.statChildren.Add(new ChildrenState(resultEvatuate_[0, i], (-35 +  i)));
                    this.statChildren.Add(new ChildrenState((resultEvatuateAngle_[0, i] * resultEvatuateTime_[0, j]), (-6 + i), (0.6f + (j * 0.1f))));
                    //yyy += (resultEvatuateAngle_[0, i] * resultEvatuateTime_[0, j]);
                } else {
                    this.statChildren.Add(new ChildrenState((resultEvatuateAngle_[0, i] * resultEvatuateTime_[0, j]), (-6 + i), -1));
                }
            }
    	}

        //Debug.Log("yyy = " + yyy);

        //this.orderStatChildren();
        this.sumProbabilityChildren = 1.0;
    }

    //public void orderStatChildren() {
    //    this.statChildren.Sort( delegate (ChildrenState child1, ChildrenState child2) { return child1.probabilityChildren.CompareTo(child2.probabilityChildren); }); // (user1.Age - user2.Age)
    //}

    /*public int choiceAngleBacktracking() {
        //Debug.Log("angle = " + this.statChildren[(statChildren.Count - 1)].angle);
        //Debug.Log("this.numTimesChoiceMax = " + this.numTimesChoiceMax);
        //Debug.Log("(statChildren.Count - 1) = " + (statChildren.Count - 1));
        //Debug.Log("probabilityChildren = " + this.statChildren[(statChildren.Count - 1)].probabilityChildren);
        //Debug.Log("0-numTimesChoice = " + this.statChildren[(statChildren.Count - 1)].numTimesChoice);
        int auxReturn;
        auxReturn = this.statChildren[(statChildren.Count - 1)].angle;
        this.statChildren[(statChildren.Count - 1)].numTimesChoice++;
        //Debug.Log("1-numTimesChoice = " + this.statChildren[(statChildren.Count - 1)].numTimesChoice);
        if (this.statChildren[(statChildren.Count - 1)].numTimesChoice == this.numTimesChoiceMax) {
            //Debug.Log("Removeeeeee");
            this.sumProbabilityChildren -= this.statChildren[(statChildren.Count - 1)].probabilityChildren;
            this.statChildren.Remove(this.statChildren[(statChildren.Count - 1)]);
        }
        //Debug.Log("------------------------------");
        return auxReturn;
    }//*/

    public DefineLaunch choiceAngleBacktracking() {
        DefineLaunch auxReturn;
        auxReturn = this.statChildren[(statChildren.Count - 1)].dLauch;
        this.statChildren[(statChildren.Count - 1)].numTimesChoice++;

        if (this.statChildren[(statChildren.Count - 1)].numTimesChoice == this.numTimesChoiceMax) {
            this.sumProbabilityChildren -= this.statChildren[(statChildren.Count - 1)].probabilityChildren;
            this.statChildren.Remove(this.statChildren[(statChildren.Count - 1)]);
        }

        return auxReturn;
    }

    /*public int choiceAngle(ref int angleLaunch) {
        double prob_ = UnityEngine.Random.Range(0f, ((float)this.sumProbabilityChildren));

        for (int i = 0; i < statChildren.Count; i++) {
            if (this.statChildren[i].probabilityChildren > prob_) {
                angleLaunch = this.statChildren[i].angle;
                //this.statChildren[i].numTimesChoice++;
                //if (this.statChildren[i].numTimesChoice == this.numTimesChoiceMax) {
                //    this.sumProbabilityChildren -= this.statChildren[i].probabilityChildren;
                //    this.statChildren.Remove(this.statChildren[i]);
                //}
                return i;
            }
        }

        angleLaunch = this.statChildren[(this.statChildren.Count - 1)].angle;
        //this.statChildren[(statChildren.Count - 1)].numTimesChoice++;
        //if (this.statChildren[(statChildren.Count - 1)].numTimesChoice == this.numTimesChoiceMax) {
        //    this.sumProbabilityChildren -= this.statChildren[(statChildren.Count - 1)].probabilityChildren;
        //    this.statChildren.Remove(this.statChildren[(statChildren.Count - 1)]);
        //}
        return (this.statChildren.Count - 1);
    }//*/

    /*public int choiceAngle(ref int angleLaunch) {
        double prob_ = UnityEngine.Random.Range(0f, ((float)this.sumProbabilityChildren));

        for (int i = 0; i < statChildren.Count; i++) {
            if (this.statChildren[i].probabilityChildren > prob_) {
                angleLaunch = this.statChildren[i].angle;
                //this.statChildren[i].numTimesChoice++;
                //if (this.statChildren[i].numTimesChoice == this.numTimesChoiceMax) {
                //    this.sumProbabilityChildren -= this.statChildren[i].probabilityChildren;
                //    this.statChildren.Remove(this.statChildren[i]);
                //}
                return i;
            }
        }

        angleLaunch = this.statChildren[(this.statChildren.Count - 1)].angle;
        //this.statChildren[(statChildren.Count - 1)].numTimesChoice++;
        //if (this.statChildren[(statChildren.Count - 1)].numTimesChoice == this.numTimesChoiceMax) {
        //    this.sumProbabilityChildren -= this.statChildren[(statChildren.Count - 1)].probabilityChildren;
        //    this.statChildren.Remove(this.statChildren[(statChildren.Count - 1)]);
        //}
        return (this.statChildren.Count - 1);
    }//*/

    public DefineLaunch choiceAngle_(ref int angleLaunch) {
        double prob_ = UnityEngine.Random.Range(0f, ((float)this.sumProbabilityChildren));

        foreach(ChildrenState d in this.statChildren) {
            if (d.probabilityChildren > prob_) {
                angleLaunch = d.dLauch.get_angle();
                return d.dLauch;
            }
        }

        angleLaunch = this.statChildren[(this.statChildren.Count - 1)].dLauch.get_angle();
        return this.statChildren[(this.statChildren.Count - 1)].dLauch;
    }//*/

    public DefineLaunch choiceAngle_(ref DefineLaunch _dLaunch) {
        double prob_ = UnityEngine.Random.Range(0f, ((float)this.sumProbabilityChildren));

        foreach(ChildrenState d in this.statChildren) {
            if (d.probabilityChildren > prob_) {
                _dLaunch = d.dLauch;
                return d.dLauch;
            }
        }

        _dLaunch = this.statChildren[(this.statChildren.Count - 1)].dLauch;
        return this.statChildren[(this.statChildren.Count - 1)].dLauch;
    }//*/

    public int choiceAngle(ref DefineLaunch _dLaunch) {
        //Debug.Log("Teste-0");
        double prob_ = UnityEngine.Random.Range(0f, ((float)this.sumProbabilityChildren));
        //Debug.Log("Teste-1_prob_ = " + prob_);
        //foreach(ChildrenState d in this.statChildren) {
        for(int i = 0; i < this.statChildren.Count; i++) {
            //Debug.Log("Teste-2_ i = " + i + ", --> = " + this.statChildren[i].probabilityChildren);
            if (this.statChildren[i].probabilityChildren > prob_) {
                //Debug.Log("Teste-3_IF");
                _dLaunch = this.statChildren[i].dLauch;
                return i;
            }
        }
        //Debug.Log("Teste-4");

        _dLaunch = this.statChildren[(this.statChildren.Count - 1)].dLauch;
        return (this.statChildren.Count - 1);
    }//*/

    /*public override Int32 Next(Int32 minValue, Int32 maxValue) {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException("minValue");
        if (minValue == maxValue) return minValue;
        Int64 diff = maxValue - minValue;
        while (true) {
            _rng.GetBytes(_uint32Buffer);
            UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);

            Int64 max = (1 + (Int64)UInt32.MaxValue);
            Int64 remainder = max % diff;
            if (rand < max - remainder) {
                return (Int32)(minValue + (rand % diff));
            }
        }
    }//*/

    /*public static byte[] RandomNumbersInRange(byte count, byte lower, byte upper) {
        if (count == 0)
            throw new IndexOutOfRangeException();
        if (lower >= upper)
            throw new IndexOutOfRangeException();
        byte modulo = (byte)(upper - lower + 1);
        if (modulo > byte.MaxValue / 2)  //for bigger numbers I would not use byte 
            throw new IndexOutOfRangeException();

        byte[] randomNumbersInRange = new byte[count];
        byte upperLimit = (byte)(byte.MaxValue - byte.MaxValue % modulo - 1);
        byte[] bigRandomsInRange = RandomsInRange(upperLimit, count);

        for (int i = 0; i < count; i++) {
            randomNumbersInRange[i] = (byte)((bigRandomsInRange[i] % modulo) + lower);
        }
        return randomNumbersInRange;
    }

    public static byte[] RandomsInRange(byte upperLimit, byte count) {
        if (count == 0)
            throw new IndexOutOfRangeException();
        if (upperLimit == byte.MaxValue)
            throw new IndexOutOfRangeException();
        byte[] workingBytes = new byte[1];
        byte workingByte;
        byte[] randomsInRange = new byte[count];
        using (var csp = new System.Security.Cryptography.RNGCryptoServiceProvider()) {
            for (int i = 0; i < count; i++) {
                workingByte = byte.MaxValue;
                while (workingByte > upperLimit) {
                    csp.GetBytes(workingBytes);
                    workingByte = workingBytes[0];
                }
                randomsInRange[i] = workingByte;
            }
        }
        return randomsInRange;
    }//*/

    public int choiceAngleSemProbability(ref DefineLaunch _dLaunch) {
        /*Int64 posElem;
        using (System.Security.Cryptography.RNGCryptoServiceProvider rg = new System.Security.Cryptography.RNGCryptoServiceProvider()) {
            byte[] rno = new byte[4];
            while (true) {
                rg.GetBytes(rno);
                Int64 diff = this.statChildren.Count;
                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                Int64 randomvalue = BitConverter.ToInt32(rno, 0);
                if (randomvalue < (max - remainder)) {
                    //return randomvalue;
                    posElem = (randomvalue % diff);
                    break;
                }
            }
        }//*/
        /*Debug.Log("trtrrrrrrrrrrrrrrr");
        System.Security.Cryptography.RNGCryptoServiceProvider RNG = new System.Security.Cryptography.RNGCryptoServiceProvider();
        byte[] bytes = new byte[4];
        int result = 0;
        Debug.Log("trtrrrrrrrrrrrrrrr-1");
        do {
            Debug.Log("trtrrrrrrrrrrrrrrr-2");
            RNG.GetBytes(bytes);
            result = BitConverter.ToInt32(bytes, result);
        } while ((result <= 0) || (result >= this.statChildren.Count));
        Debug.Log("trtrrrrrrrrrrrrrrr-3");//*/

        //int result = UnityEngine.Random.Range(0, this.statChildren.Count);
        int result = rdm.Next(0, this.statChildren.Count);

        //Debug.Log("this.statChildren.Count = " + this.statChildren.Count + ", result = " + result);
        _dLaunch = this.statChildren[result].dLauch;//*/

        return result;
    }//*/

    public void incrementNumTimesChoice(ref int posIncrement) {
        this.statChildren[posIncrement].numTimesChoice++;
        if (this.statChildren[posIncrement].numTimesChoice == this.numTimesChoiceMax) {
            this.sumProbabilityChildren -= this.statChildren[posIncrement].probabilityChildren;
            this.statChildren.Remove(this.statChildren[posIncrement]);
        }
    }

    public bool isStatChildrenEmpty() {
        return this.statChildren.Count == 0;
    }

    public void setHighTree(int _highTree_, int numMaxBird) {
        this.highTree = _highTree_;

        if (_highTree_ > 0)
            //this.wayAngleOrFunction = new int[this.highTree];
            this.wayAngleOrFunction = new DefineLaunch[this.highTree];
        else
            this.wayAngleOrFunction = null;

        this.highTreeInverted = numMaxBird - this.highTree;
        this.numTimesChoiceMax = ((ulong)Mathf.Pow(((ulong)numChildren), (this.highTreeInverted - 1)));
    }

    private void positionBirdsInArrayNumBirds(string typeBird_, int pos) {
        switch (typeBird_) {
            case "BirdRed":
            {
                this.numBirds[0, pos, 0] = 1;
                break;
            }
            case "BirdBlack":
            {
                this.numBirds[0, pos, 1] = 1;
                break;
            }
            case "BirdBlue":
            {
                this.numBirds[0, pos, 2] = 1;
                break;
            }
            case "BirdYellow":
            {
                this.numBirds[0, pos, 3] = 1;
                break;
            }
            case "BirdWhite":
            {
                this.numBirds[0, pos, 4] = 1;
                break;
            }
            default:
            {
                Debug.Log("Error funcao in ABStateNivelUnity.\n");
                break;
            }
        }
    }

    public IEnumerator RecordFramePrint() {
        /*
         [x,y,0] = essa coluna representa o pássaro red 
         [x,y,1] = essa coluna representa o pássaro black
         [x,y,2] = essa coluna representa o pássaro blue
         [x,y,3] = essa coluna representa o pássaro yellow
         [x,y,4] = essa coluna representa o pássaro white
         */
        for(int i = 0; i < ABGameWorld.Instance._birds.Count; i++) {
            this.positionBirdsInArrayNumBirds(ABGameWorld.Instance._birds[i].typeBird, i);
        }

        //numBirds[0, 0, ABGameWorld.Instance.numBirdsRed] = 1f;
        //numBirds[0, 1, ABGameWorld.Instance.numBirdsBlue] = 1f;
        //numBirds[0, 2, ABGameWorld.Instance.numBirdsBlack] = 1f;
        //numBirds[0, 3, ABGameWorld.Instance.numBirdsYellow] = 1f;
        //numBirds[0, 4, ABGameWorld.Instance.numBirdsWhite] = 1f;

        //this.recortFrame = false;
        Texture2D inputTextures;

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