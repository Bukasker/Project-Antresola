using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAnimation : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private Button button;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite clickedSprite;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;

    private Sprite originalSprite;

    private void Start()
    {
        originalSprite = button.image.sprite;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        button.image.sprite = clickedSprite;
        PlayClickSound();
        StartCoroutine(ResetButtonSprite());
    }

    private System.Collections.IEnumerator ResetButtonSprite()
    {
        yield return new WaitForSeconds(0.5f);
        button.image.sprite = originalSprite;
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
