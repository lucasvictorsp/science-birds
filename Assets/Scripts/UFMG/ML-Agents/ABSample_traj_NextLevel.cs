using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using TensorFlow;
//using UnityEngine.UI;


public class PossibleSolution {
    private List<ABStateNivelUnity> state;
    private int pointSolution, _iter_, contML_, contNotML_;
    private double timeSolution;

    public PossibleSolution(List<ABStateNivelUnity> state_ = null, int pointSolution_ = -1, int _iter__ = -1, int _contML_ = -1, int _contNotML_ = -1, double timeSolution_ = 0) {
        if (state_ == null)
            this.state = new List<ABStateNivelUnity>();
        else
            this.state = new List<ABStateNivelUnity>(state_);
        this.pointSolution = pointSolution_;
        this._iter_ = _iter__;
        this.contML_ = _contML_;
        this.contNotML_ = _contNotML_;
        this.timeSolution = timeSolution_;
    }

    public PossibleSolution(ref ABStateNivelUnity [] state_, ref int sizeArray, int pointSolution_, int _iter__, int _contML_, int _contNotML_, double timeSolution_) {
        //this.state = state_;
        for (int i = 0; i < sizeArray; i++)
            this.state.Add(new ABStateNivelUnity(state_[i]));
        this.pointSolution = pointSolution_;
        this._iter_ = _iter__;
        this.contML_ = _contML_;
        this.contNotML_ = _contNotML_;
        this.timeSolution = timeSolution_;
    }

    public PossibleSolution(ref PossibleSolution p) {
        this.state = new List<ABStateNivelUnity>(p.state);
        this.pointSolution = p.pointSolution;
        this._iter_ = p._iter_;
        this.contML_ = p.contML_;
        this.contNotML_ = p.contNotML_;
        this.timeSolution = p.timeSolution;
    }

    public ABStateNivelUnity getStatUnity(int posElem) {
        if(posElem < state.Count)
            return this.state[posElem];

        return null;
    }

    public List<ABStateNivelUnity> getStat() {
            return this.state;
    }

    public int getStatSize() {
            return this.state.Count;
    }

    public int getPointSolution() {
        return this.pointSolution;
    }

    public int get_iter_() {
        return this._iter_;
    }

    public int getContML_() {
        return this.contML_;
    }

    public int getContNotML_() {
        return this.contNotML_;
    }

    public double getTimeSolution() {
        return this.timeSolution;
    }

    public void setStateUnity(ref ABStateNivelUnity abs) {
        this.state.Add(new ABStateNivelUnity(abs));
    }

    public void setState(ref List<ABStateNivelUnity> abs) {
        this.state = new List<ABStateNivelUnity>(abs);
    }

    public void setPointSolution(int TotalPoint) {
        this.pointSolution = TotalPoint;
    }

    public void set_iter_(int _iter__) {
        this._iter_ = _iter__;
    }

    public void setContML_(int _contML_) {
        this.contML_ = _contML_;
    }

    public void setContNotML_(int _contNotML_) {
        this.contNotML_ = _contNotML_;
    }

    public void setTimeSolution(double timeSolution_) {
        this.timeSolution = timeSolution_;
    }
}


//public class ABSample_traj_NextLevel : ABSingleton<ABSample_traj_NextLevel> {
public class ABSample_traj_NextLevel : MonoBehaviour {
    private static TextAsset graphModel;                                       // The trained TensorFlow graph
    private static TFGraph graph;
    private static TFSession session;

    public static int img_height = 480;
    public static int img_width = 840;
    public static int img_heightRNA = 106;      //132;   // 264;   // 300;
    public static int img_widthRNA = 159;         // 190;       // 237;    // 474;   // 500;
    public static int img_heightRNAFinal = 351;
    public static int img_widthRNABegin = 500;      // 224;
    public static int img_heightRNABeginl = 87;
    public static int img_widthRNAFinal = 697;

    //private static int numItetarionMax = 20;
    //private static int numItetarionMax = 70;
    private static int numItetarionMax = 39;
    //private static int numItetarionMax = 124;
    //private static int numItetarionMax = 127;
    //private static int numItetarionMax = 256;
    //private static int numItetarionMax = 1270;

    private List<ABBird> _birds;
    private List<ABPig> _pigs;
    private List<ABBlock> _blocks;
    //private List<GameObject> _plataforms;
    private List<ABTNT> _tnts;

    private bool beginEvaluateNivel;

    public static StreamWriter m_sw, m_sw1, m_sw2, m_sw3, m_sw4, m_sw5;

    private static float timeStartSearch = 0.0f;

    private static ABStateNivelUnity[] way;
    private static float[,] childernProbability_;
    private static float[,] childernProbabilityTime_;
    private static bool starBegin = true;
    private static bool starBegin_ML = true;
    //private static DefineLaunch[] angleAlredyChoice; //private static int[] angleAlredyChoice;
    private static int numMaxBirds;
    public static List<List<ABStateNivelUnity>> open;

    private static int contNotML, contML, trrt, rttr;
    public static int contSol, contNotSol, contInstable, contStable, iter;

    private static bool [] usedSpecialAttack;
    private static bool usedSpecialAttackUnity;

    private static bool entryFunctionInvokeSpecialAttack;

