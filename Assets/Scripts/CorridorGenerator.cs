using System.Collections.Generic;
using UnityEngine;

public class CorridorGenerator : MonoBehaviour
{

    public static CorridorGenerator Instance;

    [SerializeField] GameObject corridorOrigin;

    [SerializeField] GameObject corridorStart;
    [SerializeField] GameObject corridorPlain;
    [SerializeField] GameObject corridorLeft;
    [SerializeField] GameObject corridorRight;
    [SerializeField] GameObject corridorEnd;
    [SerializeField] GameObject corridorRoomLeft;
    [SerializeField] GameObject corridorRoomRight;

    [SerializeField] GameObject[] corridorPieces; // pieces assigned in the inspector.

    public int corridorLength = 1;

    [SerializeField] private GameObject diamondPrefab;

    private Vector3 playerStartingPosition;

    // use a List to keep track of every piece spawned so I can delete them later.
    private List<GameObject> activeCorridorPieces = new List<GameObject>();

    private void Awake()
    {
        // Simple Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStartingPosition = corridorOrigin.transform.position + new Vector3(0, 2, 0);

        GenerateCorridor(corridorLength);

    }

    void GenerateCorridor(int desiredCorridorLength)
    {
        Vector3 currentPosition = corridorOrigin.transform.position;

        // Spawn and track the starting piece
        GameObject startPiece = Instantiate(corridorStart, currentPosition, Quaternion.identity);
        activeCorridorPieces.Add(startPiece);

        int guaranteedRoomIndex = Random.Range(0, desiredCorridorLength);

        for (int i = 0; i < desiredCorridorLength; i++)
        {
            int randomIndex;

            // If the current loop matches our guaranteed index, force it to be a room
            if (i == guaranteedRoomIndex)
            {
                // Random.Range with integers is exclusive on the max value. 
                // Using (1, 3) means it will only ever return 1 or 2.
                randomIndex = Random.Range(1, 3);
            }
            else
            {
                // Otherwise, proceed with normal random generation
                randomIndex = Random.Range(0, corridorPieces.Length);
            }

            GameObject nextCorridorPiece = corridorPieces[randomIndex];

            Vector3 nextCorridorPiecePosition = currentPosition + new Vector3(0, 0, 10); // 10 because that's how big the pieces are. Like LEGO.

            GameObject spawnedPiece = Instantiate(nextCorridorPiece, nextCorridorPiecePosition, Quaternion.identity);
            activeCorridorPieces.Add(spawnedPiece);

            currentPosition = nextCorridorPiecePosition; // reset the corridor position for the next piece.

            //check if it is the left or right corridor. If it is get the current position. if it's left, intantiate, but to the left (-x axis), same to right but +x axis
            if (randomIndex == 1) // in the inspector, this is set as the left door (and it must be! otherwise it will spawn whatever is on the first index)
            {
                Vector3 roomPosition = currentPosition + new Vector3(-10, 0, 0);

                GameObject roomL = Instantiate(corridorRoomLeft, roomPosition, Quaternion.identity);
                activeCorridorPieces.Add(roomL);

                // Generate random X and Z rotation between -90 and 90. Y stays at 0.
                Quaternion randomRotation = Quaternion.Euler(Random.Range(-90f, 90f), 0f, Random.Range(-90f, 90f));

                // Spawn diamond and track it. Raised on the Y axis so it doesn't clip the floor.
                GameObject diamond = Instantiate(diamondPrefab, roomPosition + new Vector3(-3, 2f, 0), randomRotation);
                activeCorridorPieces.Add(diamond);
            }
            else if (randomIndex == 2) // in the inspector, this is set as the right door
            {
                Vector3 roomPosition = currentPosition + new Vector3(10, 0, 0);

                GameObject roomR = Instantiate(corridorRoomRight, roomPosition, Quaternion.identity);
                activeCorridorPieces.Add(roomR);

                // Generate random X and Z rotation between -90 and 90. Y stays at 0.
                Quaternion randomRotation = Quaternion.Euler(Random.Range(-90f, 90f), 0f, Random.Range(-90f, 90f));

                // Spawn diamond and track it. Raised on the Y axis so it doesn't clip the floor.
                GameObject diamond = Instantiate(diamondPrefab, roomPosition + new Vector3(3, 2f, 0), randomRotation);
                activeCorridorPieces.Add(diamond);
            }

        }
        GameObject endPiece = Instantiate(corridorEnd, currentPosition + new Vector3(0, 0, 10), Quaternion.identity);
        activeCorridorPieces.Add(endPiece);
    }

    // This method gets called by the trigger script at the end of the hall
    public void ResetAndExpandCorridor(GameObject player)
    {
        // 1. Destroy all current corridor pieces
        foreach (GameObject piece in activeCorridorPieces)
        {
            // MUST check if it's not null, because the player might have already destroyed the diamond!
            if (piece != null)
            {
                Destroy(piece);
            }
        }
        activeCorridorPieces.Clear(); // Empty the list for the next run

        // 2. Increase the length
        corridorLength += 1;

        // 3. Generate the new corridor
        GenerateCorridor(corridorLength);

        // 4. Teleport the player back to the start (slightly raised to avoid clipping)
        TeleportPlayer(player, playerStartingPosition);
    }

    private void TeleportPlayer(GameObject player, Vector3 destination)
    {
        player.transform.position = destination;
    }
}
