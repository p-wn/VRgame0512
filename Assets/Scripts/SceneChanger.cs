using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public GameObject loadingCam;//로딩 스크린이 있는 카메라를 연결
    LoadingScreen loadingScreen; //로딩스크린 스크립트

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        loadingScreen = loadingCam.GetComponentInChildren<LoadingScreen>();
    }

    public void SceneMove(string nextSceneName)
    {
        StartCoroutine(LoadProcess(nextSceneName)); //로딩 진행 코루틴 실행 
    }

    IEnumerator LoadProcess(string sceneName)  //로딩 진행
    {
        loadingCam.SetActive(true);//로딩용 카메라를 키고
        yield return StartCoroutine(loadingScreen.Fade(true));
        //로딩스크린 코루틴이 끝나야 아래로 진행됨

        //로딩 시작
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); //비동기 씬 로드
        op.allowSceneActivation = false; //로드씬이 끝나더라도 바로 이동하지않음

        //로딩 중
        while (!op.isDone) //로딩작업이 완료될때까지 대기
        {
            if (op.progress >= 0.9f) //만약 로딩 바 같은걸 만들경우 op.progress를 image.fillAmount에 적용할 수 있다 
            {
                op.allowSceneActivation = true; //씬 이동 활성화
                break;
            }
            yield return null;
        }

        //로딩 끝
        while (true)
        {
            if (SceneManager.GetActiveScene().name == sceneName) break; //확실하게 씬이 이동된걸 씬이름 일치를통해 확인
            yield return null;
        }
        loadingCam.transform.position = Camera.main.transform.position; //camtarget 추적하는 update는 실행순서가 늦기때문에
        loadingCam.transform.rotation = Camera.main.transform.rotation; //먼저 위치를 이동시킴
        loadingScreen.camTarget = Camera.main.transform; //새로운 씬의 메인카메라로 설정

        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(loadingScreen.Fade(false)); //로딩 스크린 사라짐
    }

}
