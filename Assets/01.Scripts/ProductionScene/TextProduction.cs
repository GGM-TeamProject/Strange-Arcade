using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextProduction : MonoBehaviour
{
    public TMP_Text text;
    public GameObject cat;

    private string m_text = "이제 오락실은 내꺼다냥....";
    private string m_text2 = "난 다시 돌아올 것이다.";

    Vector3 speed = Vector3.zero;

    public GameObject white1, white2, black1, black2;
    private void Start()
    {
        cat.SetActive(false);
        StartCoroutine(OpeningProduction());
        StartCoroutine(EndingProduction());
    }

    IEnumerator OpeningProduction()
    {
        yield return StartCoroutine(Typing_1(m_text));
        cat.SetActive(true);
        cat.transform.position = new Vector3(0, -25, 10);
        StartCoroutine(MoveTo(cat, -1));
        yield return new WaitForSeconds(3f);
        StopAllCoroutines();
        cat.SetActive(false);
    }
    IEnumerator EndingProduction()
    {
        yield return StartCoroutine(Typing_1(m_text2));
        cat.SetActive(true);
        cat.transform.position = new Vector3(0, -25, 3);
        StartCoroutine(MoveTo(cat, 1));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeOut(white1, white2, black1, black2));
        StopAllCoroutines();
        cat.SetActive(false);
    }
    IEnumerator Typing_1(string t)
    {
        for(int i = 0; i < t.Length; i++)
        {
            text.text = t.Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1.75f);
        text.text = "";
    }
    IEnumerator MoveTo(GameObject obj, int rotation)
    {
        for (int i = 0; i < 700; i++)
        {
            obj.transform.position += new Vector3(0, 0, 0.01f * rotation);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator FadeOut(GameObject objWhite1, GameObject objWhite2, GameObject objBlack1, GameObject objBlack2)
    {
        objWhite1.SetActive(true);
        objWhite2.SetActive(true);
        objBlack1.SetActive(true);
        objBlack2.SetActive(true);
        for (int i = 0; i < 27.5; i++)
        {
            objWhite1.transform.position += new Vector3(0, 0.2f, 0);
            objWhite2.transform.position += new Vector3(0, -0.2f, 0);
            objBlack1.transform.position += new Vector3(0, 0.2f, 0);
            objBlack2.transform.position += new Vector3(0, -0.2f, 0);
            yield return new WaitForSeconds(0.0001f);
        }
        for(int i = 0; i < 35; i++)
        {
            objWhite1.transform.localScale += new Vector3(-0.6f, 0, 0);
            objWhite2.transform.localScale += new Vector3(-0.6f, 0, 0);
            yield return new WaitForSeconds(0.0001f);
        }
    }
}
