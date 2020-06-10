using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

//using TensorFlow;
//using UnityEngine.UI;

public class ABNaiveAgente : MonoBehaviour {
    private List<ABPig> _pigs;
    private List<ABBird> _birds;

    private static int numItetarionMax = 20;

    private static float timeStartSearch = 0.0f;

    private static bool starBegin = true;

    public static StreamWriter m_sw;

    public static int contSol, contNotSol;
    
    private static int iter;

    private static int numMaxBirds;

    private static ABStateNivelUnity_Naive[] way;

    private static int[] numFunction_mlChoice;

    private bool beginEvaluateNivel;

    public static List<List<ABStateNivelUnity_Naive>> open;
    //public static List<List<ABStateNivelUnity_Naive>> close;


    void Awake() {
        //Time.timeScale = 1.0f;
        //Time.timeScale = 1.5f;
        //Time.timeScale = 2.1f;
        Time.timeScale = 100.0f;

        ABGameWorld.Instance._isSimulation = true;
    }

	// Use this for initialization
    void Start() {
        #if UNITY_ANDROID
            TensorFlowSharp.Android.NativeBinding.Init ();
        #endif
        _pigs = ABGameWorld.Instance.get_pigs();
        _birds = ABGameWorld.Instance.get_bird();

        m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/est_Naive.txt");
        m_sw.Write("\nNaiveAgente\n");
        m_sw.Close();

        beginEvaluateNivel = true;
    }

    private void updateTimesChoice_() {
        way[numMaxBirds - 1].statChildren.Remove(way[numMaxBirds - 1].childrenChoice);
        for (int i = (numMaxBirds - 1); i > 0; i--) {
            if (way[i].statChildren.Count == 0) {
                way[i - 1].statChildren.Remove(way[i - 1].childrenChoice);
            }
        }
    }

    void renderObjects() {
        for (int i = 0; i < ABGameWorld.Instance._birds.Count; i++) {
            _birds[i].GetComponent<Renderer>().enabled = true;
        }

        for (int i = 0; i < ABGameWorld.Instance._pigs.Count; i++) {
            _pigs[i].GetComponent<Renderer>().enabled = true;
        }

        for (int i = 0; i < ABGameWorld.Instance._blocks.Count; i++) {
            ABGameWorld.Instance._blocks[i].GetComponent<Renderer>().enabled = true;
        }

        for (int i = 0; i < ABGameWorld.Instance._tnts.Count; i++) {
            ABGameWorld.Instance._tnts[i].GetComponent<Renderer>().enabled = true;
        }

        for (int i = 0; i < ABGameWorld.Instance._plataforms.Count; i++) {
            ABGameWorld.Instance._plataforms[i].GetComponent<Renderer>().enabled = true;
        }
    }

	// Update is called once per frame
	//void Update () {
		
	//}
    
    public ABStateNivelUnity_Naive ABStateunitypertenceList(ref List<List<ABStateNivelUnity_Naive>> list, ref int [] _way_, ref int deph_) {
        int contEquals;
        foreach (ABStateNivelUnity_Naive statList in list[(deph_ - 1)]) {
            contEquals = 0;
            for (int i = 0; i < deph_; i++) {
                if (statList.wayAngleOrFunction[i] == _way_[i])
                    contEquals++;
                else
                    break;
            }
            if (contEquals == deph_)
                return statList;
        }
        return null;
    }

    /*public bool ABStateunitypertenceListClose(ref uint [] _way_, ref int deph_) {
        int contEquals;
        foreach (ABStateNivelUnity_Naive statList in close[(deph_ - 1)]) {
            contEquals = 0;
            for (int i = 0; i < deph_; i++) {
                if (statList.wayAngleOrFunction[i] == _way_[i]) {
                    contEquals++;
                } else {
                    break;
                }
            }
            if (contEquals == deph_) {
                return true;
            }
        }
        return false;
    }//*/

