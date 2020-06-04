using UnityEngine;
using System.Collections.Generic;
using System;
//using System.IO;
//using System.Collections;

//using TensorFlow;
//using UnityEngine.UI;

//public class Function_ML : MonoBehaviour {
public class Function_ML {
    private const float velocidade = 10.0f;//9.68f;
    private const double x1 = -8.15f, y1 = -1.7f;
    private static readonly float sqr2Velociade = Mathf.Pow(velocidade, 2.0f), sqr4Velociade = Mathf.Pow(velocidade, 4.0f);

    public bool canFindBiggerStructure;
    //int numFuntion;
    //double angleLaunchBiggerStructure;

    //private double angleLaunch(Transform target, bool hight) { //função que faz o calculo do ângulo dado a posição do pássado, objeto alvo, força de lancamento e gravidade. Nesta Implementação é retornado apenas um dos dois ângulos possíveis ,sendo controlado pelo parâmetro hight (alto == true ou baixo == false).
    private double angleLaunch(Vector2 target, bool hight) {
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

        double resp = -100;
        if (hight) {
            resp = sqr2Velociade + aux;
            resp /= denominador;

            resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;
        } else {
            resp = sqr2Velociade - aux;
            resp /= denominador;

            resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;
        }
        if (!Double.IsNaN(resp)) {
            return resp;
        } else {
            return 45.0f;
        }
    }

