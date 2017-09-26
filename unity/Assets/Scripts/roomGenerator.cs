using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerator : MonoBehaviour {

    public GameObject player;
	public GameObject[] availableRooms;

    private List<GameObject> currentRooms = new List<GameObject>();
    private float screenWidthInPoints =0.0f;
	//Camera _camera;

    // Use this for initialization
    void Start () 
	{
		 

		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update () 
    {
    }

    void FixedUpdate () 
    {    
        GenerateRoomIfRequired ();
    }
        

    void GenerateRoomIfRequired()
    {
        //1 Lista donde guardaremos las salas a borrar, si se han pasado del limite en x
        List<GameObject> roomsToRemove = new List<GameObject>();

        //2
        bool addRooms = true;        

        //3 mi posicion
        float playerX = player.transform.position.x;

        //4 Limite para eliminar sala, si tenemos sala mas lejos de este punto, ya no se ve en pantalla asi que la elimino
        float removeRoomX = playerX - screenWidthInPoints;        

        //5 limite para añadir sala, si tenemos alguna sala a una distancia superior a esta, no hace falta crear una nueva, porque ya hay una que aun ni ha llegado
        float addRoomX = playerX + screenWidthInPoints;

        //6 la mas lejana, inicializo a -tamaño de una sala para la primera vez que no hay nada instancie la sala en ese punto y quede todo cubierto porque el player esta en el 0
        float farthestRoomEndX = -availableRooms[0].transform.Find("Techo").localScale.x;

        foreach(var room in currentRooms)
        {
            //7 cojo el floor por ejemplo porque lo cubre todo, ya me he encargado en el diseño
            float roomWidth = room.transform.Find("Techo").localScale.x;
            //dónde empieza la sala actual
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f); 
            //donde acaba la sala actual
            float roomEndX = roomStartX + roomWidth;                            

            //8 si ya tenemos una sala que aun ni se ve, pues no hace falta añadir
            if (roomStartX > addRoomX)
                addRooms = false;

            //9 si tenemos una sala que ya la hemos pasado y esta fuera de los limites, no sirve para nada, asi que la eliminamos
            if (roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            //10 vble para saber donde esta el fin de la sala mas lejana, por si hay que añadir sala, se añade a partir de ese punto
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        //11 eliminamos las salas sobrantes
        foreach(var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);            
        }

        //12 añadimos sala en caso de hacer falta
        if (addRooms)
            AddRoom(farthestRoomEndX);
    }

    void AddRoom(float farhtestRoomEndX)
    {
        //1 elegimos una sala al azar de entre todas las disponibles
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        //2 la instanciamos
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

        //3 y la colocamos, primero vemos su ancho y calculamos dónde ira colocada(su centro)
        float roomWidth = room.transform.Find("Techo").localScale.x;

        //4
        float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;

        //5
        room.transform.position = new Vector3(roomCenter, 0, 0);

        //6 y la añadimos
        currentRooms.Add(room);         
    }

}
