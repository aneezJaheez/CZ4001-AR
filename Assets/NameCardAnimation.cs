using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameCardAnimation : MonoBehaviour
{
    private List<InterpolationInfo> _childrenToMove = new();
    private float speed = 0.005f;
    private float rotationSpeed = 90.0f; // degs per sec

    // Time when the movement started.
    private float startTime = 0.0f;

    private Transform image;
    private List<Transform> cards = new();
    private List<Transform> anchors = new();
    private List<Vector3> originalCards = new();
    private List<Vector3> originalObjTransforms = new(); // for non-card objects

    private float currentAngle = 0.0f;
    private int cardRotationSeq = 0; // 0 for first flip, 1 for second flip


    void Start()
    {
        var middleCard = transform.GetChild(2).gameObject.transform;
        var topCard = transform.GetChild(1).gameObject.transform;
        var bottomCard = transform.GetChild(3).gameObject.transform;
        originalCards = new()
        {
            middleCard.localPosition,
            topCard.localPosition,
            bottomCard.localPosition
        };

        for (int i = 4; i < 8; i++)
            originalObjTransforms.Add(transform.GetChild(i).transform.localPosition);
    }

    void OnEnable()
    {
        Debug.Log("Card animation enabled");
        image = transform.GetChild(0).gameObject.transform;
        // for non-card objects
        _childrenToMove = new();
        for (int i = 4; i < 8; i++) {
            var childTransform = transform.GetChild(i).transform;
            _childrenToMove.Add(new InterpolationInfo(childTransform, image.localPosition, originalObjTransforms[i - 4]));
            childTransform.localPosition = image.localPosition; // reset to parent position
        }
        startTime = Time.time;

        // for cards
        var firstAnchor = new GameObject().transform;
        var middleCard = transform.GetChild(2).gameObject.transform;
        firstAnchor.SetParent(transform, false);
        firstAnchor.localPosition = Vector3.Lerp(image.localPosition, originalCards[0], 0.5f);

        var secondAnchor = new GameObject().transform;
        var topCard = transform.GetChild(1).gameObject.transform;
        secondAnchor.SetParent(transform, false);
        secondAnchor.localPosition = Vector3.Lerp(originalCards[0], originalCards[1], 0.5f);

        var thirdAnchor = new GameObject().transform;
        var bottomCard = transform.GetChild(3).gameObject.transform;
        thirdAnchor.SetParent(transform, false);
        thirdAnchor.localPosition = Vector3.Lerp(originalCards[0], originalCards[2], 0.5f);

        middleCard.localPosition = image.localPosition;
        middleCard.Rotate(0,0,-180);

        // disable top bototm cards first
        topCard.gameObject.SetActive(false);
        bottomCard.gameObject.SetActive(false);

        cards = new()
        {
            middleCard,
            topCard,
            bottomCard
        };

        anchors = new()
        {
            firstAnchor,
            secondAnchor,
            thirdAnchor
        };

        cardRotationSeq = 0;
        currentAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // animate children
        if (_childrenToMove.Count > 0)
        {
            float distCovered = (Time.time - startTime) * speed;

            for (int i = 0; i < _childrenToMove.Count; i++)
            {
                var child = _childrenToMove[i];
                var childTransform = child.ObjTransform;
                float fractionOfJourney = distCovered / child.Length;
                if (fractionOfJourney >= 1)
                    _childrenToMove[i] = new InterpolationInfo(childTransform, child.Start, child.End, true);
                else
                    childTransform.localPosition = Vector3.Lerp(childTransform.localPosition, child.End, fractionOfJourney);
            }

            // filter those that reached
            _childrenToMove = _childrenToMove.Where(child => !child.ReachedEnd).ToList();
        }

        if (cardRotationSeq == 0)
        {
            // middle 
            var deltaAngle = rotationSpeed * Time.deltaTime;
            if (currentAngle + deltaAngle > 180.0f)
            {
                deltaAngle = 180.0f - currentAngle;
                cardRotationSeq++;
                currentAngle = 0;

                // enable top and bottom cards
                cards[1].gameObject.SetActive(true);
                cards[2].gameObject.SetActive(true);

                cards[1].localPosition = cards[0].localPosition;
                cards[2].localPosition = cards[0].localPosition;

                cards[1].localRotation = cards[0].localRotation;
                cards[2].localRotation = cards[0].localRotation;

                anchors[1].localPosition = Vector3.Lerp(cards[0].localPosition, originalCards[1], 0.5f);
                anchors[2].localPosition = Vector3.Lerp(cards[0].localPosition, originalCards[2], 0.5f);

                cards[1].Rotate(180, 0, 0);
                cards[2].Rotate(-180, 0, 0);
            } else
            {
                currentAngle += deltaAngle;
            }
            cards[0].RotateAround(anchors[0].position, image.forward, deltaAngle);
        }
        else if(cardRotationSeq == 1)
        {
            // top and bottom 
            var deltaAngle = rotationSpeed * Time.deltaTime;
            if (currentAngle + deltaAngle > 180.0f)
            {
                deltaAngle = 180.0f - currentAngle;
                cardRotationSeq++;
            }

            cards[1].RotateAround(anchors[1].position, -image.right, deltaAngle);
            cards[2].RotateAround(anchors[2].position, image.right, deltaAngle);
            currentAngle += deltaAngle;
        }
    }
}

public readonly struct InterpolationInfo { 
    public InterpolationInfo(Transform transform, Vector3 start, Vector3 end, bool reachedEnd = false)
    {
        ObjTransform = transform;
        Start = start;
        End = end;
        Length = Vector3.Distance(start, end);
        ReachedEnd = reachedEnd;
    }

    public Vector3 Start { get; }
    public Vector3 End { get; }
    public Transform ObjTransform { get; }
    public float Length { get; }
    public bool ReachedEnd { get; }
}
