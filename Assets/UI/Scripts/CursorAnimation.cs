using UnityEngine;
using UnityEngine.UI;

public class CursorAnimation : MonoBehaviour
{
    [Header("Cursor Textures")]
    [SerializeField] private Sprite regularSprite;
    [SerializeField] private Sprite buildSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;
    [SerializeField] private Sprite grabPeopleSprite;
    [SerializeField] private Sprite grabTreesSprite;
    [SerializeField] private Sprite irrigationSprite;

    [Header("Cursor Components")]
    [SerializeField] private Image cursorImage;
    [SerializeField] private RectTransform cursorRectTransform;

    [Header("Anchor Settings")]
    [SerializeField] private Vector2 regularAnchor = new Vector2(0.1f, 0.9f);
    [SerializeField] private Vector2 buildAnchor = new Vector2(0.5f, 0.4f);
    [SerializeField] private Vector2 hoverAnchor = new Vector2(0.15f, 0.9f);
    [SerializeField] private Vector2 clickAnchor = new Vector2(0.1f, 0.9f);
    [SerializeField] private Vector2 grabPeopleAnchor = new Vector2(0.5f, 0.25f);
    [SerializeField] private Vector2 grabTreesAnchor = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 irrigationAnchor = new Vector2(0.5f, 0.5f);

    [Header("Audio Settings")]
    [SerializeField][Range(0f, 1f)] private float soundVolume = 0.1f;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        Vector2 localCursorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(cursorRectTransform.parent as RectTransform, Input.mousePosition, null, out localCursorPos);
        cursorRectTransform.localPosition = localCursorPos;
    }

    private void SetCursorTexture(Sprite sprite, Vector2 anchor)
    {
        cursorImage.sprite = sprite;
        SetAnchor(cursorImage.gameObject, anchor);
        PlayClickSound();
    }

    public void SetRegularCursor() => SetCursorTexture(regularSprite, regularAnchor);
    public void SetBuildCursor() => SetCursorTexture(buildSprite, buildAnchor);
    public void SetHoverCursor() => SetCursorTexture(hoverSprite, hoverAnchor);
    public void SetClickCursor() => SetCursorTexture(clickSprite, clickAnchor);
    public void SetGrabPeopleCursor() => SetCursorTexture(grabPeopleSprite, grabPeopleAnchor);
    public void SetGrabTreesCursor() => SetCursorTexture(grabTreesSprite, grabTreesAnchor);
    public void SetIrrigationCursor() => SetCursorTexture(irrigationSprite, irrigationAnchor);

    private void PlayClickSound()
    {
        audioSource.volume = soundVolume;
        audioSource.PlayOneShot(clickSound);
    }

    private void SetAnchor(GameObject obj, Vector2 anchor)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchorMin = anchor;
            rectTransform.anchorMax = anchor;
            rectTransform.pivot = anchor;
        }
    }
}
