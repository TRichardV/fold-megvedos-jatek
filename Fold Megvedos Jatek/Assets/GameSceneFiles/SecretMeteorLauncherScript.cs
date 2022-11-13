using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SecretMeteorLauncherScript : MonoBehaviour {

    readonly int defPrepTimeSec = 5; // 5 sec
    readonly int defWaveTimeSec = 5; // 5 sec

    public GameObject meteor;
    Transform LauncherP;

    int waveNumber = 0;

    int meteorNumberMax = 5;

    int preparationTimeMax;
    int preparationTimeCounter = 0;

    int waveTimeMax;
    int waveTimeCounter = 0;

    List<float[]> meteorSpawns = new List<float[]>();

    readonly float maxX = 2.2f;

    void Start() {

        LauncherP = this.GetComponent<Transform>();

        preparationTimeMax = (int)(1 / Time.fixedDeltaTime * defPrepTimeSec);
        waveTimeMax = (int)(1 / Time.fixedDeltaTime * defWaveTimeSec);

    }
    void createMeteor (float desX, float desY, float posX, float posY) {

        GameObject obj = Instantiate(meteor);

        obj.transform.parent = this.gameObject.transform;

        obj.transform.position = new Vector2(posX, posY);

        obj.GetComponent<MeteorScript>().desX = desX;
        obj.GetComponent<MeteorScript>().desY = desY;

    }

    void FixedUpdate() {

        GameObject.FindGameObjectsWithTag("c.wavecounter")[0].gameObject.GetComponent<TextMeshProUGUI>().text = "Wave: " + waveNumber + "\n" + waveTimeCounter + "\n" + preparationTimeCounter;

        if (preparationTimeCounter == -1) { // SPAWN METEORS

            waveTimeCounter++;

            if (waveTimeCounter >= waveTimeMax) {

                preparationTimeCounter++;
                waveTimeCounter = 0;

                waveNumber++;

            }

            for (int i = 0; i < meteorSpawns.Count; i++) {

                if (waveTimeCounter == meteorSpawns[i][0]) {

                    createMeteor(meteorSpawns[i][1], 0, meteorSpawns[i][1], LauncherP.position.y);

                }

            }


        }
        else { // PREP PHASE

            if (preparationTimeCounter == 0) {

                meteorSpawns.Clear();

                meteorNumberMax = (int) Math.Round(meteorNumberMax * 1.5f);
                waveTimeMax = (int) Math.Round(waveTimeMax * 1.3f);

                for (int i = 0; i < meteorNumberMax; i++) {

                    float[] details = new float[2];

                    details[0] = Random.Range(0, waveTimeMax);
                    details[1] = Random.Range(-maxX, maxX);

                    meteorSpawns.Add(details);

                }

                Debug.Log(meteorSpawns.Count);

            }

            preparationTimeCounter++;

            if (preparationTimeCounter >= preparationTimeMax) {

                preparationTimeCounter = -1;

            }

        }

    }

}