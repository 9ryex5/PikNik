using UnityEngine;

public class ScrollerElement : MonoBehaviour {

    [Range(0, 1)]
    public float sizePercentX;
    [Range(0, 1)]
    public float sizePercentY;

    private void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * sizePercentX, Screen.height * sizePercentY);
    }
}
