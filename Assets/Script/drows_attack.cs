using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drows_attack : MonoBehaviour
{
    public float BasicRotation;//기본 로테이션 값
    public float Rotaion;//로테이션값
    public float RightMaxRotaion;//최대 로테이션값(오른쪽)
    public float LeftMaxRotaion;//최대 로테이션값(오른쪽)
    public bool DoneMaxRotation;//최대치에 도달했는지 감지

    public bool OnAtack;//공격감지

    public float DrowScale;//드로즈 스캐일 값 

    public float AttackDelay;//공격속도 변수
    public bool DoAttackDelay;
    public float SetAttackDelay;

    // Start is called before the first frame update
    void Start()
    {
        BasicRotation = transform.rotation.z;
        OnAtack = false;
        DoneMaxRotation = false;
        CallDrrowsScale();
        DoAttackDelay = false;
        SetAttackDelay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var DrowsScript = GameObject.Find("drows").GetComponent<MovingObject>();
        DrowScale = DrowsScript.SendScaleX;
        //드로즈 이동 스크립트에서 변수불러오는함수 실행
        CallDrrowsScale();

        //기본 공격 실행
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (DoAttackDelay == false)
            {
                AttackDelay = SetAttackDelay;
                if (OnAtack == false)
                {
                    OnAtack = true;
                    DoAttackDelay = true;
                }
            }
            
           
        }
        //공격합수 반복
        if(OnAtack==true)
        {
            attack();
            print("OnAtack:");
            print(OnAtack);
            if (DrowScale > 0)
            {
                RightGoAddRotation();
                RightBackAddRotation();
            }

            else if (DrowScale < 0)
            {
                LeftGoAddRotation();
                LeftBackAddRotation();
            }

        }

        if (DoAttackDelay == true)
        {
            AtttackDelayFuc();
        }

    }

    //공격속도 관련 함수
    void AtttackDelayFuc()
    {
        if (DoAttackDelay == true)
        {
            AttackDelay -= Time.deltaTime;
        }

        if(AttackDelay < 0)
        {
            AttackDelay = 0;
            DoAttackDelay = false;
        }
    }


    //칼 각도 전환 관련 함수
    void attack()
    {
        transform.rotation = Quaternion.Euler(0,0,Rotaion);
    }

     void RightGoAddRotation()
    {
        if (DoneMaxRotation==false)
        {
            Rotaion--;
        }
        
        if (Rotaion < RightMaxRotaion)
        {
            DoneMaxRotation = true;
        }
    }

    void RightBackAddRotation()
    {
        if (DoneMaxRotation == true)
        {
            Rotaion++;
        }

        if (Rotaion > BasicRotation)
        {
            DoneMaxRotation = false;
            OnAtack = false;
        }
    }

    void LeftGoAddRotation()
    {
        if (DoneMaxRotation == false)
        {
            Rotaion++;
        }

        if (Rotaion > LeftMaxRotaion)
        {
            DoneMaxRotation = true;
        }
    }

    void LeftBackAddRotation()
    {
        if (DoneMaxRotation == true)
        {
            Rotaion--;
        }

        if (Rotaion < BasicRotation)
        {
            DoneMaxRotation = false;
            OnAtack = false;
        }
    }

    void CallDrrowsScale()
    {
        var DrowsScript= GameObject.Find("drows").GetComponent<MovingObject>();
        DrowScale = DrowsScript.SendScaleX;
    }

}
