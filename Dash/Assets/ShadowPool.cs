using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;  //����ģʽ
    public GameObject shadowPrefab;
    public int shadowCount;              //����Ҫ�ҵĶ���������ж��ٸ�Ԥ����

    public Queue<GameObject> availableObject = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        //��ʼ�������
        FillPool();
    }
    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);   //������Ŀ�ͻ��Ϊ��ǰ��Ŀ���Ӽ�

            //ȡ������ ���ض����
            ReturnPool(newShadow);
        }
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObject.Enqueue(gameObject);  //��ӵ����е�ĩ��
    }
    public GameObject GetFormPool()
    {
        if (availableObject.Count == 0)  //Ԥ��������Ķ��󲻹���
        {
            FillPool(); 
        }
        var outShadow = availableObject.Dequeue();//�Ӷ��еĿ�ͷ���gameObject
        outShadow.SetActive(true);
        return outShadow;
    }
}
