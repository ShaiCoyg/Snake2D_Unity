using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class snake : MonoBehaviour
{
    public GameObject tempFruit,fruitPrefab,tailPrefab;
    List<Transform> tail = new List<Transform>();
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        tempFruit=Instantiate(fruitPrefab,new Vector2(Random.Range(-9,4)+5f,Random.Range(-2,4)+.5f),Quaternion.identity);
        InvokeRepeating("play",0,.2f);
    }

    // Update is called once per frame
    void Update()
    { //player control snake (regular keyboard arrows - up down left right)
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (vertical==0&&horizontal!=0||vertical!=0&&horizontal==0) {
            direction=new Vector2(horizontal,vertical);
        }
    }

    void play() { //change position and random place for new fruit && add +1 tail clone attached to snake head or last tail is such exist
        Vector2 lastPosition = transform.position;
        transform.Translate(direction);
        if(!tempFruit) { 
            tempFruit=Instantiate(fruitPrefab,new Vector2(Random.Range(-9,4)+5f,Random.Range(-2,4)+.5f),Quaternion.identity);
            GameObject t = Instantiate(tailPrefab,lastPosition,Quaternion.identity);
            tail.Insert(0,t.transform);
        }
        if(tail.Count>0) { 
            tail.Last().position=lastPosition;
            tail.Insert(0,tail.Last());
            tail.RemoveAt(tail.Count-1);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject==tempFruit) {
            Destroy(tempFruit); //generate new fruit when old one consumed
        }
        else {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
