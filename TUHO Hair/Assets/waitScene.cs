using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waitScene : MonoBehaviour
{

    public Camera mainCamera; // Gắn camera chính vào đây trong Unity Inspector
    public float zoomDuration = 1.0f; // Thời gian để thực hiện hiệu ứng zoom
    public float maxZoom = 30.0f; // Mức độ zoom tối đa
    private void Awake()
    {
        LoadSceneWithZoom("Menu");
    }

    public void LoadSceneWithZoom(string sceneName)
    {
        StartCoroutine(ZoomAndLoadScene(sceneName));
    }

    private IEnumerator ZoomAndLoadScene(string sceneName)
    {
        float startTime = Time.time;
        float startSize = mainCamera.orthographicSize;

        // Hiệu ứng zoom lên từ từ
        while (Time.time < startTime + zoomDuration)
        {
            float elapsed = Time.time - startTime;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, maxZoom, elapsed / zoomDuration);
            yield return null;
        }

        // Đảm bảo camera đạt kích thước tối đa trước khi chuyển scene
        mainCamera.orthographicSize = maxZoom;

        // Chờ thêm 1 giây rồi chuyển scene
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
