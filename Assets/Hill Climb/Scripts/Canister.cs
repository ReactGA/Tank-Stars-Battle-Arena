using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Canister : MonoBehaviour {
    public float _fuel;
    public string _destroyAnimationName;
    public AudioClip _pickUpSound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(_pickUpSound);
            col.gameObject.SendMessage("AddFuel", _fuel);
            gameObject.GetComponent<Animation>().Play(_destroyAnimationName);
            Destroy(gameObject, gameObject.GetComponent<Animation>().GetClip(_destroyAnimationName).length);
        }
    }
}