    //public double timeLaunchAbility_; //, __timeLaunchAbility_;

    private static List<PossibleSolution> solutions;

    private static ABBird birdLaunch;

    private static int posIncrement;

    private static bool contMLUsed;

    void Awake() {
        //Application.runInBackground = false;
        if (starBegin_ML) {
            starBegin_ML = false;

            //m_sw = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveisNotSolution_82_22.tx");
            //m_sw1 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapAngle_82_22.txt");
            //m_sw2 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapMatrices_82_22.tx");
            //m_sw3 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveis_82_22.txt");
            //m_sw4 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapBirds_82_22.txt");


            //graphModel = Resources.Load("Test/Models/WOBN-FC2L-Amsgrad-MA-SM-1_82-SM-2_22-K-OH-P-s-106x190x6-NVB-CA_iws-RANDOM-00001-05_0_ite_N") as TextAsset;

            //graphModel = Resources.Load("UFMG/Models/Small-WOBN-FC2L-Amsgrad-MA-SM-1_15-SM-2_5-K-OH-P-s-106x190x6-NVB-CA_iws-RANDOM-00001-05_0_ite_N") as TextAsset;

            graphModel = Resources.Load("Test/Models/WOBN-FC2L-MA-SM-1_82-SM-2_22-K-OH-P-s-106x159x6-NVB-CA_iws-RANDOM-00001-05_0_ite_N") as TextAsset;

            //graphModel = Resources.Load("UFMG/Models/small-AB_cnn_WOBN-FC2L-Amsgrad-MA-SM-1_82-SM-2_5-K-OH-P-s-106x159x6-NVB-CA_iws-RANDOM-00001-05_0_ite_N") as TextAsset;

            m_sw = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/est_82_22.txt");
            //m_sw.Write("\nsmall-AB_cnn_WOBN-FC2L-Amsgrad-MA-SM-1_82-SM-2_5-K-OH-P-s-106x158x6-NVB-CA_iws-RANDOM-00001-05_0_ite_N - 1Pig - 1 Bird - NotTNT\n");
            m_sw.Write("\nWOBN-FC2L-MA-SM-1_82-SM-2_22-K-OH-P-s-106x159x6-NVB-CA_iws-RANDOM-00001-05_0_ite_N\n");
            m_sw.Close();
            //graphModel = Resources.Load("UFMG/Models/frozen_TesteNovo") as TextAsset;


            graph = new TFGraph();
            graph.Import(graphModel.bytes);
            session = new TFSession(graph);

            contSol = contNotSol = contInstable = contStable = 0;

            /*if (LevelList.Instance.CurrentIndex == 228) {
                Debug.Log("0-Time.realtimeSinceStartup - (tempo até o click - \"até agora\") = " + Time.realtimeSinceStartup);
                //Debug.Log("0-Time game HUD= " + (Time.time - HUD.Instance.timeStartGameHUD));
                Debug.Log("0-ABLevel.Name = " + LevelList.Instance.GetCurrentLevel().name);
                Debug.Log("0-Quantidade de Níveis Jogados = " + LevelList.Instance.CurrentIndex);
                Debug.Log("---");
                HUD.Instance.ClickDown_1();
            }//*/

        }
        //Time.timeScale = 0.1f;
        //Time.timeScale = 1.5f;
        //Time.timeScale = 1.5f;
        //Time.timeScale = 2.1f;
        //if(iter > 21)
        //    Time.timeScale = 1.0f;
        //else
            Time.timeScale = 100.0f;
        //Time.timeScale = 10.0f;
        ABGameWorld.Instance._isSimulation = true;
    }

    // Use this for initialization
    void Start() {
        #if UNITY_ANDROID
            TensorFlowSharp.Android.NativeBinding.Init ();
        #endif

        beginEvaluateNivel = true;
    }

    public void creatListObjects() {
        _birds = ABGameWorld.Instance.get_bird();
        _blocks = ABGameWorld.Instance.get_blocks();
        _pigs = ABGameWorld.Instance.get_pigs();
        //_plataforms = ABGameWorld.Instance.get_plataforms();
        _tnts = ABGameWorld.Instance.get_tnts();

        /*for (int i = 0; i < _birds.Count; i++) {
            _birds[i].GetComponent<Renderer>().enabled = false;
        }
        for (int i = 0; i < _plataforms.Count; i++) {
            _plataforms[i].GetComponent<Renderer>().enabled = false;
        }
        for (int i = 0; i < _blocks.Count; i++) {
            _blocks[i].GetComponent<Renderer>().enabled = false;
        }
        for (int i = 0; i < _pigs.Count; i++) {
            _pigs[i].GetComponent<Renderer>().enabled = false;
        }
        for (int i = 0; i < _tnts.Count; i++) {
            _tnts[i].GetComponent<Renderer>().enabled = false;
        }//*/
    }

