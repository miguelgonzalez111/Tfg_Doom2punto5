using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slayerAppareance : MonoBehaviour{

    public Material[] slayerMaterials;

    // Start is called before the first frame update
    void Start(){
        SkinnedMeshRenderer myRenderer = GetComponent<SkinnedMeshRenderer>();
        myRenderer.material = slayerMaterials[Random.Range(0,slayerMaterials.Length)];
    }

    // Update is called once per frame
    void Update(){
        
    }
}
