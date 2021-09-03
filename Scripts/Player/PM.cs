using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpPower;
    private bool isJumping;
    private bool is3d;
    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    // [SerializeField]
    // private Camera theCamera;
    private Rigidbody myRigid;

    public GameObject target1;
    // public GameObject teleportTarget;

    public TalkManager talkManager;
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();  // private
        isJumping = false;
        is3d = false;
        talkManager = GetComponent<TalkManager>();
    }

    void Update()  // 컴퓨터마다 다르지만 대략 1초에 60번 실행
    {
        Move();                 // 1️⃣ 키보드 입력에 따라 이동
        //CameraRotation();       // 2️⃣ 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
        CharacterRotation();    // 3️⃣ 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전 
        Jump();
        Translate_Position();

        /*if(Input.GetKeyDown(KeyCode.Z) && talkManager.scanObject !=null)
        {
            talkManager.Action(talkManager.scanObject);
        }*/
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = 10f;
        }
        else
        {
            walkSpeed = 5f;
        }
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                is3d = false;
                myRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else
            {
                
                return;
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        // if (col.gameObject.CompareTag("2DT"))
        // {
        //     isJumping = false;
        //     is3d = true;
        // }
        // if (col.gameObject.CompareTag("Teleport"))
        // {
        //     gameObject.transform.position = teleportTarget.transform.position;
        // }
    }

    // private void CameraRotation()
    // {
    //     float _xRotation = Input.GetAxisRaw("Mouse Y");
    //     float _cameraRotationX = _xRotation * lookSensitivity;

    //     currentCameraRotationX -= _cameraRotationX;
    //     currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

    //     theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    // }

    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }
    private void Translate_Position()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(is3d)
            {
                gameObject.transform.position = target1.transform.position;
                is3d = false;
            }
            else
            {
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Talkable"))
            TalkManager.Instance.Action(other.gameObject);
    }
}