    //public ABStateNivelUnity_1 ABStateunitypertenceList(ref List<List<ABStateNivelUnity_1>> list, ref ABStateNivelUnity_1 statCurrent_) {
    //public ABStateNivelUnity ABStateunitypertenceList(ref List<List<ABStateNivelUnity>> list, ref DefineLaunch [] _way_, ref int deph_) {
    public ABStateNivelUnity ABStateunitypertenceList(ref int deph_) {
        //Debug.Log("ABStateunitypertenceList-0");
        /*Debug.Log("      deph_ = " + deph_);
        for (int rrr = 0; rrr < deph_; rrr++) {
            Debug.Log("         " + rrr + ": " + _way_[rrr]);
        }*/
        int contEquals;
        foreach (ABStateNivelUnity subList in open[(deph_ - 1)]) {
            //Debug.Log("ABStateunitypertenceList-1");
            contEquals = 0;
            for (int i = 0; i < deph_; i++) {
                //if ((statList.wayAngleOrFunction[i].get_angle() == _way_[i].get_angle()) && (statList.wayAngleOrFunction[i].get_timeAttackAbility() == _way_[i].get_timeAttackAbility())) {
                //if(subList.wayAngleOrFunction[i].SameObject(ref angleAlredyChoice[i])) {
                //if(subList.wayAngleOrFunction[i].SameObject(ref way[i].choice_.dLauch)) {
                if(subList.wayAngleOrFunction[i].SameObject(ref way[i].choice_.dLauch)) {
                    contEquals++;
                } else {
                    break;
                }
            }
            if (contEquals == deph_) {
                //Debug.Log("ABStateunitypertenceList-2");
                return subList;
            }
        }

        //Debug.Log("ABStateunitypertenceList-3");
        return null;
    }

    public static void salveDateEst() {
        m_sw5 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/est_82_22.txt");
        m_sw5.Write(LevelList.Instance.GetCurrentLevel().name + "    " + HUD.Instance.tempoAcelerado().ToString() + "    " + LevelList.Instance.CurrentIndex.ToString() + "    " + Time.realtimeSinceStartup.ToString() + "    " + contInstable + "    " + contSol + "    " + contNotSol + "\n");
        m_sw5.Close();
    }

    public void renderObjectsForTest() {
        foreach(ABBird abbird in _birds) {
            abbird.GetComponent<Renderer>().enabled = true;
        }

        foreach(ABBlock abblock in _blocks) {
            abblock.GetComponent<Renderer>().enabled = true;
        }

        foreach(ABTNT abt in _tnts) {
            abt.GetComponent<Renderer>().enabled = true;
        }

        foreach(ABPig abp in _pigs) {
            abp.GetComponent<Renderer>().enabled = true;
        }

        foreach(GameObject abplat in ABGameWorld.Instance._plataforms) {
            abplat.GetComponent<Renderer>().enabled = true;
        }
    }

    public void InvokeSpecialAttack() {
        //Debug.Log("1-time = " + Time.time);
        if ((birdLaunch != null) && birdLaunch.IsFlying) {
            //Debug.Log("InvokeSpecialAttack-0");
            birdLaunch.SendMessage("SpecialAttack", SendMessageOptions.DontRequireReceiver);
        } else{
            //Debug.Log("InvokeSpecialAttack-1");
            usedSpecialAttackUnity = false;
        }

        entryFunctionInvokeSpecialAttack = true;
    }

    /*private bool checkBirDieAPAGAR(int numBirdsBeforeLaunch, ref ABBird b) {
        //if (b == null)
        //    Debug.Log("B Riferente NULL");
        //Debug.Log("checkBirDieAPAGAR-0");
        if (_birds.Count < numBirdsBeforeLaunch) {
            //Debug.Log("checkBirDieAPAGAR-1");
            return true;
        }

        //Debug.Log("checkBirDieAPAGAR-2");

        return false;
    }//*/

    public void getBestSolution() {
        int largerPoint = -1, posLargePoint = -1;
        for (int i = 0; i < solutions.Count; i++)
            if (solutions[i].getPointSolution() > largerPoint) {
                largerPoint = solutions[i].getPointSolution();
                posLargePoint = i;
                //Debug.Log("Teste-1 ___ solutions[posLargePoint].getStatSize() = " + solutions[posLargePoint].getStatSize());
            }

        //Debug.Log("(" + 0 + ", " + solutions[posLargePoint].getStatUnity(0).choice_.dLauch.get_angle() + ") ");
        //Debug.Log("(" + 0 + ", " + solutions[posLargePoint].getStatUnity(0).choice_.dLauch.get_timeAttackAbility() + ") ");

        //solutions[posLargePoint].getStatUnity(0).printVectorBirds();

        m_sw1 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapAngle_82_22.txt");
  
        m_sw2 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapMatrices_82_22.txt");

        m_sw3 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveis_82_22.txt");
        m_sw3.Write(LevelList.Instance.GetCurrentLevel().name + "   " + (solutions[posLargePoint].get_iter_() + 1) + "   " + solutions[posLargePoint].getContML_() + "   " + solutions[posLargePoint].getContNotML_() +  "   " + solutions[posLargePoint].getTimeSolution() + "   " + solutions[posLargePoint].getPointSolution());
        m_sw3.Write("\n");
        m_sw3.Close();

        m_sw3 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapTimer_82_22.txt");

        m_sw4 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/BootstrapBirds_82_22.txt");

        for (int iterSolutuion = 0; iterSolutuion < solutions[posLargePoint].getStatSize(); iterSolutuion++) {;
            m_sw1.Write("(" + iterSolutuion + ", " + solutions[posLargePoint].getStatUnity(iterSolutuion).choice_.dLauch.get_angle() + ") ");
            //m_sw1.Write("\n");
            if(solutions[posLargePoint].getStatUnity(iterSolutuion).choice_.dLauch.get_usedSpecialAttackUnity())
                m_sw3.Write("(" + iterSolutuion + ", " + solutions[posLargePoint].getStatUnity(iterSolutuion).choice_.dLauch.get_timeAttackAbility() + ") ");
            else
                m_sw3.Write("(" + iterSolutuion + ", -1) ");
            //m_sw3.Write("\n");
            for (int k = 0; k < 6; k++) {
                for (int i = 0; i < img_heightRNA; i++) {
                    for (int j = 0; j < img_widthRNA; j++) {
                        m_sw2.Write((solutions[posLargePoint].getStatUnity(iterSolutuion).matrix[0, i, j, k]).ToString());
                        if (j < (img_widthRNA - 1)) {
                            m_sw2.Write(" ");
                        }
                    }
                    m_sw2.Write("\n");
                }
            }
            for (int j = 0; j < 10; j++) {
                for (int i = 0; i < 5; i++) {
                    m_sw4.Write(solutions[posLargePoint].getStatUnity(iterSolutuion).numBirds[0, j, i]);
                    m_sw4.Write(" ");
                }
                m_sw4.Write("\n");
            }
        }

        m_sw1.Write("\n");
        m_sw3.Write("\n");

        m_sw1.Close();
        m_sw2.Close();
        m_sw3.Close();
        m_sw4.Close();//*/
    }

