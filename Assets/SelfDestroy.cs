using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem particle;
    
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        //GunTrans = GameObject.Find("Cube");
    }

    void Update()
    {
        if (particle.isStopped) //パーティクルが終了したか判別
        {
            Destroy(this.gameObject);//パーティクル用ゲームオブジェクトを削除
        }

        
    }
}
