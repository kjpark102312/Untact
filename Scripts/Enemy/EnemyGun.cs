using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러
    public LineRenderer camLineRenderer;

    public Transform fireTransform;

    private float fireDistance = 25f;

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    private float lastFireTime; // 총을 마지막으로 발사한 시점

    Enemy enemy;

    private void Start() 
    {
        enemy = GetComponent<Enemy>();

    }

    public void Fire()
    {
        // 현재 상태가 발사 가능한 상태
        // && 마지막 총 발사 시점에서 timeBetFire 이상의 시간이 지남
        if (enemy.state == Enemy.EnemyAnimState.Attack && Time.time >= lastFireTime + timeBetFire )
        {
            // 마지막 총 발사 시점을 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
        }
    }
    
    // 실제 발사 처리
    private void Shot(Vector3 _camHitPosition)
    {
        // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        RaycastHit hit;
        // 총알이 맞은 곳을 저장할 변수
        Vector3 hitPosition = Vector3.zero;

        // 레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(fireTransform.position,
            fireTransform.forward, out hit, fireDistance))
        {
            // 레이가 어떤 물체와 충돌한 경우

            // 충돌한 상대방으로부터 IDamageable 오브젝트를 가져오기 시도
            
            // 레이가 충돌한 위치 저장
            hitPosition = hit.point;
        }
        else
        {
            // 레이가 다른 물체와 충돌하지 않았다면
            // 총알이 최대 사정거리까지 날아갔을때의 위치를 충돌 위치로 사용
            hitPosition = hit.point;
        }

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));

        // 남은 탄환의 수를 -1
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 총구 화염 효과 재생
        //muzzleFlashEffect.Play();
        // 탄피 배출 효과 재생
        //shellEjectEffect.Play();

        // 총격 소리 재생
        //gunAudioPlayer.PlayOneShot(shotClip);

        // 선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // 선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);
        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;
        

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

}
