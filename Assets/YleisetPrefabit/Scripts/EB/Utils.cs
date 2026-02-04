using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//using System.Linq;

namespace EB
{
    public class Utils : Singleton<Utils>
    {
        // Commented out to reduse WebGL build size

        // public static void ExplosionForce(Vector3 explosionPos, float radius = 5f, float power = 10f)
        // {
        //     Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        //     foreach (Collider hit in colliders)
        //     {
        //         Rigidbody rb = hit.GetComponent<Rigidbody>();

        //         if (rb != null)
        //             rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        //     }
        // }

        // public static List<Transform> FindNearest(Vector3 to, Transform[] from, int count = 1)
        // {
        //     List<Transform> nearestPoints = new List<Transform>();

        //     for (int i = 0; i < count; i++)
        //     {
        //         float closestDistance = float.MaxValue;
        //         Transform nearest = from[0];
        //         for (int ii = i; ii < from.Length; ii++)
        //         {
        //             float distance = Vector3.Distance(to, from[ii].position);
        //             if (distance < closestDistance && distance != 0)
        //             {
        //                 nearest = from[ii];
        //                 closestDistance = distance;
        //                 from[ii] = from[i];
        //                 from[i] = nearest;
        //             }
        //         }
        //         nearestPoints.Add(nearest);
        //     }
        //     return nearestPoints;
        // }

        // public static List<Transform> FindNearest(Vector3 to, GameObject[] from, int count = 1)
        // {
        //     Transform[] tFrom = new Transform[from.Length];
        //     for (int i = 0; i < from.Length; i++)
        //     {
        //         tFrom[i] = from[i].transform;
        //     }
        //     return FindNearest(to, tFrom, count);
        // }
        // //HueShift
        // public static void ColorHueShift(Material mat, float amount)
        // {
        //     float h = 0;
        //     float s = 0;
        //     float v = 0;

        //     Color.RGBToHSV(mat.color, out h, out s, out v);
        //     var color = Color.HSVToRGB(h + amount, s, v);
        //     mat.SetColor("_Color", color);
        // }


        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="executionTime"></param>
        // /// <param name="action">A function that utilizes progress value to determine its behaviour, progress varies from 0 to 1.
        // /// example: (progress) => Vector3.Lerp(start, end, progress);</param>
        // /// <returns></returns>
        // public static void PerFrameExecution(float executionTime, Action<float> action, bool unscaledTime = false)
        // {
        //     Instance.StartCoroutine(_PerFrameExecution(executionTime, action, unscaledTime));
        // }


        // static IEnumerator _PerFixedFrameExecution(float executionTime, Action<float> action, bool unscaledTime = false)
        // {
        //     for (float currentTime = unscaledTime ? Time.unscaledTime : Time.time, endAt = currentTime + executionTime;
        //         currentTime < endAt;
        //         currentTime = unscaledTime ? Time.unscaledTime : Time.time
        //         )

        //     {
        //         action(1 - (endAt - currentTime) / executionTime);
        //         yield return new WaitForFixedUpdate();
        //     }
        //     action(1);// Making sure full progress indication is sent at the last frame
        // }

        // static IEnumerator _PerFrameExecution(float executionTime, Action<float> action, bool unscaledTime = false)
        // {
        //     for (float currentTime = unscaledTime ? Time.unscaledTime : Time.time, endAt = currentTime + executionTime;
        //         currentTime < endAt;
        //         currentTime = unscaledTime ? Time.unscaledTime : Time.time
        //         )

        //     {
        //         action(1 - (endAt - currentTime) / executionTime);
        //         yield return null;
        //     }
        //     action(1);// Making sure full progress indication is sent at the last frame
        // }


        // /// <summary>
        // /// Delays action by the time set.
        // /// </summary>
        // /// <param name="delay">For how long to delay the action.</param>
        // /// <param name="action">Function, delegate. Example: delegate () { print("Hello World!"); }</param>
        // public static void In(float delay, Action action, bool unscaled = false)
        // {
        //     Instance.StartCoroutine(DelayedAction(delay, action, unscaled));
        // }

        // static IEnumerator DelayedAction(float delay, Action action, bool unscaled = false)
        // {
        //     if (unscaled)
        //         yield return new WaitForSecondsRealtime(delay);
        //     else
        //         yield return new WaitForSeconds(delay);
        //     yield return null;
        //     action();
        // }

        // public static Vector3 Vector3RandomPerlin(float seed)
        // {
        //     return new Vector3(CustomPerlin(0f + seed),
        //      CustomPerlin(100f + seed),
        //       CustomPerlin(-100f + seed));
        // }

        // public static float CustomPerlin(float index)
        // {
        //     return Mathf.PerlinNoise(index, 0) * 2 - 1;
        // }
        // public static float CustomPerlinPow(float index, float pow)
        // {
        //     return Mathf.Pow(Mathf.PerlinNoise(index, 0) * 2 - 1, pow);
        // }
        // public static float CustomPerlinPowOv(float index, float pow = 3f, float overbound = .3f)
        // {
        //     return Mathf.Pow((Mathf.PerlinNoise(index, 0) + overbound) * 2 - 1, pow);
        // }


