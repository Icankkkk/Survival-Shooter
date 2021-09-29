﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    private void Start()
    {
        // mendapatkan offset antara target dan camera
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // mendapatkan posisi untuk camera
        Vector3 targetCamPos = target.position + offset;

        // set posisi camera dengan smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    /*
     Pada script di atas camera akan mengikuti player dengan smooth
     sehingga camera tidak kaku saat mengikuti player.
     */
}