    public static void salveDateEst() {
        m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/est_Naive.txt");
        m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + HUD.Instance.tempoAcelerado() + "   " + LevelList.Instance.CurrentIndex + "   " + Time.realtimeSinceStartup + "   " + contSol + "   " + contNotSol + "\n");
        //m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + HUD.Instance.timeAceleradoUnity() + "   " + LevelList.Instance.CurrentIndex + "   " + Time.realtimeSinceStartup + "   " + contInstable + "   " + (LevelList.Instance.CurrentIndex - contInstable) + "   " + contSol + "   " + contNotSol + "\n");
        m_sw.Close();
    }

    private IEnumerator Sample_traj(int nsism_) {
        int numBirdsBeforeLaunch;
        double timeNewLaunchBird;
        if (starBegin){
            starBegin = false;
            timeStartSearch = Time.time;
            if((LevelList.Instance.CurrentIndex % 100) == 0) {
                salveDateEst();
            }

            timeNewLaunchBird = Time.time; //variavel usada tbm pra ver instabilidadev
            timeStartSearch = Time.time;

            numMaxBirds = _birds.Count;

            way = null;
            way = new ABStateNivelUnity_Naive[numMaxBirds];

            numFunction_mlChoice = new int[numMaxBirds];

            open = new List<List<ABStateNivelUnity_Naive>>();
            for (int i = 1; i < numMaxBirds; i++) {
                open.Add(new List<ABStateNivelUnity_Naive>());
            }

            timeStartSearch = Time.time;
            way[0] = new ABStateNivelUnity_Naive();

            //way[0].recortFrame = false;
            //StartCoroutine(way[0].RecordFramePrint());     //way[0].RecordFramePrint();
            //yield return new WaitUntil(() => (way[0].recortFrame));

            //Debug.Log("_pigs.Cout = " + _pigs.Count);
            foreach(ABPig p in _pigs) {
                if (!way[0].addChildenAngleLaunch(p.transform.position)){
                    m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/NameLevelNotServer_NaiveAgente.txt");
                    m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "   " + (Time.time - timeStartSearch) + "   Double.IsNaN" + "   0\n");

                    m_sw.Close();

                    /////////////////////////////ABGameWorld.Instance.wonGame = -1;
                    starBegin = true;

                    _birds = null;
                    _pigs = null;
                    if (open != null) {
                        for (int i = 0; i < open.Count; i++)
                            open[i] = null;
                        open = null;
                    }
                    way = null;
                    numFunction_mlChoice = null;

                    ABGameWorld.Instance.NextLevel();
                    yield break;
                }
            }

            way[0].setHighTree(0, numMaxBirds);

            //Debug.Log("way[0].numTimesChoiceMax = " + way[0].numTimesChoiceMax);
            //Debug.Log("way[0].statChildren.Count = " + way[0].statChildren.Count);

            iter = 0;
        }
        for (int deph = 0; deph < numMaxBirds; deph++){      // for(int i = 0; i < _birds.Count; i++){
            if (deph == 0) {
                if (way[deph].isStatChildrenEmpty()) {
                    m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/NameNiveisNotSolution_Naive.txt");
                    m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "   " + (Time.time - timeStartSearch) + "   esgt");
                    m_sw.Write("\n");
                    m_sw.Close();
                    /////////////////////////////ABGameWorld.Instance.wonGame = -1;
                    starBegin = true;

                    if (LevelList.Instance.LastNivel()) {
                        m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/est_Naive.txt");

                        m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + HUD.Instance.tempoAcelerado() + "   " + (LevelList.Instance.CurrentIndex + 1) + "   " + Time.realtimeSinceStartup + "   " + contSol + "   " + contNotSol + "\n");
                        m_sw.Close();

                        Debug.Log("-FIM-simulacao");
                        Debug.Log("Last Level = " + LevelList.Instance.GetCurrentLevel().name);
                        Debug.Log("Tempos acelerado = " + HUD.Instance.tempoAcelerado());
                        Debug.Log("Total de Leveis simulados = " + (LevelList.Instance.CurrentIndex + 1));
                        Debug.Log("contSolution = " + contSol);
                        Debug.Log("contNOTSolution = " + contNotSol);
                        Debug.Log("***Time.realtimeSinceStartup = " + Time.realtimeSinceStartup);
                        Time.timeScale = 1f;

                    }

                    _birds = null;
                    _pigs = null;
                    if (open != null) {
                        for (int i = 0; i < open.Count; i++)
                            open[i] = null;
                        open = null;
                    }
                    way = null;
                    numFunction_mlChoice = null;

                    ABGameWorld.Instance.NextLevel();
                    yield break;
                }
            } else{
                 way[deph] = ABStateunitypertenceList(ref open, ref numFunction_mlChoice, ref deph);
                //if ((way[deph] == null) || (way[deph].isStatChildrenEmpty)) {
                if(way[deph] == null) {
                    way[deph] = new ABStateNivelUnity_Naive();

                    //Debug.Log("null-deph = " + deph + ", way[deph].numTimesChoiceMax = " + way[deph].numTimesChoiceMax);
                    //way[deph].recortFrame = false;
                    //StartCoroutine(way[deph].RecordFramePrint());
                    //yield return new WaitUntil(() => (way[deph].recortFrame));

                    foreach(ABPig p in _pigs) {
                        if (!way[deph].addChildenAngleLaunch(p.transform.position)){
                            m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/NameLevelNotServer_NaiveAgente.txt");
                            m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "   " + (Time.time - timeStartSearch) + "   Double.IsNaN" + "   " + deph);
                            m_sw.Write("\n");
                            m_sw.Close();
                            ABGameWorld.Instance.NextLevel();
                            yield break;
                        }
                    }
                    way[deph].setHighTree(deph, numMaxBirds);

                    for (int i = 0; i < deph; i++) {
                        way[deph].wayAngleOrFunction[i] = numFunction_mlChoice[i];
                    }

                    //Debug.Log("1-way[" + deph + "].numTimesChoiceMax = " + way[deph].numTimesChoiceMax);
                    //Debug.Log("way["+ deph + "].statChildren.Count = " + way[deph].statChildren.Count);

                    open[deph - 1].Add(way[deph]);
                } 
            }

            way[deph].ChildrenState_Naive_ml();
            numFunction_mlChoice[deph] = way[deph].childrenChoice.numFunction_ml;

            if (deph == (numMaxBirds - 1)) {
                //updateTimesChoice();
                updateTimesChoice_();
            }

            //this.renderObjects();

            /////////////////////////////yield return new WaitUntil(() => ((!_birds[0].OutOfSlingShot) && (!_birds[0].IsFlying) && (!_birds[0].alreadyShot) && _birds[0].getVelocityMagnitude() == 0));
            yield return new WaitUntil(() => ((!_birds[0].OutOfSlingShot) && (!_birds[0].IsFlying) && _birds[0].getVelocityMagnitude() == 0));

            yield return new WaitForFixedUpdate();

            _birds[0].get_RigidBody().isKinematic = false;
            _birds[0].get_Collider().enabled = false;

            yield return new WaitForFixedUpdate();

            numBirdsBeforeLaunch = _birds.Count;

            _birds[0].LaunchBird(way[deph].childrenChoice.angle);

            /////////////////////////////_birds[0].alreadyShot = true;

            timeNewLaunchBird = Time.time;

            yield return new WaitUntil(() => ((_pigs.Count == 0) || ((numBirdsBeforeLaunch > _birds.Count) && (ABGameWorld.Instance.IsLevelStableWithBirds() || (Time.time > (timeNewLaunchBird + 55.0f))))));

            yield return new WaitForFixedUpdate();

            if (_pigs.Count == 0) {
                beginEvaluateNivel = true;
                m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/NameNiveisSolution_Naive.txt");

                m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "   " + (Time.time - timeStartSearch));
                m_sw.Write("\n");
                m_sw.Close();

                m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/BootstrapAngle_Naive.txt");

                for (int iterSolutuion = 0; iterSolutuion <= deph; iterSolutuion++) {
                    m_sw.Write("(" + iterSolutuion + ", " + way[iterSolutuion].childrenChoice.angle + ", " + way[iterSolutuion].childrenChoice.numFunction_ml + ") ");
                }

                m_sw.Write("  \n");
                m_sw.Close();//*/

                if (LevelList.Instance.LastNivel()) {
                    salveDateEst();

                    Debug.Log("-FIM-simulacao");
                    Debug.Log("Last Level = " + LevelList.Instance.GetCurrentLevel().name);
                    Debug.Log("Tempos acelerado = " + HUD.Instance.tempoAcelerado());
                    Debug.Log("Total de Leveis simulados = " + (LevelList.Instance.CurrentIndex + 1));
                    Debug.Log("contSolution = " + contSol);
                    Debug.Log("contNOTSolution = " + contNotSol);
                    Debug.Log("***Time.realtimeSinceStartup = " + Time.realtimeSinceStartup);
                    Time.timeScale = 1f;

                }

                //////////////////////////ABGameWorld.Instance.wonGame = 1;
                starBegin = true;

                _birds = null;
                _pigs = null;
                if (open != null) {
                    for (int i = 0; i < open.Count; i++)
                        open[i] = null;
                    open = null;
                }
                way = null;
                numFunction_mlChoice = null;

                contSol++;

                ABGameWorld.Instance.NextLevel();
                yield break;
            }
        }
        iter++;
        if (iter == nsism_) {
            beginEvaluateNivel = true;
            m_sw = File.AppendText("C:/Users/esgot/Documents/ExperimentsUFMG/Dados_Pesquisa-Angry_birds/124/newCompere/Naive/NameNiveisNotSolution_Naive.txt");
            m_sw.Write(LevelList.Instance.GetCurrentLevel().name + "   " + iter + "   " + (Time.time - timeStartSearch));
            m_sw.Write("\n");
            m_sw.Close();
            //////////////////////////ABGameWorld.Instance.wonGame = -1;
            starBegin = true;

            if (LevelList.Instance.LastNivel()) {
                salveDateEst();
                Debug.Log("-FIM-simulacao");
                Debug.Log("Last Level = " + LevelList.Instance.GetCurrentLevel().name);
                Debug.Log("Tempos acelerado = " + HUD.Instance.tempoAcelerado());
                Debug.Log("Total de Leveis simulados = " + (LevelList.Instance.CurrentIndex + 1));
                //Debug.Log("contInstable = " + contInstable);
                //Debug.Log("contStable = " + ((LevelList.Instance.CurrentIndex + 1) - contInstable));
                Debug.Log("contSolution = " + contSol);
                Debug.Log("contNOTSolution = " + contNotSol);
                Debug.Log("***Time.realtimeSinceStartup = " + Time.realtimeSinceStartup);
                Time.timeScale = 1f;
            }

            _birds = null;
            _pigs = null;
            if (open != null) {
                for (int i = 0; i < open.Count; i++)
                    open[i] = null;
                open = null;
            }
            way = null;
            numFunction_mlChoice = null;

                contNotSol++;

            ABGameWorld.Instance.NextLevel();
            yield break;
        }

        _birds = null;
        _pigs = null;

        //_plataforms = null;
        //_tnts = null;

        //Debug.Log("iter  = "+ iter);

        ABGameWorld.Instance.NextLevel(false);
        //yield break;
    }


    void FixedUpdate() {
        //if (beginEvaluateNivel && (ABGameWorld.Instance.levelStable) && (ABGameWorld.Instance.getWonGame() == 0)) {
        ////////////////////////////////////////if (beginEvaluateNivel && (ABGameWorld.Instance.getWonGame() == 0)) {
        if (beginEvaluateNivel) {
        beginEvaluateNivel = false;
            StartCoroutine(Sample_traj(numItetarionMax));
        }
    }

}
