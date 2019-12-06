using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    // Configuration params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // Chached reference
    Level level;

    [SerializeField] int timesHit = 0; // serialized for debug

    private void Start() {
        level = FindObjectOfType<Level>();
        CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (tag == "Breakable") {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        if (timesHit >= hitSprites.Length + 1) {
            DestroyBlock();
        } else {
            ShowNextHitSprinte();
        }
    }

    private void ShowNextHitSprinte() {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
        } else {
            Debug.LogError("Block sprite missing from array: " + gameObject.name);
        }
    }

    private void DestroyBlock() {
        PlayBlockDestroySFX();
        level.BlockDestroyed();
        FindObjectOfType<GameSession>().AddToScore();
        TriggerSparkleVFX();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySFX() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void CountBreakableBlocks() {
        if (tag == "Breakable") {
            level.CountBlock();
        }
    }

    private void TriggerSparkleVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
