using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    CanvasGroup canvasGroup; //알파값 조절용
    public float fadeTime = 2f;
    Camera loadingCam;
    public Transform camTarget;
    private void Awake()
    {
        loadingCam = transform.parent.GetComponent<Camera>();
        DontDestroyOnLoad(loadingCam); //씬넘어갈때 파괴 안되게
        canvasGroup = GetComponent<CanvasGroup>();
        camTarget = Camera.main.transform; //메인카메라 = 플레이어의 카메라 
    }

    private void Update()
    {
        if (camTarget != null) //타겟이 비어있지 않은 이상 타겟과 같은곳을 바라보게 한다
        {
            loadingCam.transform.position = camTarget.transform.position;
            loadingCam.transform.rotation = camTarget.transform.rotation;
        }
    }


    public IEnumerator Fade(bool isOut)
    {
        float startAlpha = isOut ? 0 : 1; //bool에 따라서 시작알파값과 종료 알파값을 설정
        float endAlpha = isOut ? 1 : 0;
        float timer = 0;  //0부터 시간 계산
        canvasGroup.alpha = startAlpha;
        while (true) //프레임 단위로 계속 반복
        {
            timer += Time.deltaTime;//1프레임만큼의 시간을 타이머에 더해줌 
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer/ fadeTime);
            //시작 알파값에서 종료 알파값 사이의 값을 지금 지나간시간 / 총시간의 비율만큼 적용
            if (timer >= fadeTime) break; //시간이 되면 반복에서 빠져나감
            yield return null; //한프레임 뒤에 다음 루프
        }
        canvasGroup.alpha = endAlpha; //끝에 확실하게 알파값을 지정
        if (canvasGroup.alpha == 0)
        {
            loadingCam.gameObject.SetActive(false); //로딩이 끝나면 카메라를 끈다
        }
    }

}
