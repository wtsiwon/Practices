using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float hp;
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            ApplyDamage(value);
        }
    }

    private const float ColorWaitTime = 0.1f;
    private const float textMoveTime = 5f;

    private GameObject damageText => Resources.Load<GameObject>("Prefabs/DamageText");
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();

    public void ApplyDamage(float damage)
    {
        StartCoroutine(CDamageEffect(damage));
    }

    private IEnumerator CDamageEffect(float damage)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(ColorWaitTime);
        spriteRenderer.color = Color.white;

        GameObject text = Instantiate(damageText, transform.position, Quaternion.identity);
        text.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        StartCoroutine(CTextEffect(text));
        yield return new WaitForSeconds(textMoveTime);

        Destroy(text.gameObject);
    }

    private IEnumerator CTextEffect(GameObject textEffect)
    {
        float time = 0;
        TextMeshProUGUI text = textEffect.gameObject.GetComponent<TextMeshProUGUI>();

        while (time < textMoveTime)
        {
            time += Time.deltaTime;
            textEffect.transform.position += Vector3.up * Time.deltaTime;

            Color color = text.color;
            color.a -= Time.deltaTime * 0.5f;
            text.color = color;

            yield return new WaitForFixedUpdate();
        }
    }
}
