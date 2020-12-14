using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI 사용
using UnityEngine.SceneManagement;//씬 전환 사용

public class LoadingBar : MonoBehaviour
{
    public Slider progressbar;//슬라이더 선언

    private void Start()
    {
        StartCoroutine(LoadScene()); //LoadScene클래스 실행
    }
    //다음 씬의 준비가 끝난 뒤 실행하는 클래스
    IEnumerator LoadScene()
    {
        //다음 씬을 SampleScene으로 설정
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
        operation.allowSceneActivation = false;//씬 전환 대기
        while (!operation.isDone)//다음 씬이 준비가 안되었다면 실행. 준비 될 때까지 반복.
        {
            yield return null;
            if (progressbar.value < 0.9f)//슬라이더가 0.9보다 낮으면
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);//슬라이더 증가
            }
            else if (operation.progress >= 0.9f)//슬라이더가 0.9와 같거나 커지면
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);//슬라이더를 1로 증가
            }

            if (progressbar.value >= 1f && operation.progress >= 0.9f)//슬라이더가 1과 같거나 커지고 다음 씬의 준비가 되었다면
            {
                operation.allowSceneActivation = true;//다음 씬으로 전환
            }
        }
    }
}