    public double highHitLeftMostpig() { // Função que escolhe o porco mais a esquerda do nível, com ângulo de lançamento alto.
        float positionLeftMostPig = 100;
        //Transform leftMostPig = new transform();
        //GameObject leftMostPig;
        Vector2 leftMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x < positionLeftMostPig) {
                    positionLeftMostPig = b.transform.position.x;
                    //leftMostPig = b;
                    //leftMostPig.transform.
                    leftMostPig = b.position;
                }
            }
        }

        return angleLaunch(leftMostPig, true);
    }

    public double lowHitLeftMostpig() { // Função que escolhe o porco mais a esquerda do nível, com ângulo de lançamento baixo.
        float positionLeftMostPig = 100;
        //Transform leftMostPig = new transform();
        //GameObject leftMostPig;
        Vector2 leftMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x < positionLeftMostPig) {
                    positionLeftMostPig = b.transform.position.x;
                    //leftMostPig = b;
                    //leftMostPig.transform.
                    leftMostPig = b.position;
                }
            }
        }

        return angleLaunch(leftMostPig, false);
    }

    public double highHitRightMostpig() { // Função que escolhe o porco mais a direita do nível, com ângulo de lançamento alto.
        float positionRightMostPig = -100;
        //Transform rightMostPig = transform;
        Vector2 rightMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x > positionRightMostPig) {
                    positionRightMostPig = b.transform.position.x;
                    //rightMostPig = b;
                    rightMostPig = b.position;
                }
            }
        }

        return angleLaunch(rightMostPig, true);
    }


    public double lowHitRightMostpig() { // Função que escolhe o porco mais a direita do nível, com ângulo de lançamento baixo.
        float positionRightMostPig = -100;
        //Transform rightMostPig = transform;
        Vector2 rightMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x > positionRightMostPig) {
                    positionRightMostPig = b.transform.position.x;
                    //rightMostPig = b;
                    rightMostPig = b.position;
                }
            }
        }

        return angleLaunch(rightMostPig, false);
    }

    public double highHitHightMostpig() { // Função que escolhe o porco mais alto do nível, com ângulo de lançamento alto.
        float positionHightMostPig = -100;
        //Transform hightMostPig = transform;
        Vector2 hightMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)) > positionHightMostPig))) {
                positionHightMostPig = b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f);
                //hightMostPig = b;
                hightMostPig = b.position;
            }
        }

        return angleLaunch(hightMostPig, true);
    }

    public double lowHitHightMostpig() { // Função que escolhe o porco mais alto do nível, com ângulo de lançamento baixo.
        float positionHightMostPig = -100;
        //Transform hightMostPig = transform;
        Vector2 hightMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)) > positionHightMostPig))) {
                positionHightMostPig = b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f);
                //hightMostPig = b;
                hightMostPig = b.position;
            }
        }

        return angleLaunch(hightMostPig, false);
    }

    public double highHitLowerMostpig() {  // Função que escolhe o porco mais baixo do nível, com ângulo de lançamento alto.
        float positionLowerMostPig = 100;
        //Transform lowerMostPig = transform;
        Vector2 lowerMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (((b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)) < positionLowerMostPig))) {
                positionLowerMostPig = (b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f));
                lowerMostPig = b.position;
            }
        }

        return angleLaunch(lowerMostPig, true);
    }

    public double lowHitLowerMostpig() {  // Função que escolhe o porco mais baixo do nível, com ângulo de lançamento baixo.
        float positionLowerMostPig = 100;
        //Transform lowerMostPig = transform;
        Vector2 lowerMostPig = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (((b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)) < positionLowerMostPig))) {
                positionLowerMostPig = (b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f));
                lowerMostPig = b.position;
            }
        }

        return angleLaunch(lowerMostPig, false);
    }

    public double highHitPigLessLife() {  //Função que escolhe o porco com menos vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o porco mais alto.
        float life = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.GetComponent<ABPig>()._currentLife < life) {
                    life = b.GetComponent<ABPig>()._currentLife;
                }
            }
        }

        Vector2 PigLessLife = new Vector2();
        double sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) { 
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life) && (sizeY < Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero))) {
                sizeY = Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                PigLessLife = b.position;
            }
        }

        return angleLaunch(PigLessLife, true);
    }

    public double lowHitPigLessLife() {  //Função que escolhe o porco com menos vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o porco mais alto.
        float life = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.GetComponent<ABPig>()._currentLife < life) {
                    life = b.GetComponent<ABPig>()._currentLife;
                }
            }
        }

        Vector2 PigLessLife = new Vector2();
        double sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) { 
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life) && (sizeY < Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero))) {
                sizeY = Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                PigLessLife = b.position;
            }
        }

        return angleLaunch(PigLessLife, false);
    }

    public double highHitPigMoreLife() {  // Função que escolhe o porco com mais vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o porco mais alto.
        float life = -100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (b.GetComponent<ABPig>()._currentLife > life)) {
                life = b.GetComponent<ABPig>()._currentLife;
            }
        }

        Vector2 PigMoreLife = new Vector2();
        double sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life) && (sizeY < Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero))) {
                sizeY = Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                PigMoreLife = b.position;
            }
        }

        return angleLaunch(PigMoreLife, true);
    }

    public double lowHitPigMoreLife() {  //Função que escolhe o porco com mais vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o porco mais alto.
        float life = -100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (b.GetComponent<ABPig>()._currentLife > life)) {
                life = b.GetComponent<ABPig>()._currentLife;
            }
        }

        Vector2 PigMoreLife = new Vector2();
        double sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life) && (sizeY < Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero))) {
                sizeY = Math.Round((b.transform.position.y + (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                PigMoreLife = b.position;
            }
        }

        return angleLaunch(PigMoreLife, false);
    }

    public double highHitBlockLessLife() {  //Função que escolhe o bloco com menos vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o bloco mais baixo. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo alto(highHitLeftMostpig)
        float lifeBlock = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform)
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && (b.GetComponent<ABBlock>()._currentLife < lifeBlock))
                lifeBlock = b.GetComponent<ABBlock>()._currentLife;

        if (lifeBlock == 100) {
            return highHitLeftMostpig();
        } else {
            Vector2 blockLessLife = new Vector2();
            double sizeY = 100.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife == lifeBlock) && (sizeY > Math.Round((b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero))) {
                    sizeY = Math.Round((b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                    blockLessLife = b.position;
                }
            }

            return angleLaunch(blockLessLife, true);
        }
    }

    public double lowHitBlockLessLife() {  //Função que escolhe o bloco com menos vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o bloco mais baixo. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo baixo (lowHitLeftMostpig)
        float lifeBlock = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform)
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && (b.GetComponent<ABBlock>()._currentLife < lifeBlock))
                lifeBlock = b.GetComponent<ABBlock>()._currentLife;

        if (lifeBlock == 100) {
            return lowHitLeftMostpig();
        } else {
            Vector2 blockLessLife = new Vector2();
            double sizeY = 100.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife == lifeBlock) && (sizeY > Math.Round((b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero))) {
                    sizeY = Math.Round((b.transform.position.y - (b.GetComponent<Collider2D>().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                    blockLessLife = b.position;
                }
            }

            return angleLaunch(blockLessLife, true);
        }
    }



    public double highHitBlockMoreLife() {  // Função que escolhe o bloco com mais vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o maior bloco em Y. Caso exista 2 ou mais blocos com a mesma vida e tamanho em Y, escolher aquele mais proximo de um porco. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo alto (highHitLeftMostpig)
        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
        float lifeBlock = -100f;
        foreach (ABBlock b in blocks_) {
            if (b._currentLife > lifeBlock) {
                lifeBlock = b._currentLife;
            }
        }
        if (lifeBlock == -100.0f) {
            return highHitLeftMostpig();
        } else {
            List<int> blocksMoreLife = new List<int>();
            double sizeY = -1.0f;
            for (int i = 0; i < blocks_.Count; i++) {
                if (blocks_[i]._currentLife == lifeBlock) {
                    blocksMoreLife.Add(i);
                    if (sizeY < Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero)) {
                        sizeY = Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero);
                    }
                }
            }

            if (blocksMoreLife.Count == 1) {
                return angleLaunch(blocks_[blocksMoreLife[0]].transform.position, true);
            } else {
                List<int> removeElmenteTheblocksMoreLife = new List<int>();
                foreach (int i in blocksMoreLife) {
                    if (sizeY != Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero)) {
                        removeElmenteTheblocksMoreLife.Add(i);
                    }
                }
                foreach (int i in removeElmenteTheblocksMoreLife) {
                    blocksMoreLife.Remove(i);
                }
               
                if (blocksMoreLife.Count == 1) {
                    return angleLaunch(blocks_[blocksMoreLife[0]].transform.position, true);
                } else {
                    double blockMoreTallest = -1.0f;
                    foreach (int i in blocksMoreLife) {
                        if (blockMoreTallest < Math.Round((blocks_[i].transform.position.y + (blocks_[i].get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero)) {
                            blockMoreTallest = Math.Round((blocks_[i].transform.position.y + (blocks_[i].get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                        }
                    }
                    removeElmenteTheblocksMoreLife.Clear();
                    //removeElmenteTheblocksMoreLife = new List<int>();
                    for (int i = 0; i < blocksMoreLife.Count; i++) {
                        if(blockMoreTallest != Math.Round((blocks_[blocksMoreLife[i]].transform.position.y + (blocks_[blocksMoreLife[i]].get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero)) {
                            removeElmenteTheblocksMoreLife.Add(blocksMoreLife[i]);
                        }
                    }

                    foreach(int i in removeElmenteTheblocksMoreLife) {
                        blocksMoreLife.Remove(i);
                    }

                    if (blocksMoreLife.Count == 1) {
                        return angleLaunch(blocks_[blocksMoreLife[0]].transform.position, true);
                    } else {
                        List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
                        float LessDistance = 300.0f, auxDistance;
                        Vector2 blockLaunch = new Vector2();
                        foreach (int i in blocksMoreLife) {
                            foreach(ABPig p in pigs_){
                                if (p._currentLife > 0) {
                                    auxDistance = Mathf.Sqrt(Mathf.Pow((blocks_[i].transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((blocks_[i].transform.position.y - p.transform.position.y), 2));
                                    if (LessDistance > auxDistance) {
                                        LessDistance = auxDistance;
                                        blockLaunch = blocks_[i].transform.position;
                                    }
                                }
                            }
                        }

                        return angleLaunch(blockLaunch, true);
                    }
                }
            }
        }
    }

    public double lowHitBlockMoreLife() {  // Função que escolhe o bloco com mais vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o maior bloco em Y. Caso exista 2 ou mais blocos com a mesma vida e tamanho em Y, escolher aquele mais próximo de um porco. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo baixo (lowHitLeftMostpig)
        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
        float lifeBlock = -100f;
        foreach (ABBlock b in blocks_) {
            if (b._currentLife > lifeBlock) {
                lifeBlock = b._currentLife;
            }
        }
        if (lifeBlock == -100.0f) {
            return lowHitLeftMostpig();
        } else {
            List<int> blocksMoreLife = new List<int>();
            double sizeY = -1.0f;
            for (int i = 0; i < blocks_.Count; i++) {
                if (blocks_[i]._currentLife == lifeBlock) {
                    blocksMoreLife.Add(i);
                    if (sizeY < Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero)) {
                        sizeY = Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero);
                    }
                }
            }

            if (blocksMoreLife.Count == 1) {
                return angleLaunch(blocks_[blocksMoreLife[0]].transform.position, false);
            } else {
                List<int> removeElmenteTheblocksMoreLife = new List<int>();
                foreach (int i in blocksMoreLife) {
                    if (sizeY != Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero)) {
                        removeElmenteTheblocksMoreLife.Add(i);
                    }
                }
                foreach (int i in removeElmenteTheblocksMoreLife) {
                    blocksMoreLife.Remove(i);
                }
               
                if (blocksMoreLife.Count == 1) {
                    return angleLaunch(blocks_[blocksMoreLife[0]].transform.position, false);
                } else {
                    double blockMoreTallest = -1.0f;
                    foreach (int i in blocksMoreLife) {
                        if (blockMoreTallest < Math.Round((blocks_[i].transform.position.y + (blocks_[i].get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero)) {
                            blockMoreTallest = Math.Round((blocks_[i].transform.position.y + (blocks_[i].get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                        }
                    }
                    removeElmenteTheblocksMoreLife.Clear();
                    //removeElmenteTheblocksMoreLife = new List<int>();
                    for (int i = 0; i < blocksMoreLife.Count; i++) {
                        if(blockMoreTallest != Math.Round((blocks_[blocksMoreLife[i]].transform.position.y + (blocks_[blocksMoreLife[i]].get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero)) {
                            removeElmenteTheblocksMoreLife.Add(blocksMoreLife[i]);
                        }
                    }

                    foreach(int i in removeElmenteTheblocksMoreLife) {
                        blocksMoreLife.Remove(i);
                    }

                    if (blocksMoreLife.Count == 1) {
                        return angleLaunch(blocks_[blocksMoreLife[0]].transform.position, false);
                    } else {
                        List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
                        float LessDistance = 300.0f, auxDistance;
                        Vector2 blockLaunch = new Vector2();
                        foreach (int i in blocksMoreLife) {
                            foreach(ABPig p in pigs_){
                                if (p._currentLife > 0) {
                                    auxDistance = Mathf.Sqrt(Mathf.Pow((blocks_[i].transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((blocks_[i].transform.position.y - p.transform.position.y), 2));
                                    if (LessDistance > auxDistance) {
                                        LessDistance = auxDistance;
                                        blockLaunch = blocks_[i].transform.position;
                                    }
                                }
                            }
                        }

                        return angleLaunch(blockLaunch, false);
                    }
                }
            }
        }
    }

    public double highHitLargerYBlock() { //Função que mira no maior bloco no eixo Y do nível com ângulo alto. Caso tenha 2 ou mais blocos com o mesmo tamanho em Y, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo Y e a mesma quantidade de vida, escolher o que está mais próximo de um porco. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais alto (highHitLeftMostpig)
        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
        double sizeY = -1.0f;
        for (int i = 0; i < blocks_.Count; i++) {
            if (Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero) > sizeY) {
                sizeY = Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero);
            }
        }
        if (sizeY == -1.0f) {
            return highHitLeftMostpig();
        } else {
            List<ABBlock> listBlocksTallestSizeY = new List<ABBlock>();
            foreach(ABBlock b in blocks_) {
                if (Math.Round(b.get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero) == sizeY) {
                    listBlocksTallestSizeY.Add(b);
                }
            }
            if (listBlocksTallestSizeY.Count == 1) {
                return angleLaunch(listBlocksTallestSizeY[0].transform.position, true);
            } else {
                float smallerCurrentLife = 100;
                foreach(ABBlock b in listBlocksTallestSizeY) {
                    if (b.getLife() < smallerCurrentLife) {
                        smallerCurrentLife = b.getLife();
                    }
                }

                listBlocksTallestSizeY.Clear();
                foreach(ABBlock b in blocks_) {
                    if ((b._currentLife == smallerCurrentLife) && (Math.Round(b.get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero) == sizeY)) {
                        listBlocksTallestSizeY.Add(b);
                    }
                }

                if (listBlocksTallestSizeY.Count == 1) {
                    return angleLaunch(listBlocksTallestSizeY[0].transform.position, true);
                } else {
                    List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
                    float LessDistance = 300.0f, auxDistance;
                    Vector2 blockLaunch = new Vector2();
                    foreach (ABBlock b in listBlocksTallestSizeY) {
                        foreach(ABPig p in pigs_){
                            if (p._currentLife > 0) {
                                auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
                                if (LessDistance > auxDistance) {
                                    LessDistance = auxDistance;
                                    blockLaunch = b.transform.position;
                                }
                            }
                        }
                    }
                    return angleLaunch(blockLaunch, true);
                }
            }
        }
    }

    public double lowHitLargerYBlock() { //Função que mira no maior bloco no eixo Y do nível com ângulo alto. Caso tenha 2 ou mais blocos com o mesmo tamanho em Y, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo Y e a mesma quantidade de vida, escolher o que está mais próximo de um porco. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais alto (highHitLeftMostpig)
        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
        double sizeY = -1.0f;
        for (int i = 0; i < blocks_.Count; i++) {
            if (Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero) > sizeY) {
                sizeY = Math.Round(blocks_[i].get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero);
            }
        }
        if (sizeY == -1.0f) {
            return lowHitLeftMostpig();
        } else {
            List<ABBlock> listBlocksTallestSizeY = new List<ABBlock>();
            foreach(ABBlock b in blocks_) {
                if (Math.Round(b.get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero) == sizeY) {
                    listBlocksTallestSizeY.Add(b);
                }
            }
            if (listBlocksTallestSizeY.Count == 1) {
                return angleLaunch(listBlocksTallestSizeY[0].transform.position, false);
            } else {
                float smallerCurrentLife = 100;
                foreach(ABBlock b in listBlocksTallestSizeY) {
                    if (b.getLife() < smallerCurrentLife) {
                        smallerCurrentLife = b.getLife();
                    }
                }

                listBlocksTallestSizeY.Clear();
                foreach(ABBlock b in blocks_) {
                    if ((b._currentLife == smallerCurrentLife) && (Math.Round(b.get_Collider().bounds.size.y, 1, MidpointRounding.AwayFromZero) == sizeY)) {
                        listBlocksTallestSizeY.Add(b);
                    }
                }

                if (listBlocksTallestSizeY.Count == 1) {
                    return angleLaunch(listBlocksTallestSizeY[0].transform.position, false);
                } else {
                    List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
                    float LessDistance = 300.0f, auxDistance;
                    Vector2 blockLaunch = new Vector2();
                    foreach (ABBlock b in listBlocksTallestSizeY) {
                        foreach(ABPig p in pigs_){
                            if (p._currentLife > 0) {
                                auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
                                if (LessDistance > auxDistance) {
                                    LessDistance = auxDistance;
                                    blockLaunch = b.transform.position;
                                }
                            }
                        }
                    }
                    return angleLaunch(blockLaunch, false);
                }
            }
        }
    }

    public double highHitLargerXBlock() { //Função que mira no maior bloco no eixo X do nível, com ângulo alto. Caso tenha 2 ou mais blocos com o mesmo tamanho em X, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo X e a mesma quantidade de vida, escolher o que esta mais abaixo no nível. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais alto (highHitLeftMostpig)
        double sizeX = -1.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero) > sizeX)) {
                sizeX = Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero);
            }
        }

        if(sizeX == -1.0f) {
            return highHitLeftMostpig();
        } else {
            List<ABBlock> blockLargerX = new List<ABBlock>();
            float blockLessLife = 11.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero) == sizeX)) {
                    blockLargerX.Add(b.GetComponent<ABBlock>());
                    if (blockLessLife > b.GetComponent<ABBlock>()._currentLife) {
                        blockLessLife = b.GetComponent<ABBlock>()._currentLife;
                    }
                }
            }

            if (blockLargerX.Count == 1) {
                return angleLaunch(blockLargerX[0].transform.position, true);
            } else {
                foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                    if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero) == sizeX) && (blockLessLife != b.GetComponent<ABBlock>()._currentLife)) {
                    //if (blockLessLife != b.GetComponent<ABBlock>()._currentLife) {
                        blockLargerX.Remove(b.GetComponent<ABBlock>());
                    }
                }

                if (blockLargerX.Count == 1) {
                    return angleLaunch(blockLargerX[0].transform.position, true);
                } else {
                    //Transform blockLaunch = transform;
                    //ABBlock blockLaunch = new ABBlock();
                    ABBlock blockLaunch = blockLargerX[0];
                    double hight = 100.0f;
                    foreach (ABBlock blo in blockLargerX)
                        if (hight > Math.Round((blo.transform.position.y - (blo.get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero)) {
                            blockLaunch = blo;
                            hight = Math.Round((blo.transform.position.y - (blo.get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                        }

                    return angleLaunch(blockLaunch.transform.position, true);
                }
            }
        }
    }

    public double lowHitLargerXBlock() { //Função que mira no maior bloco no eixo X do nível com ângulo baixo. Caso tenha 2 ou mais blocos com o mesmo tamanho em X, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo X e a mesma quantidade de vida, escolher o que esta mais abaixo no nível. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais baixo (lowHitLeftMostpig)
        double sizeX = -1.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero) > sizeX)) {
                sizeX = Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero);
            }
        }

        if(sizeX == -1.0f) {
            return lowHitLeftMostpig();
        } else {
            List<ABBlock> blockLargerX = new List<ABBlock>();
            float blockLessLife = 11.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero) == sizeX)) {
                    blockLargerX.Add(b.GetComponent<ABBlock>());
                    if (blockLessLife > b.GetComponent<ABBlock>()._currentLife) {
                        blockLessLife = b.GetComponent<ABBlock>()._currentLife;
                    }
                }
            }

            if (blockLargerX.Count == 1) {
                return angleLaunch(blockLargerX[0].transform.position, false);
            } else {
                foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                    if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (Math.Round(b.GetComponent<ABBlock>().GetComponent<Collider2D>().bounds.size.x, 2, MidpointRounding.AwayFromZero) == sizeX) && (blockLessLife != b.GetComponent<ABBlock>()._currentLife)) {
                    //if (blockLessLife != b.GetComponent<ABBlock>()._currentLife) {
                        blockLargerX.Remove(b.GetComponent<ABBlock>());
                    }
                }

                if (blockLargerX.Count == 1) {
                    return angleLaunch(blockLargerX[0].transform.position, false);
                } else {
                    //Transform blockLaunch = transform;
                    //ABBlock blockLaunch = new ABBlock();
                    ABBlock blockLaunch = blockLargerX[0];
                    double hight = 100.0f;
                    foreach (ABBlock blo in blockLargerX)
                        if (hight > Math.Round((blo.transform.position.y - (blo.get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero)) {
                            blockLaunch = blo;
                            hight = Math.Round((blo.transform.position.y - (blo.get_Collider().bounds.size.y / 2.0f)), 1, MidpointRounding.AwayFromZero);
                        }

                    return angleLaunch(blockLaunch.transform.position, false);
                }
            }
        }
    }

    private bool elementBelongsSublists(List<List<int>> touching_, int elem) {
       foreach (List<int> subL in touching_) {
            if (subL.Contains(elem)) {
                return true;
            }
        }

        return false;
    }

    public double highHitbiggerStructureY(List<List<int>> blocksTouching) {
        if (blocksTouching.Count == 0) {
            return highHitLeftMostpig();
        }

        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;

        List<int> open = new List<int>();
        List<List<int>> close = new List<List<int>>();

        int posBlockCurrent, sizeListClose = 0;

        for (int i = 0; i < blocks_.Count; i++) {
            if (!elementBelongsSublists(close, i)) {
                close.Add(new List<int>());
                open.Add(i);
                while (open.Count > 0) {
                    posBlockCurrent = open[0];
                    open.Remove(open[0]);
                    for (int l = 0; l < blocksTouching[posBlockCurrent].Count; l++) {
                        if (!open.Contains(blocksTouching[posBlockCurrent][l]) && !elementBelongsSublists(close, blocksTouching[posBlockCurrent][l])) {
                            open.Add(blocksTouching[posBlockCurrent][l]);
                        }
                    }
                    close[sizeListClose].Add(posBlockCurrent);
                }
                sizeListClose++;
            }
        }

        //Apenas Print de Debuger
        //int contApagar = 0;
        //foreach (List<int> subList in close) {
        //    foreach (int i in subList) {
        //        Debug.Log(i);
        //    }
        //    contApagar++;
        //}

        canFindBiggerStructure = true;

        List<float> largerBlockY = new List<float>(), smallestBlockY = new List<float>();
        foreach (List<int> subListClose in close) {
            largerBlockY.Add(-100.0f);
            smallestBlockY.Add(100.0f);
        }

        int countStructure = 0;

        foreach (List<int> subListClose in close) {
            foreach (int i in subListClose) {
                //Debug.Log("i = " + i + ", blocks_.Count = " + blocks_.Count);
                float boundsBlock = (blocks_[i].get_Collider().bounds.size.y / 2.0f);
                if (largerBlockY[countStructure] < (blocks_[i].transform.position.y + boundsBlock)) {
                    largerBlockY[countStructure] = blocks_[i].transform.position.y + boundsBlock;
                }
                if (smallestBlockY[countStructure] > (blocks_[i].transform.position.y - boundsBlock)) {
                    smallestBlockY[countStructure] = blocks_[i].transform.position.y - boundsBlock;
                }
            }
            countStructure++;
        }

        float largerStructure = 0.0f;
        int posLargerStructure = 0;
        for(int i = 0; i < countStructure; i++) {
            if (largerStructure < (largerBlockY[i] - smallestBlockY[i])) {
                largerStructure = largerBlockY[i] - smallestBlockY[i];
                posLargerStructure = i;
            }
        }

        //Transform smallestBlockLargerStructure = transform;
        Vector2 smallestBlockLargerStructure = new Vector2();
        float smallesBlock = 100.0f;
        foreach (int i in close[posLargerStructure]) {
            if (smallesBlock > (blocks_[i].transform.position.y - (blocks_[i].get_Collider().bounds.size.y / 2))) {
                smallesBlock = (blocks_[i].transform.position.y - (blocks_[i].get_Collider().bounds.size.y / 2));
                smallestBlockLargerStructure = blocks_[i].transform.position;
            }
        }

        return angleLaunch(smallestBlockLargerStructure, true);
    }

    public double lowHitbiggerStructureY(List<List<int>> blocksTouching) {
        if (blocksTouching.Count == 0) {
            return lowHitLeftMostpig();
        }

        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;

        List<int> open = new List<int>();
        List<List<int>> close = new List<List<int>>();

        int posBlockCurrent, sizeListClose = 0;

        for (int i = 0; i < blocks_.Count; i++) {
            if (!elementBelongsSublists(close, i)) {
                close.Add(new List<int>());
                open.Add(i);
                while (open.Count > 0) {
                    posBlockCurrent = open[0];
                    open.Remove(open[0]);
                    for (int l = 0; l < blocksTouching[posBlockCurrent].Count; l++) {
                        if (!open.Contains(blocksTouching[posBlockCurrent][l]) && !elementBelongsSublists(close, blocksTouching[posBlockCurrent][l])) {
                            open.Add(blocksTouching[posBlockCurrent][l]);
                        }
                    }
                    close[sizeListClose].Add(posBlockCurrent);
                }
                sizeListClose++;
            }
        }

        canFindBiggerStructure = true;

        List<float> largerBlockY = new List<float>(), smallestBlockY = new List<float>();
        foreach (List<int> subListClose in close) {
            largerBlockY.Add(-100.0f);
            smallestBlockY.Add(100.0f);
        }

        int countStructure = 0;

        foreach (List<int> subListClose in close) {
            foreach (int i in subListClose) {
                float boundsBlock = (blocks_[i].get_Collider().bounds.size.y / 2.0f);
                if (largerBlockY[countStructure] < (blocks_[i].transform.position.y + boundsBlock)) {
                    largerBlockY[countStructure] = blocks_[i].transform.position.y + boundsBlock;
                }
                if (smallestBlockY[countStructure] > (blocks_[i].transform.position.y - boundsBlock)) {
                    smallestBlockY[countStructure] = blocks_[i].transform.position.y - boundsBlock;
                }
            }
            countStructure++;
        }

        float largerStructure = 0.0f;
        int posLargerStructure = 0;
        for(int i = 0; i < countStructure; i++) {
            if (largerStructure < (largerBlockY[i] - smallestBlockY[i])) {
                largerStructure = largerBlockY[i] - smallestBlockY[i];
                posLargerStructure = i;
            }
        }

        //Transform smallestBlockLargerStructure = transform;
        Vector2 smallestBlockLargerStructure = new Vector2();
        float smallesBlock = 100.0f;
        foreach (int i in close[posLargerStructure]) {
            if (smallesBlock > (blocks_[i].transform.position.y - (blocks_[i].get_Collider().bounds.size.y / 2))) {
                smallesBlock = (blocks_[i].transform.position.y - (blocks_[i].get_Collider().bounds.size.y / 2));
                smallestBlockLargerStructure = blocks_[i].transform.position;
            }
        }

        return angleLaunch(smallestBlockLargerStructure, false);
    }

    public double highHitLeftMostTNT(List<List<int>> blocksTouching_) {  // Função que mira no bloco TNT mais a esquerda do nível, com ângulo de lançamento alto. Se o estado não tiver TNT, minar na base da estrtura mais alta do nível.
        float positionHightMostLeftTNT = 100f;
        //yTransform mostLeftTNT = transform;
        Vector2 mostLeftTNT = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABTNT>() != null) && (b.GetComponent<ABTNT>()._currentLife > 0.0f) && (positionHightMostLeftTNT > b.transform.position.x)) {
                mostLeftTNT = b.position;
                positionHightMostLeftTNT = b.transform.position.x;
            }
        }

        if (positionHightMostLeftTNT != 100) {
            return angleLaunch(mostLeftTNT, true);
        } else {
            return highHitbiggerStructureY(blocksTouching_);
        }
    }


    public double lowHitLeftMostTNT(List<List<int>> blocksTouching_) {  // Função que mira no bloco TNT mais a esquerda do nível, com ângulo de lançamento alto. Se o estado não tiver TNT, minar na base da estrtura mais alta do nível.
        float positionHightMostLeftTNT = 100f;
        //yTransform mostLeftTNT = transform;
        Vector2 mostLeftTNT = new Vector2();
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABTNT>() != null) && (b.GetComponent<ABTNT>()._currentLife > 0.0f) && (positionHightMostLeftTNT > b.transform.position.x)) {
                mostLeftTNT = b.position;
                positionHightMostLeftTNT = b.transform.position.x;
            }
        }

        if (positionHightMostLeftTNT != 100) {
            return angleLaunch(mostLeftTNT, false);
        } else {
            return lowHitbiggerStructureY(blocksTouching_);
        }
    }

    public double highHitBlockNearpig() {
        float smallestDistance = 100.0f, auxDistance;
        //Transform blockNearPig = transform;
        Vector2 blockNearPig = new Vector2();
        foreach (Transform p in ABGameWorld.Instance._blocksTransform) {
            if ((p.GetComponent<ABPig>() != null) && (p.GetComponent<ABPig>()._currentLife > 0)) {
                foreach(Transform b in ABGameWorld.Instance._blocksTransform) {
                    if (!p.Equals(b) && (b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
                        auxDistance = Mathf.Sqrt(Mathf.Pow((p.transform.position.x - b.transform.position.x), 2.0f) + Mathf.Pow((p.transform.position.y - b.transform.position.y), 2));
                        if (smallestDistance > auxDistance) {
                            smallestDistance = auxDistance;
                            blockNearPig = b.position;
                        }
                    }
                }
            }
        }

        if (smallestDistance != 100.0f) {
            return angleLaunch(blockNearPig, true);
        } else {
            return highHitHightMostpig();
        }
    }

    public double lowHitBlockNearpig() {
        float smallestDistance = 100.0f, auxDistance;
        //Transform blockNearPig = transform;
        Vector2 blockNearPig = new Vector2();
        foreach (Transform p in ABGameWorld.Instance._blocksTransform) {
            if ((p.GetComponent<ABPig>() != null) && (p.GetComponent<ABPig>()._currentLife > 0)) {
                foreach(Transform b in ABGameWorld.Instance._blocksTransform) {
                    if (!p.Equals(b) && (b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
                        auxDistance = Mathf.Sqrt(Mathf.Pow((p.transform.position.x - b.transform.position.x), 2.0f) + Mathf.Pow((p.transform.position.y - b.transform.position.y), 2));
                        if (smallestDistance > auxDistance) {
                            smallestDistance = auxDistance;
                            blockNearPig = b.position;
                        }
                    }
                }
            }
        }

        if (smallestDistance != 100.0f) {
            return angleLaunch(blockNearPig, false);
        } else {
            return lowHitHightMostpig();
        }
    }

    public double highHitBlockNearpigInX() {
        float smallestDistance = 100.0f, auxDistance;
        //Transform blockNearPig = transform;
        Vector2 blockNearPig = new Vector2();
        foreach (Transform p in ABGameWorld.Instance._blocksTransform) {
            if ((p.GetComponent<ABPig>() != null) && (p.GetComponent<ABPig>()._currentLife > 0)) {
                foreach(Transform b in ABGameWorld.Instance._blocksTransform) {
                    if (!p.Equals(b)) {
                        if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
                            auxDistance = Mathf.Abs(p.transform.position.x - b.transform.position.x);
                            if (smallestDistance > auxDistance) {
                                smallestDistance = auxDistance;
                                blockNearPig = b.position;
                            }
                        }
                    }
                }
            }
        }

        if (smallestDistance != 100.0f) {
            return angleLaunch(blockNearPig, true);
        } else {
            return highHitHightMostpig();
        }
    }

    public double lowHitBlockNearpigInX() {
        float smallestDistance = 100.0f, auxDistance;
        //Transform blockNearPig = transform;
        Vector2 blockNearPig = new Vector2();
        foreach (Transform p in ABGameWorld.Instance._blocksTransform) {
            if ((p.GetComponent<ABPig>() != null) && (p.GetComponent<ABPig>()._currentLife > 0)) {
                foreach(Transform b in ABGameWorld.Instance._blocksTransform) {
                    if (!p.Equals(b)) {
                        if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
                            auxDistance = Mathf.Abs(p.transform.position.x - b.transform.position.x);
                            if (smallestDistance > auxDistance) {
                                smallestDistance = auxDistance;
                                blockNearPig = b.position;
                            }
                        }
                    }
                }
            }
        }

        if (smallestDistance != 100.0f) {
            return angleLaunch(blockNearPig, false);
        } else {
            return lowHitHightMostpig();
        }
    }

    public Double highHitMoreHightCircle() {
        float hightCircle = -100.0f;
        //Transform moreHightCircle = transform;
        Vector2 moreHightCircle = new Vector2();
        bool haveBlockInLevel = false ;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && ((b.GetComponent<ABBlock>().name == "Circle(Clone)") || (b.GetComponent<ABBlock>().name == "CircleSmall(Clone)"))) {
                haveBlockInLevel = true;
                if (hightCircle < (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2))) {
                    hightCircle = (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2));
                    moreHightCircle = b.position;
                }
            }
        }

        if (!haveBlockInLevel) {
            return highHitLargerYBlock();
        } else {
            return angleLaunch(moreHightCircle, true);
        }
    }

    public Double lowHitMoreHightCircle() {
        float hightCircle = -100.0f;
        //Transform moreHightCircle = transform;
        Vector2 moreHightCircle = new Vector2();
        bool haveBlockInLevel = false ;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && ((b.GetComponent<ABBlock>().name == "Circle(Clone)") || (b.GetComponent<ABBlock>().name == "CircleSmall(Clone)"))) {
                haveBlockInLevel = true;
                if (hightCircle < (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2))) {
                    hightCircle = (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2));
                    moreHightCircle = b.position;
                }
            }
        }

        if (!haveBlockInLevel) {
            return lowHitLargerYBlock();
        } else {
            return angleLaunch(moreHightCircle, false);
        }
    }

    public Double highHitMorelowerCircle() {
        float hightCircle = 100.0f;
        //Transform moreLowerHightCircle = transform;
        Vector2 moreLowerHightCircle = new Vector2();
        bool haveBlockInLevel = false ;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && ((b.GetComponent<ABBlock>().name == "Circle(Clone)") || (b.GetComponent<ABBlock>().name == "CircleSmall(Clone)"))) {
                haveBlockInLevel = true;
                if (hightCircle > (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2))) {
                    hightCircle = (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2));
                    moreLowerHightCircle = b.position;
                }
            }
        }

        if (!haveBlockInLevel) {
            return highHitLargerXBlock();
        } else {
            return angleLaunch(moreLowerHightCircle, true);
        }
    }

    public Double lowHitMorelowerCircle() {
        float hightCircle = 100.0f;
        //Transform moreLowerHightCircle = transform;
        Vector2 moreLowerHightCircle = new Vector2();
        bool haveBlockInLevel = false ;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && ((b.GetComponent<ABBlock>().name == "Circle(Clone)") || (b.GetComponent<ABBlock>().name == "CircleSmall(Clone)"))) {
                haveBlockInLevel = true;
                if (hightCircle > (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2))) {
                    hightCircle = (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2));
                    moreLowerHightCircle = b.position;
                }
            }
        }

        if (!haveBlockInLevel) {
            return lowHitLargerXBlock();
        } else {
            return angleLaunch(moreLowerHightCircle, false);
        }
    }

    public double highTallestPigSupoerteBlock(List<List<int>> PigsTouchingBlocks) {
        List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
        int posPigsTouchingBlocks = 0;
        float TallestPigLevel = -100.0f;
        int posBlockTouchingTallestPig = -1;
        foreach (List<int> subList in PigsTouchingBlocks) {
            if (subList.Count != 0) {
                if (TallestPigLevel < (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2))) {
                    TallestPigLevel = (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2));
                    posBlockTouchingTallestPig = posPigsTouchingBlocks;
                }
            }
            posPigsTouchingBlocks++;
        }
        if(posBlockTouchingTallestPig == -1)
            return highHitHightMostpig();
        else {
            float Ypig, YBlock;
            List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
            foreach (int b in PigsTouchingBlocks[posBlockTouchingTallestPig]) {
                Ypig = (pigs_[posBlockTouchingTallestPig].transform.position.y - (pigs_[posBlockTouchingTallestPig].get_Collider().bounds.size.y / 2));
                YBlock = (blocks_[b].transform.position.y + (blocks_[b].get_Collider().bounds.size.y / 2));
                if (Math.Abs(Ypig - YBlock) < 0.1f) {
                    return angleLaunch(blocks_[b].transform.position, true);
                }
            }

            return highHitHightMostpig();
        }
    }

    public double lowTallestPigSupoerteBlock(List<List<int>> PigsTouchingBlocks) {
        List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
        int posPigsTouchingBlocks = 0;
        float TallestPigLevel = -100.0f;
        int posBlockTouchingTallestPig = -1;
        foreach (List<int> subList in PigsTouchingBlocks) {
            if (subList.Count != 0) {
                if (TallestPigLevel < (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2))) {
                    TallestPigLevel = (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2));
                    posBlockTouchingTallestPig = posPigsTouchingBlocks;
                }
            }
            posPigsTouchingBlocks++;
        }
        if(posBlockTouchingTallestPig == -1)
            return lowHitHightMostpig();
        else {
            float Ypig, YBlock;
            List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
            foreach (int b in PigsTouchingBlocks[posBlockTouchingTallestPig]) {
                Ypig = (pigs_[posBlockTouchingTallestPig].transform.position.y - (pigs_[posBlockTouchingTallestPig].get_Collider().bounds.size.y / 2));
                YBlock = (blocks_[b].transform.position.y + (blocks_[b].get_Collider().bounds.size.y / 2));
                if (Math.Abs(Ypig - YBlock) < 0.1f) {
                    return angleLaunch(blocks_[b].transform.position, false);
                }
            }

            return lowHitHightMostpig();
        }
    }

    public double choiceFunction(int numFunction, List<List<int>> listToList = null) {
        switch (numFunction) {
            case 0:
            {
                return highHitLeftMostpig();
                //break;
            }
            case 1:
            {
                return highHitRightMostpig();
                //break;
            }
            case 2:
            {
                return highHitHightMostpig();
                //break;
            }
            case 3:
            {
                return highHitLowerMostpig();
                //break;
            }
            case 4:
            {
                return highHitPigLessLife();
                //break;
            }
            case 5:
            {
                    return highHitPigMoreLife();
                //break;
            }
            case 6:
            {
                return highHitBlockLessLife();
                //break;
            }
            case 7:
            {
                return highHitBlockMoreLife();
                //break;
            }
            case 8:
            {
                return highHitLargerYBlock();
                //break;
            }
            case 9:
            {
                return highHitLargerXBlock();
                //break;
            }
            case 10:
            {
                if (listToList != null) {
                    return highHitLeftMostTNT(listToList);
                } else {
                        Debug.Log("Erro em Case 10 \"highHitLeftMostTNT()\"--> ListToList is Empty");
                }
                break;
            }
            case 11:
            {
                if (listToList != null) {
                    return highHitbiggerStructureY(listToList);
                } else {
                        Debug.Log("Erro in Case 11 \"highHitbiggerStructureY()\" --> ListToList is Empty");
                }
                break;
            }
            case 12:
            {
                return highHitBlockNearpig();
                //break;
            }
            case 13:
            {
                return highHitMoreHightCircle();
                //break;
            }
            case 14:
            {
                return lowHitMoreHightCircle();
                //break;
            }
            case 15:
            {
                return highHitMorelowerCircle();
            }
            case 16:
            {
                if (listToList != null) {
                    return highTallestPigSupoerteBlock(listToList);
                } else {
                        Debug.Log("Erro in Case 11 \"tallestPigSupoerteBlock()\" --> ListToList is Empty");
                }
                break;
            }
            case 17:
            {
                return lowHitLeftMostpig();
                //break;
            }
            case 18:
            {
                return lowHitRightMostpig();
                //break;
            }
            case 19:
            {
                return lowHitHightMostpig();
                //break;
            }
            case 20:
            {
                return lowHitLowerMostpig();
                //break;
            }
            case 21:
            {
                return lowHitPigLessLife();
                //break;
            }
            case 22:
            {
                return lowHitPigMoreLife();
                //break;
            }
            case 23:
            {
                return lowHitBlockLessLife();
                //break;
            }
            case 24:
            {
                return lowHitBlockMoreLife();
                //break;
            }
            case 25:
            {
                return lowHitLargerYBlock();
                //break;
            }
            case 26:
            {
                return lowHitLargerXBlock();
                //break;
            }
            case 27:
            {
                if (listToList != null) {
                    return lowHitLeftMostTNT(listToList);
                } else {
                        Debug.Log("Erro em Case 10 \"highHitLeftMostTNT()\"--> ListToList is Empty");
                }
                break;
            }
            case 28:
            {
                if (listToList != null) {
                    return lowHitbiggerStructureY(listToList);
                } else {
                        Debug.Log("Erro in Case 11 \"highHitbiggerStructureY()\" --> ListToList is Empty");
                }
                break;
            }
            case 29:
            {
                return lowHitBlockNearpig();
                //break;
            }
            case 30:
            {
                return lowHitMorelowerCircle();
                //break;
            }
            case 31:
            {
                if (listToList != null) {
                    return lowTallestPigSupoerteBlock(listToList);
                } else {
                        Debug.Log("Erro in Case 11 \"tallestPigSupoerteBlock()\" --> ListToList is Empty");
                }
                break;
            }
            default:
            {
                Debug.Log("Erro, numero de função não encontrada = " + numFunction);
                return 0;
                //break;
            }
        }

        Debug.Log("Erro, função Choice da Function_ML nem entrou no siwtch case... --> numFunction = " + numFunction);
        return 0;
    }
}