    private IEnumerator Sample_traj(int nsism_) {
        int numBirdsBeforeLaunch;
        double timeNewLaunchBird;
        if (starBegin){
            if((LevelList.Instance.CurrentIndex % 100) == 0) {
                salveDateEst();
            
                /*m_sw.Flush();
                m_sw1.Flush();
                m_sw2.Flush();
                m_sw3.Flush();
                m_sw4.Flush();//*/

            }

            contML = 1;
            contNotML = -1;

            timeNewLaunchBird = Time.time; //variavel usada tbm pra ver instabilidade
            if (Time.timeScale == 100) {
                yield return new WaitForSeconds(0.2f);
            } else {
                yield return new WaitForSeconds(0.6f);
            }

            yield return new WaitUntil(() => (((_blocks.Count + _pigs.Count + _tnts.Count) != ABGameWorld.Instance.totalObjectsIniLevel) || (ABGameWorld.Instance.GetLevelStability() == 0)) || (Time.time > (timeNewLaunchBird + 25.0f)));

            float velocityLevel = ABGameWorld.Instance.GetLevelStability();

            if ((_blocks.Count + _pigs.Count + _tnts.Count) != ABGameWorld.Instance.totalObjectsIniLevel) {
                //Debug.Log("   ##Level Instavel --> " + LevelList.Instance.GetCurrentLevel().name + "   " + (_blocks.Count + _pigs.Count + _tnts.Count) + " != " + ABGameWorld.Instance.totalObjectsIniLevel);
                contInstable++;

                m_sw5 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveisNotSolutionInstable_82_22.txt");
                m_sw5.Write(LevelList.Instance.GetCurrentLevel().name + "   Instable   " + velocityLevel + "   " + (Time.time - timeNewLaunchBird) + "   " + (_blocks.Count + _pigs.Count + _tnts.Count) + " != " + ABGameWorld.Instance.totalObjectsIniLevel);
                m_sw5.Write("\n");
                m_sw5.Close();

                if (LevelList.Instance.LastNivel()) {
                    //m_sw5 = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/est_82_22.txt");
                    //m_sw5.Write(LevelList.Instance.GetCurrentLevel().name + "    " + HUD.Instance.tempoAcelerado().ToString() + "    " + LevelList.Instance.CurrentIndex.ToString() + "    " + Time.realtimeSinceStartup.ToString() + "    " + contInstable + "    " + contSol + "    " + contNotSol + "\n");
                    //m_sw5.Close();

                    salveDateEst();

                    Debug.Log("-FIM-simulacao");
                    Debug.Log("Last Level = " + LevelList.Instance.GetCurrentLevel().name);
                    Debug.Log("Tempos acelerado = " + HUD.Instance.tempoAcelerado().ToString());
                    Debug.Log("Total de Leveis simulados = " + LevelList.Instance.CurrentIndex.ToString());
                    Debug.Log("contInstable = " + contInstable);
                    Debug.Log("contStable = " + contStable);
                    Debug.Log("***Time.realtimeSinceStartup = " + Time.realtimeSinceStartup);
                    Time.timeScale = 1f;

                    /*m_sw.Close();
                    m_sw1.Close();
                    m_sw2.Close();
                    m_sw3.Close();
                    m_sw4.Close();//*/
                }

                _birds = null;
                _blocks = null;
                _pigs = null;
                ABGameWorld.Instance._plataforms = null;
                _tnts = null;

                ABGameWorld.Instance.NextLevel();
                yield break;
            } else {
                contStable++;
            }


            numMaxBirds = _birds.Count;
            way = null;
            way = new ABStateNivelUnity[numMaxBirds];
            open = new List<List<ABStateNivelUnity>>();
            //close = new List<List<ABStateNivelUnity_1>>();
            for (int i = 1; i < numMaxBirds; i++) {
                open.Add(new List<ABStateNivelUnity>());
                //close.Add(new List<ABStateNivelUnity_1>());
            }
            starBegin = false;
            timeStartSearch = Time.time;
            way[0] = new ABStateNivelUnity();

            usedSpecialAttack = new bool[numMaxBirds];

            solutions = null;
            solutions = new List<PossibleSolution>();

            way[0].probability = 1f;
            way[0].setHighTree(0, numMaxBirds);
            //Debug.Log("way[0].numTimesChoiceMax = " + way[0].numTimesChoiceMax);
            way[0].recortFrame = false;
            StartCoroutine(way[0].RecordFramePrint());     //way[0].RecordFramePrint();
            yield return new WaitUntil(() => (way[0].recortFrame));

            //childernProbability_ = Evaluate(ref way[0]);
            Evaluate_1(ref way[0]);

            //way[0].inicializaChildernProbability(ref childernProbability_, ref childernProbabilityTime_, ref _birds[0]);
            way[0].inicializaChildernProbability(ref childernProbability_, ref childernProbabilityTime_, _birds[0].typeBird);
            childernProbability_ = null;
            childernProbabilityTime_ = null;

            //for(int i = 0; i < 22; i++){
            //  Debug.Log("way[" + i + "] = " + way[0].statChildren[i].dLauch.get_timeAttackAbility());
            //}

            //angleAlredyChoice = new DefineLaunch[_birds.Count]; // new int[_birds.Count];

            iter = 0;
            trrt = rttr = 0;
        }
        //bool analitycMLTensorflow;
        //Debug.Log("iter = " + iter);
        for (int deph = 0; deph < numMaxBirds; deph++){      // for(int i = 0; i < _birds.Count; i++){
            contMLUsed = false;
            usedSpecialAttackUnity = true;
            //Debug.Log("usedSpecialAttackUnity = " + usedSpecialAttackUnity);
            entryFunctionInvokeSpecialAttack = false;
            //numFunction_mlChoice[deph] = way[deph].choiceNumFunction_ml();
            if (deph == 0) {
                if (way[deph].isStatChildrenEmpty()) {
                    //Debug.Log("iter = " + iter + ", trrt = " + trrt);
                    //Debug.Log("rttr = " + rttr);

                    //m_sw = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveisNotSolution.txt");

                    m_sw = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveisNotSolution_82_22.txt");
                    m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "    " + contML + "    " + contNotML + "    " + (Time.time - timeStartSearch) + " esgotado"); //m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "    " + (Time.time - timeStartSearch));
                    m_sw.Write("\n");
                    m_sw.Close();
                    //////////////////////////ABGameWorld.Instance.wonGame = -1;
                    starBegin = true;

                    if (LevelList.Instance.LastNivel()) {
                        salveDateEst();

                        Debug.Log("-FIM-simulacao");
                        Debug.Log("Last Level = " + LevelList.Instance.GetCurrentLevel().name);
                        Debug.Log("Tempos acelerado = " + HUD.Instance.tempoAcelerado().ToString());
                        Debug.Log("Total de Leveis simulados = " + LevelList.Instance.CurrentIndex.ToString());
                        Debug.Log("contInstable = " + contInstable);
                        Debug.Log("contStable = " + contStable);
                        Debug.Log("***Time.realtimeSinceStartup = " + Time.realtimeSinceStartup);
                        Time.timeScale = 1f;

                        /*m_sw.Close();
                        m_sw1.Close();
                        m_sw2.Close();
                        m_sw3.Close();
                        m_sw4.Close();//*/
                    }

                    _birds = null;
                    _blocks = null;
                    _pigs = null;
                    ABGameWorld.Instance._plataforms = null;
                    _tnts = null;
                    /*foreach (List<ABStateNivelUnity_1> subList in open) {
                        //foreach(ABStateNivelUnity_1 abstat in subList) {
                        //    Destroy(abstat);
                        //}
                    }//*/
                    if (open != null) {
                        for (int i = 0; i < open.Count; i++)
                            open[i] = null;
                        open = null;
                    }
                    way = null;
                    //angleAlredyChoice = null;

                    contNotSol++;

                    ABGameWorld.Instance.NextLevel();
                    yield break;
                }
                contNotML++;
            } else {
                //Debug.Log("dfsdfddddddddddddddddddddddddddddsdf");
                //way[deph] = ABStateunitypertenceList(ref open, ref angleAlredyChoice, ref deph);
                trrt++;
                way[deph] = ABStateunitypertenceList(ref deph);
                if (way[deph] == null) {
                    contMLUsed = true;
                    rttr++;
                    //Debug.Log("rttr = " + rttr);
                    contML++;
                    way[deph] = new ABStateNivelUnity();
                    way[deph].setHighTree(deph, numMaxBirds);
                    //Debug.Log("null-deph = " + deph + ", way[deph].numTimesChoiceMax = " + way[deph].numTimesChoiceMax);
                    way[deph].recortFrame = false;
                    StartCoroutine(way[deph].RecordFramePrint());
                    yield return new WaitUntil(() => (way[deph].recortFrame));

                    //////for (int i = 0; i < deph; i++) {
                    //////    way[deph].wayAngleOrFunction[i] = new DefineLaunch(ref angleAlredyChoice[i]);
                    //    Debug.Log("numFunction_mlChoice[" + i + "] = " + numFunction_mlChoice[i]);
                    //////}

                    for (int i = 0; i < deph; i++)
                        way[deph].wayAngleOrFunction[i] = new DefineLaunch(ref way[i].choice_.dLauch);


                    //childernProbability_ = Evaluate(ref way[deph]);
                    //childernProbability_ = Evaluate_1(ref way[deph]);
                    Evaluate_1(ref way[deph]);
                    //way[deph].inicializaChildernProbability(ref childernProbability_, ref childernProbabilityTime_);
                    way[deph].inicializaChildernProbability(ref childernProbability_, ref childernProbabilityTime_, _birds[0].typeBird);
                    childernProbability_ = null;
                    childernProbabilityTime_ = null;
                    //open[deph - 1].Add(way[deph]);
                } else {
                    //Debug.Log("contNotML = " + contNotML);
                    contNotML++;
                    //Debug.Log("notNULL-deph = " + deph + ", way[deph].numTimesChoiceMax = " + way[deph].numTimesChoiceMax);
                }
            }

            //Debug.Log("deph = " + deph);

            //posStateChilderMoreOne = way[deph].choiceAngle(ref angleAlredyChoice[deph]);
            //posStateChilderMoreOne = way[deph].choiceAngleSemProbability(ref angleAlredyChoice[deph]);
            //way[deph].incrementNumTimesChoice(ref posStateChilderMoreOne);
            //posIncrement = way[deph].choiceAngleSemProbability();
            //way[deph].incrementNumTimesChoice(ref posIncrement);

            way[deph].choiceAngleSemProbability();
            //way[deph].choiceAngleBacktrackingWithOutProBability();

            if (deph == (numMaxBirds - 1)) {
                way[deph].statChildren.Remove(way[deph].statChildren[way[deph].posChoice_]);
                for(int iIter = deph; iIter > 0; iIter--) {
                    if (way[iIter].isStatChildrenEmpty()) {
                        way[iIter - 1].statChildren.Remove(way[iIter - 1].statChildren[way[iIter - 1].posChoice_]);
                    } else {
                        break;
                    }
                }
            }

            //Debug.Log("way[" + deph + "].statChildren.Count = " + way[deph].statChildren.Count);

            //angleAlredyChoice[deph] = way[deph].choiceAngleBacktracking();

            //way[deph].statChildren[way[deph].returnChildrenStateInt(numFunction_mlChoice[deph])].angle = angleAlredyChoice[deph];

            //string rrrt = "";
            //for (int iii = 0; iii <= deph; iii++) {
            //    rrrt += numFunction_mlChoice[iii];
            //    rrrt += ",";
            //}
            //Debug.Log("rrrt = " + rrrt);

            yield return new WaitUntil(() => ((!_birds[0].OutOfSlingShot) && (!_birds[0].IsFlying) &&  _birds[0].getVelocityMagnitude() == 0));
            ///////////////////////////////////////yield return new WaitUntil(() => ((!_birds[0].OutOfSlingShot) && (!_birds[0].IsFlying) && (!_birds[0].alreadyShot) && _birds[0].getVelocityMagnitude() == 0));

            yield return new WaitForFixedUpdate();

            _birds[0].get_RigidBody().isKinematic = false;
            _birds[0].get_Collider().enabled = false;

            yield return new WaitForFixedUpdate();

            numBirdsBeforeLaunch = _birds.Count;
            birdLaunch = _birds[0];

            renderObjectsForTest();

            //birdLaunch.LaunchBird(angleAlredyChoice[deph].get_angle());
            birdLaunch.LaunchBird(way[deph].choice_.dLauch.get_angle());
            //birdLaunch.LaunchBird(29);
            //Debug.Log("0-time = " + Time.time + ", way[deph].choice_.dLauch.get_timeAttackAbility() = " + way[deph].choice_.dLauch.get_timeAttackAbility());
            //if (angleAlredyChoice[deph].get_timeAttackAbility() != -1)// {
            if (way[deph].choice_.dLauch.get_timeAttackAbility() != -1)// {
                Invoke("InvokeSpecialAttack", (way[deph].choice_.dLauch.get_timeAttackAbility() - 0.009f));
                //Invoke("InvokeSpecialAttack", (0.5f - 0.009f));
                //Invoke("InvokeSpecialAttack", 0.5f);
            //Invoke("InvokeSpecialAttack", (angleAlredyChoice[deph].get_timeAttackAbility() - 0.019f));
            //}
            //else// {
            //    way[deph].choice_.dLauch.set_usedSpecialAttackUnity(false);
            //    Debug.Log("BRAZEEEELLLLLL");
            //}


            //Debug.Log("Teste-1");
            //birdLaunch.alreadyShot = true;

            timeNewLaunchBird = Time.time;

            //_birds[0].LaunchBird(66f);
            //StartCoroutine(_birds[0].LaunchBird_(2.5f));
            //_birds[0].LaunchBird(14.0f);


            //__timeLaunchAbility_ = Time.time;

            //Invoke("InvokeSpecialAttack", angleAlredyChoice[deph].get_timeAttackAbility());


            yield return new WaitUntil(() => (((numBirdsBeforeLaunch > _birds.Count) && (ABGameWorld.Instance.IsLevelStableWithBirds() || (Time.time > (timeNewLaunchBird + 30.0f)))) || (Time.time > (timeNewLaunchBird + 90.0f))));
            //yield return new WaitUntil(() => ((_pigs.Count == 0) || ((numBirdsBeforeLaunch > _birds.Count) && (ABGameWorld.Instance.IsLevelStableWithBirds() || (Time.time > (timeNewLaunchBird + 30.0f))))));
            //yield return new WaitUntil(() => ((_pigs.Count == 0) || (checkBirDieAPAGAR(numBirdsBeforeLaunch, ref birdLaunch) && (ABGameWorld.Instance.IsLevelStableWithBirds() || (Time.time > (timeNewLaunchBird + 30.0f))))));

            //yield return new WaitUntil(() => (entryFunctionInvokeSpecialAttack || (angleAlredyChoice[deph].get_timeAttackAbility() == -1)));
            yield return new WaitUntil(() => (entryFunctionInvokeSpecialAttack || (way[deph].choice_.dLauch.get_timeAttackAbility() == -1)));

            if ((way[deph].choice_.dLauch.get_timeAttackAbility() != -1) && (!way[deph].choice_.dLauch.get_usedSpecialAttackUnity()))
                way[deph].RemoveSpecialAttackNeverUse();

            yield return new WaitForFixedUpdate();

            //usedSpecialAttack[deph] = usedSpecialAttackUnity;
            way[deph].choice_.dLauch.set_usedSpecialAttackUnity(usedSpecialAttackUnity);

            //Debug.Log("(" + way[deph].choice_.dLauch.get_angle() + ", " + way[deph].choice_.dLauch.get_timeAttackAbility() + ", " + way[deph].choice_.dLauch.get_usedSpecialAttackUnity() + ")");

            if ((deph > 0) && contMLUsed) {
                open[deph - 1].Add(way[deph]);
            }

            //Debug.Log("(" + way[deph].choice_.dLauch.get_angle()+ ", " + way[deph].choice_.dLauch.get_timeAttackAbility() + ", " + way[deph].choice_.dLauch.get_usedSpecialAttackUnity() + ")");

            //Debug.Log("Teste-2");

            //if (deph == 3) {
            //    Debug.Log("way[deph].statChildren.Count = " + way[deph].statChildren.Count);
            //    if (way[deph].statChildren.Count == 17) {
            //        Debug.Log("way[deph - 1].statChildren.Count = " + way[deph - 1].statChildren.Count);
            //    }
            //}


            //if((iter == 5) && (deph == (numMaxBirds - 1)))
            //  _pigs.Clear();
            if (_pigs.Count == 0) {
                //HUD.Instance.AddScore(10000);
                //HUD.Instance.AddScore((((uint)(_birds.Count)) * 10000));
                //Debug.Log("Pig == 0 --> score = " + HUD.Instance.GetScore());
                //PossibleSolution auxSol = new PossibleSolution();
                //PossibleSolution auxSol = new PossibleSolution(ref way, ref deph, 4444);
                PossibleSolution auxSol = new PossibleSolution();
                for (int i = 0; i <= deph; i++)
                    auxSol.setStateUnity(ref way[i]);
                //auxSol.setPointSolution((((int)HUD.Instance.GetScore()) + 10000 + (_birds.Count * 10000)));
                auxSol.setPointSolution(((int)HUD.Instance.GetScore() + 10000 + (_birds.Count * 10000)));
                auxSol.set_iter_(iter);
                auxSol.setContML_(contML);
                auxSol.setContNotML_(contNotML);
                auxSol.setTimeSolution((Time.time - timeStartSearch));
                solutions.Add(auxSol);
            }
        }
        iter++;
        //Debug.Log("iter = " + iter + ", way[0] = " + way[0].statChildren.Count );
        if (iter == nsism_) {
            //Debug.Log("PERDEUUUUU.... --> iter = " + iter + " contNotML " + contNotML + ", contML = " + contML);

            if (solutions.Count >= 1) {
                contSol++;
                //Debug.Log("contSol = " + contSol);
                getBestSolution();
            } else {
                contNotSol++;
                //m_sw = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveisNotSolution.txt");
                m_sw = File.AppendText(Application.dataPath + "/Resources/Test/bootstrap/NameNiveisNotSolution_82_22.txt");
                m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "    " + contML + "    " + contNotML + "    " + (Time.time - timeStartSearch));
                m_sw.Write("\n");
                m_sw.Close();
                //ABGameWorld.Instance.wonGame = -1;
            }
            //beginEvaluateNivel = true;

