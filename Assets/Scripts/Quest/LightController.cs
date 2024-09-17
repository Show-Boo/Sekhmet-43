using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //���� �� ĳ���ؿ��� �Լ�. Ư�� �±׸� ���� ���鸸 �����ϰ� �޾ƿ� ��, ���������� ���� �ٲ���... �Ŀ� �����̴� �͵� �߰��� ����

    private List<Light> pointLights;
    public List<Light> excludedLights;  // ������ Point Light ����Ʈ

    public float blinkInterval = 1.0f; // �����Ÿ��� ����


    void Start()
    {
        pointLights = new List<Light>();
        excludedLights = new List<Light>();  // ������ Point Light ����Ʈ �ʱ�ȭ
        Light[] allLights = FindObjectsOfType<Light>();

        foreach (Light light in allLights)
        {
            if (light.type == LightType.Point)
            {
                pointLights.Add(light);

                //Ư�� �±׸� ���� ���� ����
                if (light.CompareTag("ExcludeLight"))
                {
                    excludedLights.Add(light);
                }
            }
        }

        ChangeAllPointLightsExceptExcluded(Color.red);
    }

    // ���ܵ� Point Light�� �����ϰ� �������� ���� �����ϴ� �Լ� + �����Ÿ���

    public void ChangeAllPointLightsExceptExcluded(Color newColor)
    {
        foreach (Light light in pointLights)
        {
            // ���ܵ� Light ����Ʈ�� ���Ե��� ���� ��쿡�� �� ����
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
            // ���ܵ� Light ����Ʈ�� ���Ե��� ���� ��쿡�� �� ����
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
            // ������ 0���� �Ͽ� ��
            foreach (Light light in pointLights)
            {
                light.intensity = 0;
            }
            yield return new WaitForSeconds(blinkInterval); // ���� ���� ����

            // ������ 1�� �Ͽ� ��
            foreach (Light light in pointLights)
            {
                light.intensity = 1;
            }
            yield return new WaitForSeconds(blinkInterval); //���� ���� ����
        }
    }

}
