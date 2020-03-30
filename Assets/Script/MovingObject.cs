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

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
        scalez = transform.localScale.z;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.Q))
        {
            applyRunSpeed = runSpeed;//뛰는거 적용
        }
        else
        {
            applyRunSpeed = 0;
        }

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
