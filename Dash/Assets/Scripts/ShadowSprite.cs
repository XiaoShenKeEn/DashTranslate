using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]   //这里的目的就是为了限制残影显示的时间
    public float activeTime; //显示时间
    public float activeStart; //开始显示的时间点

    [Header("不透明度控制")]
    private float alpha;      //不透明度的数值
    public float alphaSet;   //不透明度的初始数值
    public float alphaMultiplier;   //乘积参数，使渐变变得更加自然

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();         
        playerSprite = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time; //显示时间应该等于当前时间点

    }
    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.5f,0.5f,1,alpha);

        thisSprite.color = color;  //设置颜色

        if (Time.time >= activeStart + activeTime)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
