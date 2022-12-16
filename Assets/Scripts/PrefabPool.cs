using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 使用 PrefabPool.instance 即可使用創建好的 Prefab 
 */
public class PrefabPool : MonoBehaviour
{
    // Start is called before the first frame update

    public static PrefabPool instance { get; private set; }



    public GameObject Goblin;

    public GameObject Axe;

    public GameObject Tornado;


    void Start()
    {
        instance = this;
    }

}
