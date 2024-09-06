using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResourceSpriteAnimator : MonoBehaviour
{
    public UnityEvent OnComplete;

    public SpriteAnimatorType Type;
    public string FolderName;
    [SerializeField] private Sprite[] samples;

    public bool PlayOnAwake;
    public bool DestroyAfterAnimation;
    public bool Loop;
    public bool TwoSided;

    [SerializeField] private Image _image;
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        samples = Resources.LoadAll<Sprite>("Animations/" + FolderName);

        if (Type == SpriteAnimatorType.image) _image = GetComponent<Image>();
        else if (Type == SpriteAnimatorType.spriteRenderer) _renderer = GetComponent<SpriteRenderer>();

        if (PlayOnAwake) Play();
    }

    public void Play()
    {
        StopAllCoroutines();
        if (Type == SpriteAnimatorType.image) StartCoroutine(animationRoutineImage());
        else if (Type == SpriteAnimatorType.spriteRenderer) StartCoroutine(animationRoutineSprite());
    }

    public void LoadAndPlay(string folderName)
    {
        StopAllCoroutines();
        samples = Resources.LoadAll<Sprite>("Animations/" + folderName);
        if (Type == SpriteAnimatorType.image) StartCoroutine(animationRoutineImage());
        else if (Type == SpriteAnimatorType.spriteRenderer) StartCoroutine(animationRoutineSprite());
    }



    private IEnumerator animationRoutineSprite()
    {
        do
        {
            for (int i = 0; i < samples.Length; i++)
            {
                _renderer.sprite = samples[i];
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            if (TwoSided)
            {
                for (int i = samples.Length - 1; i >= 0; i--)
                {
                    _renderer.sprite = samples[i];
                    yield return new WaitForSeconds(Time.fixedDeltaTime);
                }
            }
        }
        while (Loop);

        OnComplete?.Invoke();

        if (DestroyAfterAnimation) Destroy(gameObject);
    }

    private IEnumerator animationRoutineImage()
    {
        do
        {
            for (int i = 0; i < samples.Length; i++)
            {
                _image.sprite = samples[i];
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            if (TwoSided)
            {
                for (int i = samples.Length - 1; i >= 0; i--)
                {
                    _image.sprite = samples[i];
                    yield return new WaitForSeconds(Time.fixedDeltaTime);
                }
            }
        }
        while (Loop);

        OnComplete?.Invoke();

        if (DestroyAfterAnimation) Destroy(gameObject);
    }
}

public enum SpriteAnimatorType
{
    image,
    spriteRenderer
}