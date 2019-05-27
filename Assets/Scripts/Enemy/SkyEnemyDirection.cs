using UnityEngine;

public class SkyEnemyDirection : MonoBehaviour
{
	public float chengx=0.0f, chengy=0.0f;
	private float time;
	//変化値代入用
	[SerializeField]
	private float S;
	private float s;
	[SerializeField]
	private float C;
	private float c;
	void Start()
    {
        // 変化値を初期化
		s = 1.0f / S;
		c = 1.0f / C;
		time = 0.0f;
	}
    void Update()
    {
		time += Time.deltaTime;
		// SINで計算
		chengx = Mathf.Sin(2 * Mathf.PI * s * time);
		// COSで計算
		chengy = Mathf.Cos(2 * Mathf.PI * c * time);
	}
}