            starBegin = true;

            if (LevelList.Instance.LastNivel()) {
                salveDateEst();

                Debug.Log("-FIM-simulacao");
                Debug.Log("Last Level = " + LevelList.Instance.GetCurrentLevel().name);
                Debug.Log("Tempos acelerado = " + HUD.Instance.tempoAcelerado().ToString());
                Debug.Log("Total de Leveis simulados = " + LevelList.Instance.CurrentIndex.ToString());
                Debug.Log("contInstable = " + contInstable);
                Debug.Log("contStable = " + contStable);
                Debug.Log("***Time.realtimeSinceStartup = " + Time.realtimeSinceStartup);
                Time.timeScale = 1f;

                /*m_sw.Close();
                m_sw1.Close();
                m_sw2.Close();
                m_sw3.Close();
                m_sw4.Close();//*/
            }

            _birds = null;
            _blocks = null;
            _pigs = null;
            ABGameWorld.Instance._plataforms = null;
            _tnts = null;
            /*foreach (List<ABStateNivelUnity_1> subList in open) {
                        //foreach(ABStateNivelUnity_1 abstat in subList) {
                        //    Destroy(abstat);
                        //}
                    }//*/
            if (open != null) {
                for (int i = 0; i < open.Count; i++)
                    open[i] = null;
                open = null;
            }
            way = null;
            //angleAlredyChoice = null;

