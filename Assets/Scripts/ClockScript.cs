using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    Sprite[] sprites;
    public int difficulty = 1;
    public int wave = 0;
    public int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("phone clock all");
    }

    // Update is called once per frame
    void Update()
    {
        // values to add. each wave is 26 long, with the 26th being generally unused.
        // each difficulty adds 26*7*dif (0, 1, 2, 3, 4, 5, xx)
        // each level adds 26*level.
        // add wave directly.
        int spriteNumber = (difficulty - 1)*7*27;
        if (level >= 6)
            spriteNumber += 7 * 27;
        else
            spriteNumber += (level) * 27;
        if (wave > 25)
            spriteNumber += 26;
        else
            spriteNumber += wave;
        if (spriteNumber >= sprites.Length)
        {
            spriteNumber = sprites.Length - 1;
        }

        this.GetComponent<SpriteRenderer>().sprite = sprites[spriteNumber];
    }


}
