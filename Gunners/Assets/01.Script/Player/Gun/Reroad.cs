using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reroad : MonoBehaviour
{
    [SerializeField] private Image image;
    private RectTransform trm;
    private Canvas canvas;
    private IGun gun;

    private void Awake()
    {
        gun = GetComponent<IGun>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        trm = Instantiate(image, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform).GetComponent<RectTransform>();
        image = trm.GetComponent<Image>();
    }

    private void OnEnable()
    {
        gun.OnReroad += StartReroad;
    }

    private void OnDisable()
    {
        gun.OnReroad -= StartReroad;
    }

    private void Update()
    {
        trm.position = transform.right * 1.5f + transform.position;
    }

    private void StartReroad(float time)
    {
        StartCoroutine(Reroading(time));
    }

    private IEnumerator Reroading(float time)
    {
        float delta = 0f;

        while(delta <= time)
        {
            delta += Time.deltaTime;
            image.fillAmount = delta / time;
            yield return null;
        }

        image.fillAmount = 0f;
    }
}
