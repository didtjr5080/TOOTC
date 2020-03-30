using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour{

    public float speed;//기본 이동속도

    private Vector3 vector;

    public float runSpeed;//뛰는 속도
    private float applyRunSpeed;

    private float scalex;
    private float scaley;
    private float scalez;

    public float CoolSkill1;//1번 스킬 쿨타임
    public bool DoSkill1; //스킬1 지속
    public bool CoolOnSkill1;//스킬1 쿨온
    public float Skill1SetCool;//스킬1 쿨타임 설정
    public float OnSkill1Time;//스킬1 지속시간
    public float SetOnSkill1Time;//스킬1 지속시간설정

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
        scalez = transform.localScale.z;

        Skill1SetCool = 7;//스킬1 쿨 설정
        SetOnSkill1Time = 3;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //스킬1실행
        if (Input.GetKey(KeyCode.Q))
        {
            if (CoolOnSkill1 == false)//스킬1 실행
            {
                CoolSkill1 = Skill1SetCool;//스킬 쿨 설정
                DoSkill1 = true;//스킬 지속 on
                CoolOnSkill1 = true;//스킬 지속 off
                OnSkill1Time = SetOnSkill1Time;//스킬 지속시간 설정
                applyRunSpeed = runSpeed;//뛰는거 적용
            }
            
        }

        //스킬 1 지속 시간 설정
        if (DoSkill1 == true)
        {
            OnSkill1Time -= Time.deltaTime;
        }
        else
        {
            
            applyRunSpeed = 0;
        }

        if (OnSkill1Time <= 0)
        {
            DoSkill1 = false;
            OnSkill1Time = 0;
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

        //이동 부분
        if (Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") !=0 ){

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            animator.SetBool("walking",true);//걷는 애니메이션

            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);//x값 변환
                if (vector.x < 0)
                {
                    transform.localScale = new Vector3(-scalex, scaley, scalez);//좌우반전(좌)
                }
                else if (vector.x > 0)
                {
                    transform.localScale = new Vector3(scalex, scaley, scalez);//좌우반전(우)
                }
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);//y값 변환
            }

        }

        else
        {
            animator.SetBool("walking", false);//서 있는 애니메이션
        }

    }

}
