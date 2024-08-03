using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 0.85f;

    private SpriteRenderer sr;
    private Color color;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        alpha = alphaSet;

        timeActivated = Time.time;
    }

    public void SetData(SpriteRenderer spriteSr, Transform player)
    {
        sr.sprite = spriteSr.sprite;
        color = spriteSr.color;
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        var cl = new Color(color.r, color.g, color.b, alpha);
        sr.color = cl;

        if (Time.time >= timeActivated + activeTime)
        {
            PlayerAfterImagePool.Instance.AddToPool(this);
        }
    }
}