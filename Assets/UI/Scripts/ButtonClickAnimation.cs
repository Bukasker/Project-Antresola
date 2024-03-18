using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAnimation : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private Button button;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite clickedSprite;
    [SerializeField] private float buttonDelay = 0.2f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float clickVolume = 0.1f;

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
        yield return new WaitForSeconds(buttonDelay);
        button.image.sprite = originalSprite;
    }

    private void PlayClickSound()
    {
        audioSource.volume = clickVolume;
        audioSource.PlayOneShot(clickSound);
    }
}
