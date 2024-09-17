using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //빛을 싹 캐싱해오는 함수. 특정 태그를 가진 빛들만 제외하고 받아온 후, 빨간색으로 빛을 바꿔줌... 후에 깜빡이는 것도 추가할 예정

    private List<Light> pointLights;
    public List<Light> excludedLights;  // 제외할 Point Light 리스트

    public float blinkInterval = 1.0f; // 깜빡거리는 간격


    void Start()
    {
        pointLights = new List<Light>();
        excludedLights = new List<Light>();  // 제외할 Point Light 리스트 초기화
        Light[] allLights = FindObjectsOfType<Light>();

        foreach (Light light in allLights)
        {
            if (light.type == LightType.Point)
            {
                pointLights.Add(light);

                //특정 태그를 가진 빛을 제외
                if (light.CompareTag("ExcludeLight"))
                {
                    excludedLights.Add(light);
                }
            }
        }

        ChangeAllPointLightsExceptExcluded(Color.red);
    }

    // 제외된 Point Light를 제외하고 나머지의 색을 변경하는 함수 + 깜빡거리게

    public void ChangeAllPointLightsExceptExcluded(Color newColor)
    {
        foreach (Light light in pointLights)
        {
            // 제외된 Light 리스트에 포함되지 않은 경우에만 색 변경
            if (!excludedLights.Contains(light))
            {
                light.color = newColor;
            }
        }

        StartCoroutine(BlinkLightsIntensity());

    }

    public void TurnOffAllColor()
    {
        foreach (Light light in pointLights)
        {
            // 제외된 Light 리스트에 포함되지 않은 경우에만 색 변경
            if (!excludedLights.Contains(light))
            {
                light.color = Color.black;
            }
        }
    }




    IEnumerator BlinkLightsIntensity()
    {
        while (true)
        {
            // 강도를 0으로 하여 끔
            foreach (Light light in pointLights)
            {
                light.intensity = 0;
            }
            yield return new WaitForSeconds(blinkInterval); // 꺼짐 상태 유지

            // 강도를 1로 하여 켬
            foreach (Light light in pointLights)
            {
                light.intensity = 1;
            }
            yield return new WaitForSeconds(blinkInterval); //켜짐 상태 유지
        }
    }

}
