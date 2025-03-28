using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuildControll : MonoBehaviour
{
    [SerializeField] public BuildState state;
    
    public List<GameObject> currentEmojis;
    
    [SerializeField]private GameObject particlePrefab;
    [SerializeField] private List <EmojiPair> emojiParticleTextures;
    [Serializable] 
    public struct EmojiPair
    {
        public EmojiTypes emojiType;
        public Texture sprite;
    }
    private GameObject[] playingEmoji;


    public void InitBuild()
    {
        GetComponent<BoxCollider>().size = new Vector3(state.size.x, state.size.y, state.size.x / 2);
        transform.Find("Img").localScale = Vector3.one* Math.Max(state.size.x,state.size.y)*0.75f;
        transform.Find("Img").GetComponent<SpriteRenderer>().sprite = state.buildTexture;
        transform.Find("Img").localPosition = state.localPositionSetting;

        transform.Find("shadow").localScale = new Vector3(state.size.x, Math.Max(state.size.x, state.size.y) / 2, state.size.y);
    }

    // Start is called before the first frame update
    ///////////////
    ///　関数　　///
    //////////////    
    
    public void AddEmojiParticle(EmojiTypes emoji)
    {
        GameObject newParticle=Instantiate(particlePrefab, this.transform);
        newParticle.GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex",emojiParticleTextures.Find(s=>s.emojiType==emoji).sprite);
        newParticle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        currentEmojis.Add(newParticle);
    }

    //////////////
    /// テスト　///
    //////////////
    
    [ContextMenu(nameof(SpawnParticle))]
    public void SpawnParticle() {
        AddEmojiParticle(EmojiTypes.Happy);
        AddEmojiParticle(EmojiTypes.Mad);
        AddEmojiParticle(EmojiTypes.Excited);
    }
}
