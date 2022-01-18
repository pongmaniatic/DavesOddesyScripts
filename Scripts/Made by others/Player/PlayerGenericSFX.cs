using UnityEngine;

public class PlayerGenericSFX : MonoBehaviour
{

    public static PlayerGenericSFX Instance;

    #region Movement SFX
    public AudioClip movementSFX;
    public AudioSource audioSourceMovement;

    public bool isWalking;
    public bool IsWalking
    {
        get { return isWalking; }

        set
        {
            isWalking = value;
            if (isWalking)
            {
                if (!audioSourceMovement.isPlaying)
                {

                    audioSourceMovement.Play();

                }
            }
            else
            {
                audioSourceMovement.Stop();

            }

        }
    }
    #endregion

    #region  Emote SFX
    public AudioClip[] happyEmoteSFXs;
    public AudioClip[] negativeEmoteSFXs;
    #endregion


    public AudioSource firstAudioSource;
    public AudioSource secondAudioSource;

    public AudioSource thirdAudioSourceLoop;

    private PlayerMovement1 _playerMovement;

    // public int sfxQueueImportance; TO DO if we need it some day

    private void Awake()
    {
        SetToStaticInstance();

        audioSourceMovement.clip = movementSFX;
        _playerMovement = GetComponentInParent<PlayerMovement1>();

    }


    private void Update()
    {
        IsWalking = _playerMovement.isWalking;
    }

    public void SetToStaticInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySFX(AudioClip sfx, float sfxVolume)
    {
        firstAudioSource.pitch = 1.0f;
        firstAudioSource.PlayOneShot(sfx, sfxVolume);
    }


    public void PlayLoopSFX(AudioClip sfx, float sfxVolume)
    {
        firstAudioSource.pitch = 1.0f;
        thirdAudioSourceLoop.clip = sfx;
        thirdAudioSourceLoop.volume = sfxVolume;
        thirdAudioSourceLoop.Play();
    }


    public void PlayRandomSFX(AudioClip[] sfx, float sfxVolume)
    {
        firstAudioSource.pitch = 1.0f;
        firstAudioSource.PlayOneShot(sfx[Random.Range(0, sfx.Length)], sfxVolume);
    }


    public void PlayRandomPitchSFX(AudioClip sfx, float sfxVolume, float randomPitchMin, float randomPitchMax)
    {
        firstAudioSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        firstAudioSource.PlayOneShot(sfx, sfxVolume);
    }

    public void PlayTwoSFXInQueue(AudioClip firstSFX, AudioClip secondSFX)
    {
        firstAudioSource.pitch = 1.0f;
        secondAudioSource.pitch = 1.0f;
        firstAudioSource.clip = firstSFX;
        secondAudioSource.clip = secondSFX;

        double _clipOneDuration = firstAudioSource.clip.samples / secondAudioSource.clip.frequency;

        firstAudioSource.PlayScheduled(AudioSettings.dspTime + 0.1);
        secondAudioSource.PlayScheduled(AudioSettings.dspTime + 0.1 + _clipOneDuration);


    }
}