            ABGameWorld.Instance.NextLevel();
            yield break;
        }

        _birds = null;
        _blocks = null;
        _pigs = null;
        ABGameWorld.Instance._plataforms = null;
        _tnts = null;

        ABGameWorld.Instance.NextLevel(false);
        //yield break;
    }

    /*private float[,] Evaluate(ref ABStateNivelUnity statCurrent) {
        float[,] angleAndForce;

        using (var graph = new TFGraph()) {

            graphModel = Resources.Load("UFMG/Models/WOBN-FC2L-SM-K-OH-P-s-106x190x6-VB-CA_iws-150592+423029+475447_3_+_-1+1-_+_1_ite_LM") as TextAsset;
            //graphModel = Resources.Load("UFMG/Models/frozen_AB_CNNSM-K-OH-P-s-106x190-6chanels-VB-ComplAngles-iws-218456_1") as TextAsset;

            //print("graphModel.name = " + graphModel.name);

            graph.Import(graphModel.bytes);
            TFSession session = new TFSession(graph);
            var runner = session.GetRunner();

            TFTensor input_1 = statCurrent.matrix;
            TFTensor input_2 = statCurrent.numBirds;

            // Set up the input tensor and input
            runner.AddInput(graph["matricesOneHot6Chanels_input_"][0], input_1)
                  .AddInput(graph["vectorNumBirds_input_"][0], input_2)
                  .Fetch(graph["dense_2/Softmax"][0]);

            angleAndForce = runner.Run()[0].GetValue() as float[,];

            session.Dispose();
            graph.Dispose();
        }

        return angleAndForce;
    }//*/

    private void Evaluate_1(ref ABStateNivelUnity statCurrent) {
        var runner = session.GetRunner();

        TFTensor input_1 = statCurrent.matrix;
        TFTensor input_2 = statCurrent.numBirds;

        // Set up the input tensor and input
        runner.AddInput(graph["matricesOneHot6Chanels_input_"][0], input_1)
                  .AddInput(graph["vectorNumBirds_input_"][0], input_2)
                  .Fetch(graph["pred_AngleOrFunction/Softmax"][0], graph["pred_especialAblity/Softmax"][0]);
        //runner.AddInput(graph["matricesOneHot6Chanels_input_"][0], input_1)
        //          .AddInput(graph["vectorNumBirds_input_"][0], input_2)
        //          .Fetch(graph["pred_AngleOrFunction/Softmax"][0], graph["pred_especialAblity/Softmax"][0]);
        var output = runner.Run();
        childernProbability_ = (float[,])output[0].GetValue();
        childernProbabilityTime_ = (float[,])output[1].GetValue();
        //angleAndForce = runner.Run()[0].GetValue() as float[,];
        //var fdsfsd = runner.Run()[0].GetValue() as float[,];
        runner = null;

        //float somaessaporraaedoidao = 0;
        //Debug.Log("childernProbabilityTime_ = " + childernProbabilityTime_);
        //for (int i = 0; i < 5; i++) {
            //Debug.Log(i + "-sssssss = " + childernProbabilityTime_[0, i]);
        //    somaessaporraaedoidao += childernProbabilityTime_[0, i];
        //}
        //Debug.Log("somaessaporraaedoidao = " + somaessaporraaedoidao);
        //return angleAndForce;
    }

    /*private float[,] Evaluate_1(ref ABStateNivelUnity statCurrent) {
        float[,] angleAndForce;

        var runner = session.GetRunner();

        TFTensor input_1 = statCurrent.matrix;
        TFTensor input_2 = statCurrent.numBirds;

        // Set up the input tensor and input
        runner.AddInput(graph["matricesOneHot6Chanels_input_"][0], input_1)
                .AddInput(graph["vectorNumBirds_input_"][0], input_2)
                .Fetch(graph["dense_2/Softmax"][0]);

        angleAndForce = runner.Run()[0].GetValue() as float[,];

        runner = null;

        return angleAndForce;
    }//*/

    void FixedUpdate() {
        //if (beginEvaluateNivel && (ABGameWorld.Instance.levelStable) && (ABGameWorld.Instance.getWonGame() == 0)) {
        ////////////////////////////////////////if (beginEvaluateNivel && (ABGameWorld.Instance.getWonGame() == 0)) {
        if (beginEvaluateNivel) {
            beginEvaluateNivel = false;
            creatListObjects();
            StartCoroutine(Sample_traj(numItetarionMax));
        }
    }

    // Update is called once per frame
    //void Update() {
        //Time.timeScale = tiime;
    //}
}