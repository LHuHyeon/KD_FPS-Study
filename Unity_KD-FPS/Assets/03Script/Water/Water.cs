using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float waterDrag;   // 물속 중력
    private float originDrag;

    [SerializeField] private Color waterColor;      // 물속 색깔
    [SerializeField] private float waterFogDensity; // 물 탁함 정도

    [SerializeField] private Color waterNightColor; // 밤 물속 색깔
    [SerializeField] private float waterNightFogDensity; // 밤 물 탁함 정도

    private Color originColor;      // 기본 색깔
    private float originFogDensity; // 기본 탁함 정도

    [SerializeField] private Color originNightColor;        // 기본 밤 색깔
    [SerializeField] private float originNightFogDensity;   // 기본 밤 탁함 정도

    [SerializeField] private string sound_WaterOut;
    [SerializeField] private string sound_WaterIn;
    [SerializeField] private string sound_Breathe;

    [SerializeField] private float breatheTime;
    private float currentBreatheTime;
    
    void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;

        originDrag = 0;
    }

    void Update() {
        if (GameManager.isWater){
            currentBreatheTime += Time.deltaTime;
            if (currentBreatheTime >= breatheTime){
                SoundManager.instance.PlaySE(sound_Breathe);
                currentBreatheTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player")){
            GetWater(other);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag("Player")){
            GetOutWater(other);
        }
    }

    private void GetWater(Collider _player)
    {
        SoundManager.instance.PlaySE(sound_WaterIn);

        GameManager.isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;

        if(!GameManager.isNight){
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterFogDensity;
        }
        else{
            RenderSettings.fogColor = waterNightColor;
            RenderSettings.fogDensity = waterNightFogDensity;
        }
    }

    private void GetOutWater(Collider _player)
    {
        if (GameManager.isWater){
            SoundManager.instance.PlaySE(sound_WaterOut);
            
            GameManager.isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;

            if(!GameManager.isNight){
                RenderSettings.fogColor = originColor;
                RenderSettings.fogDensity = originFogDensity;
            }
            else{
                RenderSettings.fogColor = originNightColor;
                RenderSettings.fogDensity = originNightFogDensity;
            }
        }
    }
}