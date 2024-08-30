using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;  //单例模式
    public GameObject shadowPrefab;
    public int shadowCount;              //我需要我的对象池里面有多少个预制体

    public Queue<GameObject> availableObject = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        //初始化对象池
        FillPool();
    }
    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);   //生成项目就会成为当前项目的子级

            //取消启用 返回对象池
            ReturnPool(newShadow);
        }
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObject.Enqueue(gameObject);  //添加到队列的末端
    }
    public GameObject GetFormPool()
    {
        if (availableObject.Count == 0)  //预防池子里的对象不够用
        {
            FillPool(); 
        }
        var outShadow = availableObject.Dequeue();//从队列的开头获得gameObject
        outShadow.SetActive(true);
        return outShadow;
    }
}
