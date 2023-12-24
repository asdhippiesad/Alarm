using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundComponent : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private AlarmSystem _alarm;
    [SerializeField] private AudioSource _audio;

    public AlarmSystem Alarm => _alarm;

    private float _minVolumeAlarm = 0.0f;
    private float _maxVolumeAlarm = 2.0f;

    private void OnEnable()
    {
        Alarm.Registered += StartPlayAudio;
        Alarm.Disarmed += StopPlayAudio;
        Debug.Log("OnEnable");
    }

    private void OnDisable()
    {
        Alarm.Registered -= StartPlayAudio;
        Alarm.Disarmed -= StopPlayAudio;
        Debug.Log("OnDisable");
    }
    private void StartPlayAudio()
    {
        _audio.Play();
        StartCoroutine(SoundOn(_maxVolumeAlarm));
        Debug.Log("Alarm");
    }
    private void StopPlayAudio()
    {
        StopCoroutine(SoundOff(_minVolumeAlarm));
    }

    private IEnumerator SoundOn(float currentVolume)
    {
        while (_audio.volume != currentVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, currentVolume, _transitionSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator SoundOff(float currentVolume)
    {
        while (_audio.volume != currentVolume)
        {
            yield return new WaitWhile(() => _audio.volume != currentVolume);
        }

        if (_audio.volume == 0)
        {
            _audio.Stop();
        }
    }
}