//////////////////////-------------------------------------------------------------------------------------/////////////////////////
/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using System.IO;

//using TensorFlow;
//using UnityEngine.UI;

public class Function_ML : MonoBehaviour {
    private const float velocidade = 9.68f;
    private const double x1 = -8.15f, y1 = -1.7f;
    private static readonly float sqr2Velociade = Mathf.Pow(velocidade, 2.0f), sqr4Velociade = Mathf.Pow(velocidade, 4.0f);
    private float gravity;

    public bool canFindBiggerStructure;
    double angleLaunchBiggerStructure;

    private double angleLaunch(Transform target, bool hight) { //função que faz o calculo do ângulo dado a posição do pássado, objeto alvo, força de lancamento e gravidade. Nesta Implementação é retornado apenas um dos dois ângulos possíveis ,sendo controlado pelo parâmetro hight (alto == true ou baixo == false).
        double deltaX = target.transform.position.x - x1;
        double deltaY = target.transform.position.y - y1;

        double aux = Mathf.Pow((float)deltaX, 2.00f);

        aux *= gravity;

        double aux1 = 2 * deltaY * sqr2Velociade;

        aux += aux1;
        aux *= gravity;
        aux = sqr4Velociade - aux;
        aux = Mathf.Sqrt((float)aux);

        double denominador = gravity * deltaX;

        double resp = -100;
        if (hight) {
            resp = sqr2Velociade + aux;
            resp /= denominador;

            resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;
        } else {
            resp = sqr2Velociade - aux;
            resp /= denominador;

            resp = Mathf.Atan((float)resp) * 180 / Mathf.PI;
        }

        if (!Double.IsNaN(resp)) {
            return resp;
        } else {
            return -100.0;
        }
    }

