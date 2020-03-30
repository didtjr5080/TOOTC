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

    public GameObject 기본칼;
    public GameObject 공격준비칼;

    public float DrowScale;//드로즈 스캐일 값 

    public float AttackDelay;//공격속도 변수
    public bool DoAttackDelay;
    public float SetAttackDelay;

    public float CoolSkill1;//1번 스킬 쿨타임
    public bool DoSkill1; //스킬1 지속
    public bool CoolOnSkill1;//스킬1 쿨온
    public float Skill1SetCool;//스킬1 쿨타임 설정
    public float OnSkill1Time;//스킬1 지속시간
    public float SetOnSkill1Time;//스킬1 지속시간설정
    public bool SendDoSkill1;
    public bool OnSkill1;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        BasicRotation = transform.rotation.z;
        OnAtack = false;
        DoneMaxRotation = false;
        CallDrrowsScale();
        DoAttackDelay = false;
        SetAttackDelay = 1;

        Skill1SetCool = 7;//스킬1 쿨 설정
        SetOnSkill1Time = 3;
        DoSkill1 = false;
        CoolOnSkill1 = false;
        SendDoSkill1 = false;
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
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                Rotaion = 0;
            }
           
        }


        //스킬1실행
        if (Input.GetKey(KeyCode.Z))
        {
            AttackReady();
            AttackReadyCool();
        }

        AttackReadyCool();

        //공격합수 반복
        if (OnAtack==true)
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

        if (OnSkill1 == true)
        {
            기본칼.SetActive(false);
            공격준비칼.SetActive(true);
            
        }

        if (OnSkill1 == false)
        {
            기본칼.SetActive(true);
            공격준비칼.SetActive(false);
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





    void AttackReady()//스킬1 함수
    {
        if (CoolOnSkill1 == false)//스킬1 실행
        {
            CoolSkill1 = Skill1SetCool;//스킬1 쿨 설정
            DoSkill1 = true;//스킬1 지속 on
            CoolOnSkill1 = true;//스킬1 쿨 on
            OnSkill1Time = SetOnSkill1Time;//스킬1 지속시간 설정
            SendDoSkill1 = true;
        }
    }

    void AttackReadyCool()
    {

        //스킬 1 지속 시간 설정
        if (DoSkill1 == true)
        {
            OnSkill1Time -= Time.deltaTime;
            OnSkill1 = true;
        }
        else
        {

        }

        if (OnSkill1Time <= 0)
        {
            DoSkill1 = false;
            OnSkill1Time = 0;
            SendDoSkill1 = false;
            OnSkill1 = false;
        }

        //스킬1 쿨타임 설정
        if (CoolOnSkill1 == true)
        {
            CoolSkill1 -= Time.deltaTime;
        }

        if (CoolSkill1 <= 0)
        {
            CoolOnSkill1 = false;
            CoolSkill1 = 0;
        }
    }

}
