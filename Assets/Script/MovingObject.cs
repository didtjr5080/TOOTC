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

    public float CoolSkill3;//3번 스킬 쿨타임
    public bool DoSkill3; //스킬3 지속
    public bool CoolOnSkill3;//스킬3 쿨온
    public float Skill3SetCool;//스킬3 쿨타임 설정
    public float OnSkill3Time;//스킬3 지속시간
    public float SetOnSkill3Time;//스킬3 지속시간설정

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
        scalez = transform.localScale.z;

        Skill3SetCool = 7;//스킬3 쿨 설정
        SetOnSkill3Time = 3;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //스킬3실행
        if (Input.GetKey(KeyCode.C))
        {
            TouchOfDevil();
        }

        TouchOfDevilCool();//스킬3 쿨타임, 지속시간

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

    void TouchOfDevil()//스킬3 함수
    {
        if (CoolOnSkill3 == false)//스킬3 실행
        {
            CoolSkill3 = Skill3SetCool;//스킬3 쿨 설정
            DoSkill3 = true;//스킬3 지속 on
            CoolOnSkill3 = true;//스킬3 지속 off
            OnSkill3Time = SetOnSkill3Time;//스킬3 지속시간 설정
            applyRunSpeed = runSpeed;//뛰는거 적용
        }
    }

    void TouchOfDevilCool()
    {

        //스킬 3 지속 시간 설정
        if (DoSkill3 == true)
        {
            OnSkill3Time -= Time.deltaTime;
        }
        else
        {

            applyRunSpeed = 0;
        }

        if (OnSkill3Time <= 0)
        {
            DoSkill3 = false;
            OnSkill3Time = 0;
        }

        //스킬3 쿨타임 설정
        if (CoolOnSkill3 == true)
        {
            CoolSkill3 -= Time.deltaTime;
        }

        if (CoolSkill3 <= 0)
        {
            CoolOnSkill3 = false;
            CoolSkill3 = 0;
        }
    }
}

    

        
