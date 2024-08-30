using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("ʱ����Ʋ���")]   //�����Ŀ�ľ���Ϊ�����Ʋ�Ӱ��ʾ��ʱ��
    public float activeTime; //��ʾʱ��
    public float activeStart; //��ʼ��ʾ��ʱ���

    [Header("��͸���ȿ���")]
    private float alpha;      //��͸���ȵ���ֵ
    public float alphaSet;   //��͸���ȵĳ�ʼ��ֵ
    public float alphaMultiplier;   //�˻�������ʹ�����ø�����Ȼ

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

        activeStart = Time.time; //��ʾʱ��Ӧ�õ��ڵ�ǰʱ���

    }
    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.5f,0.5f,1,alpha);

        thisSprite.color = color;  //������ɫ

        if (Time.time >= activeStart + activeTime)
        {
            //���ض����
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