        // public static Vector2 PerlinVector2(float x, float delta)
        // {
        //     return new Vector2(Mathf.PerlinNoise(x, x) * 2 - 1, Mathf.PerlinNoise(x, x + delta) * 2 - 1);
        // }

        // static string textColorStart = "<color=#00000000>";
        // static string textColorEnd = "</color>";
        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="textField">Should contain required text.</param>
        // /// <returns></returns>
        // public static IEnumerator RollingTextLetterByLetter(Text textField)
        // {
        //     string text = textField.text;
        //     //var color = textField.color;
        //     //color.a = 0;
        //     //textField.color = color;

        //     for (int i = 1; i < text.Length - 1; i++)
        //     {
        //         string newText = text.Insert(i, textColorStart) + textColorEnd;
        //         textField.text = newText;
        //         yield return new WaitForSeconds(0.02f);
        //     }
        //     textField.text = text;
        // }

        // public static string TextSurroundByUIColorInfo(string text, string HexColor = "FF0000FF")
        // {
        //     return "<color=#" + HexColor + ">" + text + textColorEnd;
        // }


        public static T DeepClone<T>(T obj)
        {
            T objResult;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = (T)bf.Deserialize(ms);
            }
            return objResult;
        }
    }

    // public static class ExtensionMethods
    // {
    //     public static T GetComponentInParents<T>(this GameObject gameObject)
    //             where T : UnityEngine.Component
    //     {
    //         for (Transform t = gameObject.transform; t != null; t = t.parent)
    //         {
    //             T result = t.GetComponent<T>();
    //             if (result != null)
    //                 return result;
    //         }
    //         return null;
    //     }

    //     public static T GetRandom<T>(this List<T> list)
    //     {
    //         var index = Random.Range(0, list.Count);
    //         //Debug.Log(list.Count -1+ ": "+ index);
    //         return list[index];
    //     }

    //     public static T GetRandom<T>(this T[] array)
    //     {
    //         var index = Random.Range(0, array.Length);
    //         //Debug.Log(list.Count -1+ ": "+ index);
    //         return array[index];
    //     }

    //     public static List<GameObject> GetActive(this List<GameObject> gameObjectList)
    //     {
    //         return gameObjectList.Where((o) => o.activeSelf).ToList();
    //     }
    //     public static List<GameObject> GetDisabled(this List<GameObject> gameObjectList)
    //     {
    //         return gameObjectList.Where((o) => !o.activeSelf).ToList();
    //     }

    //     public static void SetActive(this GameObject[] gameObjects, bool active = true)
    //     {
    //         foreach (var go in gameObjects)
    //         {
    //             go.SetActive(active);
    //         }
    //     }

    //     public static int ActiveCount(this GameObject gameObject)
    //     {
    //         int childCount = gameObject.transform.childCount;
    //         int activeCount = 0;
    //         for (int i = 0; i < childCount; i++)
    //         {
    //             if (gameObject.transform.GetChild(i).gameObject.activeSelf)
    //                 activeCount++;
    //         }
    //         return activeCount;
    //     }

    //     public static Vector3 Round(this Vector3 vectorToRound)
    //     {
    //         return new Vector3((int)vectorToRound.x, (int)vectorToRound.y, (int)vectorToRound.z);
    //     }

    //     public static Vector2 Round(this Vector2 vectorToRound)
    //     {
    //         return new Vector2((int)vectorToRound.x, (int)vectorToRound.y);
    //     }

    //     public static Vector3 Pow(this Vector3 vecToPow, float pow)
    //     {
    //         return new Vector3(Mathf.Pow(vecToPow.x, pow), Mathf.Pow(vecToPow.y, pow), Mathf.Pow(vecToPow.z, pow));
    //     }
    // }

    // public static class IListExtensions
    // {
    //     /// <summary>
    //     /// Shuffles the element order of the specified list.
    //     /// </summary>
    //     public static void Shuffle<T>(this IList<T> ts)
    //     {
    //         var count = ts.Count;
    //         var last = count - 1;
    //         for (var i = 0; i < last; ++i)
    //         {
    //             var r = Random.Range(i, count);
    //             var tmp = ts[i];
    //             ts[i] = ts[r];
    //             ts[r] = tmp;
    //         }
    //     }

    //     public static void Shuffle<T>(this IList<T> ts, int seed)
    //     {
    //         var count = ts.Count;
    //         var last = count - 1;
    //         Random.InitState(seed);
    //         for (var i = 0; i < last; ++i)
    //         {
    //             var r = Random.Range(i, count);
    //             var tmp = ts[i];
    //             ts[i] = ts[r];
    //             ts[r] = tmp;
    //         }
    //     }
    // }
}