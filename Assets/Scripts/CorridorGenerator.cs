using UnityEngine;

public class CorridorGenerator : MonoBehaviour
{
    [SerializeField] GameObject corridorOrigin;

    [SerializeField] GameObject corridorStart;
    [SerializeField] GameObject corridorPlain;
    [SerializeField] GameObject corridorLeft;
    [SerializeField] GameObject corridorRight;
    [SerializeField] GameObject corridorEnd;
    [SerializeField] GameObject corridorRoomLeft;
    [SerializeField] GameObject corridorRoomRight;

    [SerializeField] GameObject[] corridorPieces; // pieces assigned in the inspector.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 currentPosition = corridorOrigin.transform.position;

        Instantiate(corridorStart, currentPosition, Quaternion.identity);

        for (int i = 0; i < 6; i++)
        {
            int randomIndex = Random.Range(0, corridorPieces.Length);

            GameObject nextCorridorPiece = corridorPieces[randomIndex];

            Vector3 nextCorridorPiecePosition = currentPosition + new Vector3(0, 0, 10); // 10 because that's how big the pieces are. Like LEGO.


            Instantiate(nextCorridorPiece, nextCorridorPiecePosition, Quaternion.identity);

            currentPosition = nextCorridorPiecePosition; // reset the corridor position for the next piece.

            //check if it is the left or right corridor. If it is get the current position. if it's left, intantiate, but to the left (-x axis), same to right but +x axis
            if (randomIndex == 1) // in the inspector, this is set as the left door (and it must be! otherwise it will spawn whatever is on the first index)
            {
                Instantiate(corridorRoomLeft, currentPosition + new Vector3(-10, 0, 0), Quaternion.identity); // spawns to the left of the newly-spawned corridor piece (which should be the left door)
            }
            else if (randomIndex == 2) // in the inspector, this is set as the right door
            {
                Instantiate(corridorRoomRight, currentPosition + new Vector3(10, 0, 0), Quaternion.identity); // spawns to the right of the newly-spawned corridor piece (which should be the right door)
            }


        }

        Instantiate(corridorEnd, currentPosition + new Vector3(0, 0, 10), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
