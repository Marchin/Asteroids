using UnityEngine;

[CreateAssetMenu(menuName = "Custom/AsteroidType")]
public class AsteroidType : ScriptableObject {
    public GameObject m_prefab;
    public Sprite[] m_sprites;
    
    public GameObject Request() {
        GameObject go = Instantiate(m_prefab);
        go.GetComponent<SpriteRenderer>().sprite = 
            m_sprites[Random.Range(0, m_sprites.Length)];
        return go;
    }
    
}