    public double highHitLeftMostpig() { // Função que escolhe o porco mais a esquerda do nível, com ângulo de lançamento alto.
        float positionLeftMostPig = 100;
        Transform leftMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x < positionLeftMostPig) {
                    positionLeftMostPig = b.transform.position.x;
                    leftMostPig = b;
                }
            }
        }

        return angleLaunch(leftMostPig, true);
    }

    public double lowHitLeftMostpig() { // Função que escolhe o porco mais a esquerda do nível, com ângulo de lançamento baixo.
        float positionLeftMostPig = 100;
        Transform leftMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x < positionLeftMostPig) {
                    positionLeftMostPig = b.transform.position.x;
                    leftMostPig = b;
                }
            }
        }

        return angleLaunch(leftMostPig, false);
    }

    public double highHitRightMostpig() { // Função que escolhe o porco mais a direita do nível, com ângulo de lançamento alto.
        float positionRightMostPig = -100;
        Transform rightMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x > positionRightMostPig) {
                    positionRightMostPig = b.transform.position.x;
                    rightMostPig = b;
                }
            }
        }

        return angleLaunch(rightMostPig, true);
    }


    public double lowHitRightMostpig() { // Função que escolhe o porco mais a direita do nível, com ângulo de lançamento baixo.
        float positionRightMostPig = -100;
        Transform rightMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.x > positionRightMostPig) {
                    positionRightMostPig = b.transform.position.x;
                    rightMostPig = b;
                }
            }
        }

        return angleLaunch(rightMostPig, false);
    }

    public double highHitHightMostpig() { // Função que escolhe o porco mais alto do nível, com ângulo de lançamento alto.
        float positionHightMostPig = -100;
        Transform hightMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.y > positionHightMostPig) {
                    positionHightMostPig = b.transform.position.y;
                    hightMostPig = b;
                }
            }
        }

        return angleLaunch(hightMostPig, true);
    }

    public double lowHitHightMostpig() { // Função que escolhe o porco mais alto do nível, com ângulo de lançamento baixo.
        float positionHightMostPig = -100;
        Transform hightMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.y > positionHightMostPig) {
                    positionHightMostPig = b.transform.position.y;
                    hightMostPig = b;
                }
            }
        }

        return angleLaunch(hightMostPig, false);
    }

    public double highHitLowerMostpig() {  // Função que escolhe o porco mais baixo do nível, com ângulo de lançamento alto.
        float positionHightMostPig = 100;
        Transform lowerMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.y < positionHightMostPig) {
                    positionHightMostPig = b.transform.position.y;
                    lowerMostPig = b;
                }
            }
        }

        return angleLaunch(lowerMostPig, true);
    }

    public double lowHitLowerMostpig() {  // Função que escolhe o porco mais baixo do nível, com ângulo de lançamento baixo.
        float positionHightMostPig = 100;
        Transform lowerMostPig = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.transform.position.y < positionHightMostPig) {
                    positionHightMostPig = b.transform.position.y;
                    lowerMostPig = b;
                }
            }
        }

        return angleLaunch(lowerMostPig, false);
    }

    public double highHitPigLessLife() {  //Função que escolhe o porco com menos vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o porco mais alto.
        float life = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0)) {
                if (b.GetComponent<ABPig>()._currentLife < life) {
                    life = b.GetComponent<ABPig>()._currentLife;
                }
            }
        }

        int cont = 0, pos = 0;
        List<Transform> listPigLessLife = new List<Transform>();
        float sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life)) {
                listPigLessLife.Add(b);
                if (sizeY < b.transform.position.y) {
                    sizeY = b.transform.position.y;
                    pos = cont;
                }
                cont++;
            }
        }

        return angleLaunch(listPigLessLife[pos], true);

        //if (cont == 1) {
        //    return angleLaunch(listPigLessLife[0], true);
        //} else {
        //    float hight = -100;
        //    Transform pigLessLife = transform;
        //    foreach (Transform p in listPigLessLife) {
        //        if (p.transform.position.y > hight) {
        //            hight = p.transform.position.y;
        //            pigLessLife = p;
        //        }
        //    }
        //    return angleLaunch(pigLessLife, true);
        //}
    }

    public double lowHitPigLessLife() {  //Função que escolhe o porco com menos vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o porco mais alto.
        float life = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (b.GetComponent<ABPig>()._currentLife < life)) {
                life = b.GetComponent<ABPig>()._currentLife;
            }
        }

        ////int cont = 0;
        ////List<Transform> listPigLessLife = new List<Transform>();
        ////foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
        ////    if (b.GetComponent<ABPig>() != null) {
        ////        if (b.GetComponent<ABPig>()._currentLife == life) {
        ////            listPigLessLife.Add(b);
        ////            cont++;
        ////        }
        ////    }
        ////}

        int cont = 0, pos = 0;
        List<Transform> listPigLessLife = new List<Transform>();
        float sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life)) {
                listPigLessLife.Add(b);
                if (sizeY < b.transform.position.y) {
                    sizeY = b.transform.position.y;
                    pos = cont;
                }
                cont++;
            }
        }

        return angleLaunch(listPigLessLife[pos], false);

        ////if (cont == 1) {
        ////    return angleLaunch(listPigLessLife[0], false);
        ////} else {
        ////    float hight = -100.0f;
        ////    Transform pigLessLife = transform;
        ////    foreach (Transform p in listPigLessLife) {
        ////        if (p.transform.position.y > hight) {
        ////            hight = p.transform.position.y;
        ////            pigLessLife = p;
        ////        }
        ////    }
        ////    return angleLaunch(pigLessLife, false);
        ////}
    }

    public double highHitPigMoreLife() {  // Função que escolhe o porco com mais vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o porco mais alto.
        float life = -100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (b.GetComponent<ABPig>()._currentLife > life)) {
                life = b.GetComponent<ABPig>()._currentLife;
            }
        }

        int cont = 0, pos = 0;
        List<Transform> listPigLessLife = new List<Transform>();
        float sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life)) {
                listPigLessLife.Add(b);
                if (sizeY < b.transform.position.y) {
                    sizeY = b.transform.position.y;
                    pos = cont;
                }
                cont++;
            }
        }

        return angleLaunch(listPigLessLife[pos], true);

        ////int cont = 0;
        ////List<Transform> listPigMoreLife = new List<Transform>();
        ////foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
        ////    if (b.GetComponent<ABPig>() != null) {
        ////        if (b.GetComponent<ABPig>()._currentLife == life) {
        ////                listPigMoreLife.Add(b);
        ////            cont++;
        ////        }
        ////    }
        ////}

        ////if (cont == 1) {
        ////    return angleLaunch(listPigMoreLife[0], true);
        ////} else {
        ////    float hight = -100;
        ////    Transform pigMoreLife = transform;
        ////    foreach (Transform p in listPigMoreLife) {
        ////        if (p.transform.position.y > hight) {
        ////            hight = p.transform.position.y;
        ////            pigMoreLife = p;
        ////        }
        ////    }
        ////    return angleLaunch(pigMoreLife, true);
        ////}

    }

    public double lowHitPigMoreLife() {  //Função que escolhe o porco com mais vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um porco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o porco mais alto.
        float life = -100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife > 0) && (b.GetComponent<ABPig>()._currentLife > life)) {
                life = b.GetComponent<ABPig>()._currentLife;
            }
        }

        int cont = 0, pos = 0;
        List<Transform> listPigLessLife = new List<Transform>();
        float sizeY = -100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == life)) {
                listPigLessLife.Add(b);
                if (sizeY < b.transform.position.y) {
                    sizeY = b.transform.position.y;
                    pos = cont;
                }
                cont++;
            }
        }

        return angleLaunch(listPigLessLife[pos], false);

        ////int cont = 0;
        ////List<Transform> listPigMoreLife = new List<Transform>();
        ////foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
        ////    if (b.GetComponent<ABPig>() != null) {
        ////        if (b.GetComponent<ABPig>()._currentLife == life) {
        ////                listPigMoreLife.Add(b);
        ////            cont++;
        ////        }
        ////    }
        ////}

        ////if (cont == 1) {
        ////    return angleLaunch(listPigMoreLife[0], false);
        ////} else {
        ////    float hight = -100;
        ////    Transform pigMoreLife = transform;
        ////    foreach (Transform p in listPigMoreLife) {
        ////        if (p.transform.position.y > hight) {
        ////            hight = p.transform.position.y;
        ////            pigMoreLife = p;
        ////        }
        ////    }
        ////    return angleLaunch(pigMoreLife, false);
        ////}
    }

    public double highHitBlockLessLife() {  //Função que escolhe o bloco com menos vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o bloco mais baixo. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo alto(highHitLeftMostpig)
        float lifeBlock = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && (b.GetComponent<ABBlock>()._currentLife < lifeBlock)) {
                lifeBlock = b.GetComponent<ABBlock>()._currentLife;
            }
        }

        ////int cont = 0;
        ////List<Transform> listBlockLessLife = new List<Transform>();
        ////foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
        ////    if (b.GetComponent<ABBlock>() != null) {
        ////        if (b.GetComponent<ABBlock>()._currentLife == lifeBlock) {
        ////            listBlockLessLife.Add(b);
        ////            cont++;
        ////        }
        ////    }
        ////}

        int cont = 0, pos = 0;
        List<Transform> listBlockLessLife = new List<Transform>();
        float sizeY = 100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == lifeBlock)) {
                listBlockLessLife.Add(b);
                if (sizeY > b.transform.position.y) {
                    sizeY = b.transform.position.y;
                    pos = cont;
                }
                cont++;
            }
        }

        return angleLaunch(listBlockLessLife[pos], true);


        ////if (cont == 0) {
        ////    return highHitLeftMostpig();
        ////} else if (cont == 1) {
        ////    return angleLaunch(listBlockLessLife[0].transform, true);
        ////} else {
        ////    float hight = 100.0f;
        ////    Transform blockLessLife = transform;
        ////    foreach (Transform bloco in blockLessLife) {
        ////        if (bloco.transform.position.y < hight) {
        ////            hight = bloco.transform.position.y;
        ////            blockLessLife = bloco;
        ////        }
        ////    }

        ////    return angleLaunch(blockLessLife.transform, true);
        ////}
    }

    public double lowHitBlockLessLife() {  //Função que escolhe o bloco com menos vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais baixa), escolher dentre esses o bloco mais baixo. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo baixo (lowHitLeftMostpig)
        float lifeBlock = 100;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && (b.GetComponent<ABBlock>()._currentLife < lifeBlock)) {
                lifeBlock = b.GetComponent<ABBlock>()._currentLife;
            }
        }

        int cont = 0, pos = 0;
        List<Transform> listBlockLessLife = new List<Transform>();
        float sizeY = 100.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABPig>() != null) && (b.GetComponent<ABPig>()._currentLife == lifeBlock)) {
                listBlockLessLife.Add(b);
                if (sizeY > b.transform.position.y) {
                    sizeY = b.transform.position.y;
                    pos = cont;
                }
                cont++;
            }
        }

        return angleLaunch(listBlockLessLife[pos], false);


        ////int cont = 0;
        ////List<Transform> listBlockLessLife = new List<Transform>();
        ////foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
        ////    if (b.GetComponent<ABBlock>() != null) {
        ////        if (b.GetComponent<ABBlock>()._currentLife == lifeBlock) {
        ////            listBlockLessLife.Add(b);
        ////            cont++;
        ////        }
        ////    }
        ////}

        ////if (cont == 0) {
        ////    return lowHitLeftMostpig();
        ////} else if (cont == 1) {
        ////    return angleLaunch(listBlockLessLife[0].transform, false);
        ////} else {
        ////    float hight = 100.0f;
        ////    Transform blockLessLife = transform;
        ////    foreach (Transform bloco in blockLessLife) {
        ////        if (bloco.transform.position.y < hight) {
        ////            hight = bloco.transform.position.y;
        ////            blockLessLife = bloco;
        ////        }
        ////    }

        ////    return angleLaunch(blockLessLife.transform, false);
        ////}
    }



    public double highHitBlockMoreLife() {  // Função que escolhe o bloco com mais vida do níveil, com ângulo de lançamento alto. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o maior bloco em Y. Caso exista 2 ou mais blocos com a mesma vida e tamanho em Y, escolher aquele mais proximo de um porco. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo alto (highHitLeftMostpig)
        float lifeBlock = 100f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && (b.GetComponent<ABBlock>()._currentLife < lifeBlock)) {
                lifeBlock = b.GetComponent<ABBlock>()._currentLife;
            }
        }

        if (lifeBlock != 100.0f) {
            List<Transform> blockLargerYWithLifeBlock = new List<Transform>();
            float sizeY = -1.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife == lifeBlock)) {
                    blockLargerYWithLifeBlock.Add(b);
                    if (sizeY < b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y) {
                        sizeY = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
                    }
                }
            }

            if (blockLargerYWithLifeBlock.Count == 1) {
                return angleLaunch(blockLargerYWithLifeBlock[0], true);
            } else {
                foreach (Transform b in blockLargerYWithLifeBlock) {
                    if (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y != sizeY) {
                        blockLargerYWithLifeBlock.Remove(b);
                    }
                }

                if (blockLargerYWithLifeBlock.Count == 1) {
                    return angleLaunch(blockLargerYWithLifeBlock[0], true);
                } else {
                    Transform blockLaunch = transform;
                    float LessDistance = 300.0f, auxDistance;
                    List<ABPig> _pigs = ABGameWorld.Instance._pigs;
                    foreach (Transform b in blockLargerYWithLifeBlock) {
                        foreach (ABPig p in _pigs) {
                            if (p._currentLife > 0) {
                                auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
                                if (LessDistance > auxDistance) {
                                    LessDistance = auxDistance;
                                    blockLaunch = b;
                                }
                            }
                        }
                    }

                    return angleLaunch(blockLaunch, true);
                }
            }

        } else {
            return highHitLeftMostpig();
        }
    }

    public double lowHitBlockMoreLife() {  // Função que escolhe o bloco com mais vida do níveil, com ângulo de lançamento baixo. Caso tenha mais de um bloco, com a mesma vida (sendo ela a mais alta), escolher dentre esses o maior bloco em Y. Caso exista 2 ou mais blocos com a mesma vida e tamanho em Y, escolher aquele mais próximo de um porco. Obs.: Se o nível não tiver blocos, mirar no porco mais a esquerda com ângulo baixo (lowHitLeftMostpig)
        float lifeBlock = 100f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && (b.GetComponent<ABBlock>()._currentLife < lifeBlock)) {
                lifeBlock = b.GetComponent<ABBlock>()._currentLife;
            }
        }

        if (lifeBlock != 100.0f) {
            List<Transform> blockLargerYWithLifeBlock = new List<Transform>();
            float sizeY = -1.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife == lifeBlock)) {
                    blockLargerYWithLifeBlock.Add(b);
                    if (sizeY < b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y) {
                        sizeY = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
                    }
                }
            }

            if (blockLargerYWithLifeBlock.Count == 1) {
                return angleLaunch(blockLargerYWithLifeBlock[0], false);
            } else {
                foreach (Transform b in blockLargerYWithLifeBlock) {
                    if (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y != sizeY) {
                        blockLargerYWithLifeBlock.Remove(b);
                    }
                }

                if (blockLargerYWithLifeBlock.Count == 1) {
                    return angleLaunch(blockLargerYWithLifeBlock[0], false);
                } else {
                    Transform blockLaunch = transform;
                    float LessDistance = 300.0f, auxDistance;
                    List<ABPig> _pigs = ABGameWorld.Instance._pigs;
                    foreach (Transform b in blockLargerYWithLifeBlock) {
                        foreach (ABPig p in _pigs) {
                            if (p._currentLife > 0) {
                                auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
                                if (LessDistance > auxDistance) {
                                    LessDistance = auxDistance;
                                    blockLaunch = b;
                                }
                            }
                        }
                    }

                    return angleLaunch(blockLaunch, false);
                }
            }

        } else {
            return lowHitLeftMostpig();
        }
    }

    public double highHitLargerYBlock() { //Função que mira no maior bloco no eixo Y do nível com ângulo alto. Caso tenha 2 ou mais blocos com o mesmo tamanho em Y, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo Y e a mesma quantidade de vida, escolher o que está mais próximo de um porco. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais alto (highHitLeftMostpig)
        float sizeY = -1.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y > sizeY)) {
                sizeY = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
            }
        }

        if (sizeY != -1.0f) {
            List<Transform> blockLargerY = new List<Transform>();
            float blockLessLife = 11.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y == sizeY)) {
                    blockLargerY.Add(b);
                    if ((b.GetComponent<ABBlock>()._currentLife > 0) && (blockLessLife > b.GetComponent<ABBlock>()._currentLife)) {
                        blockLessLife = b.GetComponent<ABBlock>()._currentLife;
                    }
                }
            }

            if (blockLargerY.Count == 1) {
                return angleLaunch(blockLargerY[0], true);
            } else {
                foreach (Transform b in blockLargerY) {
                    if (blockLessLife != b.GetComponent<ABBlock>()._currentLife) {
                        blockLargerY.Remove(b);
                    }
                }

                if (blockLargerY.Count == 1) {
                    return angleLaunch(blockLargerY[0], true);
                } else {
                    Transform blockLaunch = transform;
                    float LessDistance = 300.0f, auxDistance;
                    List<ABPig> _pigs = ABGameWorld.Instance._pigs;
                    foreach (Transform b in blockLargerY) {
                        foreach (ABPig p in _pigs) {
                            if (p._currentLife > 0) {
                                auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
                                if (LessDistance > auxDistance) {
                                    LessDistance = auxDistance;
                                    blockLaunch = b;
                                }
                            }
                        }
                    }

                    return angleLaunch(blockLaunch, true);
                }
            }

        } else {
            return highHitLeftMostpig();
        }
    }


    public double lowHitLargerYBlock() { //Função que mira no maior bloco no eixo Y do nível com ângulo baixo. Caso tenha 2 ou mais blocos com o mesmo tamanho em Y, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo Y e a mesma quantidade de vida, escolher o que está mais próximo de um porco. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais baixo (lowHitLeftMostpig)
        float sizeY = -1.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y > sizeY)) {
                sizeY = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
            }
        }

        if (sizeY != -1.0f) {
            List<Transform> blockLargerY = new List<Transform>();
            float blockLessLife = 11.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y == sizeY)) {
                    blockLargerY.Add(b);
                    if (blockLessLife > b.GetComponent<ABBlock>()._currentLife) {
                        blockLessLife = b.GetComponent<ABBlock>()._currentLife;
                    }
                }
            }

            if (blockLargerY.Count == 1) {
                return angleLaunch(blockLargerY[0], false);
            } else {
                foreach (Transform b in blockLargerY) {
                    if (blockLessLife != b.GetComponent<ABBlock>()._currentLife) {
                        blockLargerY.Remove(b);
                    }
                }

                if (blockLargerY.Count == 1) {
                    return angleLaunch(blockLargerY[0], false);
                } else {
                    Transform blockLaunch = transform;
                    float LessDistance = 300.0f, auxDistance;
                    List<ABPig> _pigs = ABGameWorld.Instance._pigs;
                    foreach (Transform b in blockLargerY) {
                        foreach (ABPig p in _pigs) {
                            if (p._currentLife > 0) {
                                auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
                                if (LessDistance > auxDistance) {
                                    LessDistance = auxDistance;
                                    blockLaunch = b;
                                }
                            }
                        }
                    }

                    return angleLaunch(blockLaunch, false);
                }
            }

        } else {
            return lowHitLeftMostpig();
        }
    }


    public double highHitLargerXBlock() { //Função que mira no maior bloco no eixo X do nível com ângulo alto. Caso tenha 2 ou mais blocos com o mesmo tamanho em X, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo X e a mesma quantidade de vida, escolher o que esta mais abaixo no nível. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais alto (highHitLeftMostpig)
        float sizeX = -1.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.x > sizeX)) {
                sizeX = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
            }
        }

        if (sizeX != -1.0f) {
            List<Transform> blockLargerX = new List<Transform>();
            float blockLessLife = 11.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.x == sizeX)) {
                    blockLargerX.Add(b);
                    if (blockLessLife > b.GetComponent<ABBlock>()._currentLife) {
                        blockLessLife = b.GetComponent<ABBlock>()._currentLife;
                    }
                }
            }

            if (blockLargerX.Count == 1) {
                return angleLaunch(blockLargerX[0], true);
            } else {
                foreach (Transform b in blockLargerX) {
                    if (blockLessLife != b.GetComponent<ABBlock>()._currentLife) {
                        blockLargerX.Remove(b);
                    }
                }

                if (blockLargerX.Count == 1) {
                    return angleLaunch(blockLargerX[0], true);
                } else {
                    Transform blockLaunch = transform;
                    float hight = 100.0f;
                    foreach (Transform b in blockLargerX)
                        if (hight != b.transform.position.y)
                            blockLaunch = b;

                    return angleLaunch(blockLaunch, true);
                }
            }
        } else {
            return highHitLeftMostpig();
        }
    }

    public double lowHitLargerXBlock() { //Função que mira no maior bloco no eixo X do nível com ângulo baixo. Caso tenha 2 ou mais blocos com o mesmo tamanho em X, escolher aquele com menos vida. Caso tenha 2 ou mais blocos com mesmo tamanho no eixo X e a mesma quantidade de vida, escolher o que esta mais abaixo no nível. Obs. se o nível não tiver blocos, mirar no porco mais a esquerda no nível com ângulo mais baixo (lowHitLeftMostpig)
        float sizeX = -1.0f;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.x > sizeX)) {
                sizeX = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
            }
        }

        if (sizeX != -1.0f) {
            List<Transform> blockLargerX = new List<Transform>();
            float blockLessLife = 11.0f;
            foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
                if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.x == sizeX)) {
                    blockLargerX.Add(b);
                    if (blockLessLife > b.GetComponent<ABBlock>()._currentLife) {
                        blockLessLife = b.GetComponent<ABBlock>()._currentLife;
                    }
                }
            }

            if (blockLargerX.Count == 1) {
                return angleLaunch(blockLargerX[0], false);
            } else {
                foreach (Transform b in blockLargerX) {
                    if (blockLessLife != b.GetComponent<ABBlock>()._currentLife) {
                        blockLargerX.Remove(b);
                    }
                }

                if (blockLargerX.Count == 1) {
                    return angleLaunch(blockLargerX[0], false);
                } else {
                    Transform blockLaunch = transform;
                    float hight = 100.0f;
                    foreach (Transform b in blockLargerX)
                        if (hight != b.transform.position.y)
                            blockLaunch = b;

                    return angleLaunch(blockLaunch, false);
                }
            }
        } else {
            return lowHitLeftMostpig();
        }
    }

    //public void OnCollisionEnter(Collision collision) {
    //    Debug.Log(collision.collider.name);
    //}

    ////double highHitSupportBlock() {
    ////    List<List<int>> estruturas;
    ////    foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
    ////        if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0.0f)) {
    ////            //b.GetComponent<ABBlock>().get_Collider().isTrigger = true;
    ////            //b.GetComponent<ABBlock>().get_Collider().Size
    ////        }
    ////    }

    ////    return 0;
    ////}



    public double highHitLeftMostTNT() {  // Função que mira no bloco TNT mais a esquerda do nível, com ângulo de lançamento alto. Se o estado não tiver TNT, minar na base da estrtura mais alta do nível.
        float positionHightMostLeftTNT = 100f;
        Transform mostLeftTNT = transform;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            //b.GetComponent<ABBlock>().get_Collider().isTrigger = true;
            if ((b.GetComponent<ABTNT>() != null) && (b.GetComponent<ABTNT>()._currentLife > 0.0f) && (positionHightMostLeftTNT > b.transform.position.x)) {
                mostLeftTNT = b;
                positionHightMostLeftTNT = b.transform.position.x;
            }
        }

        if (positionHightMostLeftTNT != 100) {
            return angleLaunch(mostLeftTNT, true);
        } else {
            return 0;
        }
    }

    //private double checkTouching(List<List<ABBlock>> blocksTouching_, List<ABBlock> _blockTouching, ABBlock b_) {
    //    if (_blockTouching.Count == 0) {
    //        return 0;
    //    }
    //    return 0;
    //}

    ////struct LargestStructureClose {
        ////public int block;
        ////public int father;
        ////public float sizeY;

        ////public LargestStructureClose(int block_ = -1, int father_ = -1, float sizeY_ = -1.0f){
        ////    this.block = block_;
        ////    this.father = father_;
        ////    this.sizeY = sizeY_;
        ////}

    ////    public LargestStructureClose(LargestStructureClose l) {
    ////        this.block = l.block;
    ////        this.father = l.father;
    ////        this.sizeY = l.sizeY;
    ////        }
    ////}

    ////public double highHitbiggerStructureY() {
    ////public IEnumerator highHitbiggerStructureY() {
    ////    Debug.Log("TESTE_highHitbiggerStructureY_-0");
    ////    canFindBiggerStructure = false;
    ////    List<ABBlock> _listBlocks = ABGameWorld.Instance._blocks;
    ////    Debug.Log("TESTE_highHitbiggerStructureY_-1");
    ////    StartCoroutine(touchingBlocks(_listBlocks));
    //StartCoroutine(touchingBlocks());
    ////    Debug.Log("TESTE_highHitbiggerStructureY_-2");
    ////    yield return new WaitUntil(() => (canFindBiggerStructure));
    ////}

    private bool ListOfListeTheNumBlockCloseConteinsElemente(List<List<int>> blocksTouching_, int elem) {
       foreach (List<int> subL in blocksTouching_) {
            if (subL.Contains(elem)) {
                return true;
            }
        }

        return false;
    }

    public double highHitbiggerStructureY(List<List<int>> blocksTouching) {
        List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;

        List<int> open = new List<int>();
        List<List<int>> close = new List<List<int>>();

        int posBlockCurrent, sizeListClose = 0;

        for (int i = 0; i < blocks_.Count; i++) {
            if (!ListOfListeTheNumBlockCloseConteinsElemente(close, i)) {
                close.Add(new List<int>());
                open.Add(i);
                while (open.Count > 0) {
                    posBlockCurrent = open[0];
                    open.Remove(open[0]);
                    for (int l = 0; l < blocksTouching[posBlockCurrent].Count; l++) {
                        if (!open.Contains(blocksTouching[posBlockCurrent][l]) && !ListOfListeTheNumBlockCloseConteinsElemente(close, blocksTouching[posBlockCurrent][l])) {
                            open.Add(blocksTouching[posBlockCurrent][l]);
                        }
                    }
                    close[sizeListClose].Add(posBlockCurrent);
                }
                sizeListClose++;
            }
        }

        int contApagar = 0;
        foreach (List<int> subList in close) {
            foreach (int i in subList) {
                Debug.Log(i);
            }
            contApagar++;
        }

        canFindBiggerStructure = true;
        //Debug.Log("concluiu...");

        List<float> largerBlockY = new List<float>(), smallestBlockY = new List<float>();
        foreach (List<int> subListClose in close) {
            largerBlockY.Add(-100.0f);
            smallestBlockY.Add(100.0f);
        }

        int countStructure = 0;
        foreach (List<int> subListClose in close) {
            foreach(int i in subListClose) {
                float boundsBlock = blocks_[i].get_Collider().bounds.size.y;
                boundsBlock = boundsBlock / 2.0f;
                if (largerBlockY[countStructure] < (blocks_[i].transform.position.y + boundsBlock)) {
                    largerBlockY[countStructure] = blocks_[i].transform.position.y + boundsBlock;
                }
                if (smallestBlockY[countStructure] > (blocks_[i].transform.position.y - boundsBlock)) {
                    smallestBlockY[countStructure] = blocks_[i].transform.position.y - boundsBlock;
                }
            }
            countStructure++;
        }

        float largerStructure = 0.0f;
        int posLargerStructure = 0;
        for(int i = 0; i < countStructure; i++) {
            if (largerStructure < (largerBlockY[i] - smallestBlockY[i])) {
                largerStructure = largerBlockY[i] - smallestBlockY[i];
                posLargerStructure = i;
            }
        }

        Transform smallestBlockLargerStructure = transform;
        float smallesBlock = 100.0f;
        foreach (int i in close[posLargerStructure]) {
            if (smallesBlock > (blocks_[i].transform.position.y - (blocks_[i].get_Collider().bounds.size.y / 2))) {
                smallesBlock = (blocks_[i].transform.position.y - (blocks_[i].get_Collider().bounds.size.y / 2));
                smallestBlockLargerStructure = blocks_[i].transform;
            }
        }

        return angleLaunch(smallestBlockLargerStructure, true);
    }

    ////private IEnumerator touchingBlocks(List<ABBlock> _blocks_) {
    //private IEnumerator touchingBlocks() {
    ////Debug.Log("TESTE_touchingBlocks_-0");
    //List<ABBlock> _blocks_ = ABGameWorld.Instance._blocks;
    ////yield return new WaitForFixedUpdate();

    ////foreach (ABBlock b in _blocks_) {
    ////    if (b._currentLife > 0) {
    ////        b.GetComponent<Rigidbody2D>().isKinematic = false;
    ////    }
    ////}

    ////yield return new WaitForFixedUpdate();

    ////List<List<int>> directContactBetweenBlocks = new List<List<int>>();
    ////List<int> auxSubList;
    ////for(int i = 0; i < _blocks_.Count; i++) {
    ////    if (_blocks_[i]._currentLife > 0) {
    ////        auxSubList = new List<int>();
    ////        //Debug.Log("_blocks_[i].name = " + _blocks_[i].name + ", (" + _blocks_[i].transform.position.x + ", " + _blocks_[i].transform.position.y + ")");
    ////        for (int j = 0; j < _blocks_.Count; j++) {
    ////            if ((i != j) && _blocks_[j]._currentLife > 0) {
    ////                if (_blocks_[i].GetComponent<Collider2D>().IsTouching(_blocks_[j].GetComponent<Collider2D>())) {
    ////                    //Debug.Log(".    .   _blocks_[j].name = " + _blocks_[j].name + ", (" + _blocks_[j].transform.position.x + ", " + _blocks_[j].transform.position.y + ")");
    ////                    auxSubList.Add(j);
    ////                }
    ////            }
    ////        }
    ////        directContactBetweenBlocks.Add(auxSubList);
    ////        //break;
    ////    }
    ////}

    ////yield return new WaitForFixedUpdate();

    //int uApagarU = 0;
    //foreach (List<int> subList in directContactBetweenBlocks) {
    //    Debug.Log("uApagarU = " + uApagarU);
    //    foreach (int y in subList) {
    //        Debug.Log(".    .(" + y + ", " + uApagarU + ")");
    //    }
    //    uApagarU++;
    //}

    //yield return new WaitForFixedUpdate();

    ////foreach (ABBlock b in _blocks_) {
    ////    if (b._currentLife > 0) {
    ////        b.GetComponent<Rigidbody2D>().isKinematic = true;
    ////    }
    ////}

    ////yield return new WaitForFixedUpdate();

    //for(int i = 0; i < _blocks_.Count; i++) {
    //    Debug.Log("_blocks_[" + i + "]. name = " + _blocks_[i].name);
    //}

    ////    biggerStructure(directContactBetweenBlocks);
    ////    yield return new WaitUntil(() => (canFindBiggerStructure));
    ////}





    ////public void testTouch() {
    ////  canRetorn = false;
    ////  foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
    ////      if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
    ////          b.GetComponent<Rigidbody2D>().isKinematic = false;
    ////          Debug.Log("-00-b.GetComponent<Rigidbody2D>().isKinematic = " + b.GetComponent<Rigidbody2D>().isKinematic);
    ////      } 
    ////  }

    ////    foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
    ////        if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
    ////            foreach (Transform b1 in ABGameWorld.Instance._blocksTransform) {
    ////                if ((b1.GetComponent<ABBlock>() != null) && (b1.GetComponent<ABBlock>()._currentLife > 0)) {
    ////                    if (!b.Equals(b1)) {
    ////                        Debug.Log("-1-b.GetComponent<Rigidbody2D>().isKinematic = " + b.GetComponent<Rigidbody2D>().isKinematic);
    ////                        Debug.Log("-1-b1.GetComponent<Rigidbody2D>().isKinematic = " + b1.GetComponent<Rigidbody2D>().isKinematic);
    ////                        if (b.GetComponent<Collider2D>().IsTouching(b1.GetComponent<Collider2D>())){
    ////                            Debug.Log("Encostou");
    ////                        } else {
    ////                            Debug.Log("Nao Encostou...");
    ////                        }
    ////                    }
    ////                }
    ////            }
    ////        }
    ////    }

    ////    canRetorn = true;
    ////}















    ////double lowHitLargerYBlock() {
    ////    float sizeY = -100.0f;
    ////    List<Transform> blockLargerY = new List<Transform>();
    ////    foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
    ////        if (b.GetComponent<ABBlock>() != null) {
    ////            if (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y > sizeY) {
    ////                sizeY = b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y;
    ////                //blockLargerY = b;
    ////            }
    ////        }
    ////    }

    ////    if (sizeY != -100.0f) {
    ////        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
    ////            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>().GetComponent<BoxCollider2D>().size.y == sizeY)) {
    ////                blockLargerY.Add(b);
    ////            }
    ////        }

    ////        if (blockLargerY.Count == 1) {
    ////            return angleLaunch(blockLargerY[0].transform, false);
    ////        } else {
    ////            Transform blockLaunch = transform;
    ////            float LessDistance = 300.0f, auxDistance;
    ////            List<ABPig> _pigs = ABGameWorld.Instance._pigs;
    ////            foreach (Transform b in blockLargerY) {
    ////                foreach (ABPig p in _pigs) {
    ////                    auxDistance = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - p.transform.position.x), 2.0f) + Mathf.Pow((b.transform.position.y - p.transform.position.y), 2));
    ////                    if (LessDistance > auxDistance){
    ////                        LessDistance = auxDistance;
    ////                        blockLaunch = p.transform;
    ////                    }
    ////                }
    ////            }

    ////            return angleLaunch(blockLaunch, false);
    ////        }

    ////    } else {
    ////        List<ABPig> _pigs = ABGameWorld.Instance._pigs;
    //System.Random rnd = new System.Random();
    //int pos = rnd.Next(_pigs.Count);
    //return angleLaunch(_pigs[pos].transform, true);
    ////        return angleLaunch(_pigs[0].transform, false);
    ////    }
    ////}

    public double highHitBlockNearpig() {
        float smallestDistance = 100.0f, auxDistance;
        Transform blockNearPig = transform;
        foreach (Transform p in ABGameWorld.Instance._blocksTransform) {
            if ((p.GetComponent<ABPig>() != null) && (p.GetComponent<ABPig>()._currentLife > 0)) {
                foreach(Transform b in ABGameWorld.Instance._blocksTransform) {
                    if (!p.Equals(b) && (b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0)) {
                        auxDistance = Mathf.Sqrt(Mathf.Pow((p.transform.position.x - b.transform.position.x), 2.0f) + Mathf.Pow((p.transform.position.y - b.transform.position.y), 2));
                        if (smallestDistance > auxDistance) {
                            smallestDistance = auxDistance;
                            blockNearPig = b;
                        }
                    }
                }
            }
        }

        return angleLaunch(blockNearPig, true);
    }

    public Double highHitMoreHightCircle() {
        float hightCircle = -100.0f;
        Transform moreHightCircle = transform;
        bool haveBlockInLevel = false ;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && ((b.GetComponent<ABBlock>().name == "Circle") || (b.GetComponent<ABBlock>().name == "CircleSmall"))) {
                haveBlockInLevel = true;
                if (hightCircle < (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2))) {
                    hightCircle = (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2));
                    moreHightCircle = b;
                }
            }
        }

        if (!haveBlockInLevel) {
            return highHitLargerYBlock();
        } else {
            return angleLaunch(moreHightCircle, true);
        }
    }

    public Double highHitMorelowerCircle() {
        float hightCircle = 100.0f;
        Transform moreHightCircle = transform;
        bool haveBlockInLevel = false ;
        foreach (Transform b in ABGameWorld.Instance._blocksTransform) {
            if ((b.GetComponent<ABBlock>() != null) && (b.GetComponent<ABBlock>()._currentLife > 0) && ((b.GetComponent<ABBlock>().name == "Circle") || (b.GetComponent<ABBlock>().name == "CircleSmall"))) {
                haveBlockInLevel = true;
                if (hightCircle > (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2))) {
                    hightCircle = (b.GetComponent<ABBlock>().transform.position.y + (b.GetComponent<ABBlock>().get_Collider().bounds.size.y / 2));
                    moreHightCircle = b;
                }
            }
        }

        if (!haveBlockInLevel) {
            return highHitLargerXBlock();
        } else {
            return angleLaunch(moreHightCircle, true);
        }
    }

    public double tallestPigSupoerteBlock(List<List<int>> PigsTouchingBlocks) {
        List<ABPig> pigs_ = ABGameWorld.Instance._pigs;
        int posPigsTouchingBlocks = 0;
        float TallestPigLevel = -100.0f;
        int posBlockTouchingTallestPig = -1;
        foreach (List<int> subList in PigsTouchingBlocks) {
            if (subList.Count != 0) {
                //for(int i = 0; i < subList.Count; i++) {
                //    if (TallestPigLevel < (pigs_[subList[i]].transform.position.y + (pigs_[subList[i]].get_Collider().bounds.size.y /2))) {
                //        TallestPigLevel = (pigs_[subList[i]].transform.position.y + (pigs_[subList[i]].get_Collider().bounds.size.y / 2));
                //        posBlockTouchingTallestPig = i;
                //    }
                //}
                if (TallestPigLevel < (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2))) {
                    TallestPigLevel = (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2));
                    posBlockTouchingTallestPig = posPigsTouchingBlocks;
                }
            }
            posPigsTouchingBlocks++;
        }
        if(posBlockTouchingTallestPig == -1)
            return highHitHightMostpig();
        else {
            float Ypig, YBlock;
            List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;
            foreach (int b in PigsTouchingBlocks[posPigsTouchingBlocks]) {
                Ypig = (pigs_[posPigsTouchingBlocks].transform.position.y + (pigs_[posPigsTouchingBlocks].get_Collider().bounds.size.y / 2));
                YBlock = (blocks_[b].transform.position.y + (blocks_[b].get_Collider().bounds.size.y / 2));
                if (Math.Abs(Ypig - YBlock) < 0.1f) {
                    return angleLaunch(blocks_[b].transform, true);
                }
            }

            return highHitHightMostpig();
        }

        //List<ABBlock> blocks_ = ABGameWorld.Instance._blocks;

        ////List<int> open = new List<int>();
        ////List<List<int>> close = new List<List<int>>();

        ////int posBlockOrPigCurrent, sizeListClose = 0;

        ////for (int i = 0; i < pigs_.Count; i++) {
        ////    if (!elementBelongsSublists(close, i)) {
        ////        close.Add(new List<int>());
        ////        open.Add(i);
        ////        while (open.Count > 0) {
        ////            posBlockOrPigCurrent = open[0];
        ////            open.Remove(open[0]);
        ////            for (int l = 0; l < PigsTouchingBlocks[posBlockOrPigCurrent].Count; l++) {
        ////                if (!open.Contains(PigsTouchingBlocks[posBlockOrPigCurrent][l]) && !elementBelongsSublists(close, PigsTouchingBlocks[posBlockOrPigCurrent][l])) {
        ////                    open.Add(PigsTouchingBlocks[posBlockOrPigCurrent][l]);
        ////                }
        ////            }
        ////            close[sizeListClose].Add(posBlockOrPigCurrent);
        ////        }
        ////        sizeListClose++;
        ////    }
        ////}

        //int contApagar = 0;
        //foreach (List<int> subList in close) {
        //    foreach (int i in subList) {
        //        Debug.Log(i);
        //    }
        //    contApagar++;
        //}

        //return angleLaunch(smallestBlockLargerStructure, true);
    }
}//*/
