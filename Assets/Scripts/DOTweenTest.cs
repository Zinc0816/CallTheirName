using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{

    Vector3 _enemyVec;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Vec=new Vector3(5,0,0);
        this.transform.DOLocalMove(Vec, 0.2f)
                        .SetRelative()
                        .SetEase(Ease.InOutElastic)
                        .SetDelay(1f)
                        .SetLoops(2,LoopType.Yoyo);        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButton(0)){
            this.transform.DOMove(new Vector3(3.0f,0f,0f), 2f)
                            .SetEase(Ease.InOutElastic)
                            .SetLoops(1,LoopType.Yoyo);
        }
        */
    }
}
