using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Core{
    public class SpawnPath : MonoBehaviour {
        private float sphereRadius = 0.2f;
        
        private void OnDrawGizmos() {
            for(int i = 0; i < transform.childCount ; i++)
            {
                Gizmos.DrawLine(GetChildPosition(i), GetChildPosition(GetNextChildIndex(i)));
                DrawSphereOnGizmos(GetChildPosition(i));
            }
        }

        private void DrawSphereOnGizmos(Vector3 center)
        {
            Gizmos.DrawSphere(center, sphereRadius);
        }

        private Vector3 GetChildPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        private int GetNextChildIndex(int index){
            if(index == transform.childCount - 1){
                return 0;
            }
            return index + 1;
        }

        public Vector3 GetRandomSpawnPoint()
        {
            // Get lenght of paths
            int index = GetRandomPathIndex();
            Vector3 a, b;
            GetSegmentByIndex(index, out a, out b);

            return GetRandomPointInSegment(a, b);
        }

        private Vector3 GetRandomPointInSegment(Vector3 a, Vector3 b){
            // float resX = Random.Range(a.x, b.x);
            // float resY =  ((resX - a.x) * (b.y - a.y) / (b.x - a.x) ) + a.y; 
            // return new Vector3(resX, resY, 0f);

            Vector3 delt = b - a;
            delt *= Random.value;
            return a + delt;
        }

        private void GetSegmentByIndex(int index, out Vector3 a, out Vector3 b) {
            a = GetChildPosition(index);
            b = GetChildPosition(GetNextChildIndex(index));
         }

        private int GetRandomPathIndex()
        {
            float[] pathLengths = new float[GetChildCount()];
            for (int i = 0; i < GetChildCount(); ++i)
            {
                pathLengths[i] = GetSegmentMagnitude(GetChildPosition(i), GetChildPosition(GetNextChildIndex(i)));
            }
            // Divide into (0,1)
            float totalLength = GetLengthOfPaths();
            float[] compressedLengths = new float[GetChildCount()];
            for (int i = 0; i < GetChildCount(); ++i)
            {
                compressedLengths[i] = pathLengths[i] / totalLength;
            }

            // Select segment base on compressedLengths
            int selectIndex = -1;
            float randFloat = Random.value;

            for (int i = 0; i < compressedLengths.Length; i++)
            {
                if(compressedLengths[i] > randFloat)
                {
                    selectIndex = i;
                    break;
                }
                randFloat -= compressedLengths[i];
            }

            Assert.AreNotEqual(selectIndex, -1);
            return selectIndex;
        }

        private int GetChildCount(){
            return transform.childCount;
        }

        private float GetSegmentMagnitude(Vector3 a, Vector3 b){
            Vector3 delt = a - b;
            return delt.magnitude;
        }

        private float GetLengthOfPaths(){
            float sum = 0;
            for(int i = 0; i < GetChildCount(); ++i){ 
                sum += GetSegmentMagnitude(GetChildPosition(i), GetChildPosition(GetNextChildIndex(i)));
            }
            return sum;
        }
    }
}