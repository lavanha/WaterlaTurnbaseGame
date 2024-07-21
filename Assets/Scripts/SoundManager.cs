using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float timerShoot;

    private void Update()
    {
        if (timerShoot <= 0)
        {
            return;
        }
        timerShoot -= Time.deltaTime;
        if (timerShoot <= 0.05f)
        {
            PlaySound(audioClipRefsSO.shoot, Camera.main.transform.position);
        }
    }

    private void Start()
    {
        BaseAcion.OnAnyActionStarted += BaseAcion_OnAnyActionStarted;
        BaseAcion.OnAnyActionCompleted += BaseAcion_OnAnyActionCompleted;
        CameraManager.ShootCameraVisualCompleted += CameraManager_ShootCameraVisualCompleted;
        Door.isOpenCloseDoor += Door_isOpenCloseDoor;
    }

    private void Door_isOpenCloseDoor(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.openCloseDoor, Camera.main.transform.position);
    }

    private void CameraManager_ShootCameraVisualCompleted(object sender, System.EventArgs e)
    {
        timerShoot = 0.8f;
    }

    private void BaseAcion_OnAnyActionCompleted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case GrenadeAction:
                PlaySound(audioClipRefsSO.granadeExplosion, Camera.main.transform.position);
                break;
        }
    }

    private void BaseAcion_OnAnyActionStarted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case MoveAction:
                PlaySound(audioClipRefsSO.footStep, Camera.main.transform.position);
                break;
            case SwordAction:
                PlaySound(audioClipRefsSO.swordSlash, Camera.main.transform.position);
                break;
            case GrenadeAction:
                PlaySound(audioClipRefsSO.granadeLancher, Camera.main.transform.position);
                break;
        }
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumn = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumn);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumn = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumn);
    }
}
