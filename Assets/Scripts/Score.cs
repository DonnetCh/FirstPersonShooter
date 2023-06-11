using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    private int player;
    public int coins;
    public string Dineros;
    // Start is called before the first frame update
    void Start()
    {
      //  player = GetComponent<Player>().Coins;
    }

    // Update is called once per frame
    void Update()
    {
        //coins = player;
        
       // score.text = "Monedas recolectadas: " + coins.ToString() + "/5"; ;
    }
}
