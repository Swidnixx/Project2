using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProceduralGen
{
    public class ProceduralGeometry : MonoBehaviour
    {
        public int iteration = 0;

        public SegmentLibrary segmentLibrary;

        public Transform parent;

        public int activeCount = 2;
        int HalfActiveCount => (int)(activeCount * 0.5f);


        List<Vector3> triggers = new List<Vector3>();
        public float incrementDistance = 3;

        public RuntimeSegment startSegment;
        public bool removeStartSegment;
        Transform player;

        LinkedList<RuntimeSegment> spawnedSegments = new LinkedList<RuntimeSegment>();

        public bool[] customOnes = { true };

        RuntimeSegment middleNext;
        RuntimeSegment middlePrev;


        #region Unity Callbacks
        void Start()
        {
            player = PlayerController.Instance.transform;

            Init();
        }
        private void Update()
        {
            if (Vector3.Distance(player.position, middleNext.go.transform.position) < incrementDistance)
            {
                Increment();
            }

            if (Vector3.Distance(player.position, middlePrev.go.transform.position) < incrementDistance)
            {
                Decrement();
            }

        }

        private void OnDrawGizmos()
        {
            if (triggers.Count == 0 || triggers == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawCube(middlePrev.go.transform.position, 10 * Vector3.one);
            Gizmos.color = Color.green;
            Gizmos.DrawCube(middleNext.go.transform.position, 10 * Vector3.one);
            //Gizmos.DrawWireSphere(triggers[triggers.Count - (activeCount / 2 + 1)], 10);
            //Gizmos.DrawWireSphere(triggers[triggers.Count - (activeCount / 2 - 1)], 10);

            Gizmos.color = Color.white;
            foreach (Vector3 t in triggers)
            {
                Gizmos.DrawWireSphere(t, 1);
            }

            // Gizmos.DrawWireCube(roomGenerator.transform.position, Vector3.one);
        } 
        #endregion

        public void Init()
        {
            Clear();

            if(startSegment.go == null)
            {
                startSegment.go = SpawnElement(iteration).go;
            }

            spawnedSegments.AddFirst(startSegment);

            //Set StartSegment as the middle one and Set its neighbours as next and prev nodes
            LinkedListNode<RuntimeSegment> middle = spawnedSegments.Find(startSegment);

            //Starting at 'iteration' and go forward
            for (int i = iteration + 1; i < iteration + HalfActiveCount; i++)
            {
                var segment = SpawnElement(i);
                ProduceHead(segment);
            }
            middleNext = middle.Previous.Value;

            //Starting at 'iteration' and go backwards
            for (int i = iteration - 1; i > iteration - HalfActiveCount; i--)
            {
                var segment = SpawnElement(i);
                ProduceTail(segment);
            }
            middlePrev = middle.Next.Value;
        }

        public void Clear()
        {
            iteration = 0;
            spawnedSegments.Clear();
            triggers.Clear();

            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                GameObject segment = parent.GetChild(i).gameObject;
                if(segment != startSegment.go || removeStartSegment)
                    DestroyImmediate(segment);
            }
        }

        public void Increment()
        {
            iteration++;
            RemoveTail();

            int index = iteration + HalfActiveCount - 1;

            if (IsCustom(index) && !removeStartSegment)
            {
                //Currently producing only startSegment as custom. Need to be changed
                spawnedSegments.AddFirst(startSegment);
            }
            else
            {
                var segment = SpawnElement(index);
                ProduceHead(segment);
            }

            // Take middleNext as LinkedListNode and get its neighbours
            LinkedListNode<RuntimeSegment> middle = spawnedSegments.Find(middleNext);
            middleNext = middle.Previous.Value;
            middlePrev = middle.Next.Value;
        }
        public void Decrement()
        {
            iteration--;
            RemoveHead();

            int index = iteration - HalfActiveCount + 1;

            if (IsCustom(index) && !removeStartSegment)
            {
                spawnedSegments.AddLast(startSegment);
            }
            else
            {
                var segment = SpawnElement(index);
                ProduceTail(segment);
            }

            LinkedListNode<RuntimeSegment> middle = spawnedSegments.Find(middlePrev);
            middleNext = middle.Previous.Value;
            middlePrev = middle.Next.Value;
        }

        RuntimeSegment SpawnElement(int i)
        {
            //Get segment data from library
            SegmentData data = segmentLibrary.GetSegment(i);

            //Setup GO handle anchor
            GameObject go = new GameObject(i.ToString() + data.name);
            go.transform.parent = parent;

            //Setup runtime data
            RuntimeSegment element = new RuntimeSegment() { go = go, info = data };

            //Spawn prefab from data and center it
            GameObject segmentPrefab = Instantiate(element.info.prefab);
            segmentPrefab.transform.parent = element.go.transform;
            segmentPrefab.transform.localPosition = Vector3.zero;
            segmentPrefab.transform.localRotation = Quaternion.Euler(Vector3.zero);

            return element;
        }
        void ProduceHead(RuntimeSegment newHead)
        {
            //RuntimeSegment s = SpawnElement(i);
            RuntimeSegment prevHead = spawnedSegments.First.Value;

            newHead.go.transform.position = prevHead.go.transform.TransformPoint(prevHead.info.exit);
            newHead.go.transform.rotation = prevHead.go.transform.rotation * Quaternion.AngleAxis(prevHead.info.GetRotation(), Vector3.up);
            newHead.go.transform.position -= newHead.go.transform.TransformDirection(newHead.info.entrance);
            //s.go.transform.position -= s.go.transform.TransformPoint(s.info.entrance);

            triggers.Add(newHead.go.transform.position);

            spawnedSegments.AddFirst(newHead);
        }
        void ProduceTail(RuntimeSegment newTail)
        {
            //RuntimeSegment s = SpawnElement(i);
            RuntimeSegment prevTail = spawnedSegments.Last.Value;

            newTail.go.transform.rotation = prevTail.go.transform.rotation * Quaternion.Inverse(Quaternion.AngleAxis(newTail.info.GetRotation(), Vector3.up));
            newTail.go.transform.position = prevTail.go.transform.TransformPoint(prevTail.info.entrance);
            newTail.go.transform.position -= newTail.go.transform.TransformDirection(newTail.info.exit);

            triggers.Add(newTail.go.transform.position);

            spawnedSegments.AddLast(newTail);
        }

        void RemoveTail()
        {
            RuntimeSegment tail = spawnedSegments.Last.Value;

            spawnedSegments.RemoveLast();

            if (removeStartSegment || tail != startSegment)
                DestroyImmediate(tail.go);
        }
        void RemoveHead()
        {
            RuntimeSegment head = spawnedSegments.First.Value;

            spawnedSegments.RemoveFirst();

            if (removeStartSegment || head != startSegment)
                DestroyImmediate(head.go);
        }
        bool IsCustom(int i)
        {
            return i < customOnes.Length && i >= 0 && customOnes[i];
        }
    }

    [Serializable]
    public class RuntimeSegment
    {
        public GameObject go;
        public SegmentData info;
    }
}