using System.Collections;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI MUI;

    public Animator firstMenu;
    private Animator currentMenu;

    private void Awake()
    {
        MUI = this;
    }

    private void Start()
    {
        currentMenu = firstMenu;
    }

    public void GoRight(Animator _menu)
    {
        StartCoroutine(SetInactive(currentMenu.gameObject, 0.5f));
        _menu.gameObject.SetActive(true);
        currentMenu.Play("CenterLeft");
        _menu.Play("RightCenter");
        currentMenu = _menu;
    }

    public void GoLeft(Animator _menu)
    {
        StartCoroutine(SetInactive(currentMenu.gameObject, 0.5f));
        _menu.gameObject.SetActive(true);
        currentMenu.Play("CenterRight");
        _menu.Play("LeftCenter");
        currentMenu = _menu;
    }

    public void Appear(Animator _menu)
    {
        _menu.gameObject.SetActive(true);
        //Update Position
        _menu.GetComponent<RectTransform>().localPosition = Vector3.one;
        _menu.GetComponent<RectTransform>().localPosition = Vector3.zero;
        _menu.Play("Appear");
    }

    public void Disappear(Animator _menu)
    {
        StartCoroutine(SetInactive(_menu.gameObject, 0.5f));
        _menu.Play("Disappear");
    }

    IEnumerator SetInactive(GameObject _menu, float time)
    {
        yield return new WaitForSeconds(time);
        _menu.SetActive(false);
    }
}
