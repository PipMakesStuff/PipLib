using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using TMPro;



namespace PipLib
{
        
    public class Util
    { 
        //Allows for a quick and easy way to point something towards something else in 2D,
        //If you want it to track that object call it in the update. 
        public static void PointToward2D(Transform _pointer, Transform _target)
        {
            Vector2 dir = _pointer.position - _target.position;
            _pointer.transform.right = dir;
        }

        //Quick refrence to where the mouse is in the world on screen with a z value of 10. 
        //This is primarly for working in Unity2D.
        public static Vector3 MousePos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        //This will randomly shake the object, or set it to its original position if it's been shaken in the previous frame. 
        //AKA calling this function will shake this object within the same bounds with one frame displacing and the other frame repositioning.
        public static void Shake(Transform _position, float? amount = null, Vector3? originalPos = null)
        {
            if (originalPos == null|| originalPos == _position.position)
            {
                _position.position += new Vector2(UnityEngine.Random.Range(-1, 2) * (amount ?? 1), UnityEngine.Random.Range(-1, 2) * (amount ?? 1)).ToVector3();
            }
            else if(originalPos != _position.position)
            {
                _position.position = originalPos??_position.position;
            }
                        
        }

        //Returns a float with an absolute range, with a optional offset. 
        //For example Util.AbsRand(5, 20) would give you something between 15 and 25. 
        //Util.AbsRand(3) would give you between -3 and 3.
        public static float AbsRand(float absoluteRange, float? offset = null)
        {
            return UnityEngine.Random.Range(-absoluteRange, absoluteRange)+offset??0;
        }

        //This is a bit of a dirty method to use, but if you need to really set the volume you can.
        //I'd also like to point out that it will only change the objects in the current scene
        //If you Instansiate anything else it will have its default volume. 
        public static void UpdateGlobalVolume(float volumeMod)
        {
            AudioSource[] audioSources = MonoBehaviour.FindObjectsOfType<AudioSource>();
            for (int i = 0; i < audioSources.Length; i++)
            {          
                    audioSources[i].volume *= volumeMod;               
            }
        }

        //This allows you to add text to a string. For example if you want say "Hi , it's been a lovely day"
        // and you go with InjectString(target, NAME, 4), then it will return "Hi NAME, it's been a lovely day"
        public static string InjectString(string target, string Injection, int? index = 0) 
        {
            StringBuilder assembly = new StringBuilder();
            string temp1 ="", temp2 ="";
            if ((index??0) > 0)
            {
                for (int i = 0; i < index; i++)
                {
                    temp1 += target[i];
                }
                for (int i = index??0; i < target.Length; i++)
                {
                    temp2 += target[i];
                }
                assembly.Append(temp1);
                assembly.Append(Injection);
                assembly.Append(temp2);
                return assembly.ToString();

            }
            else
            {
                assembly.Append(Injection);
                assembly.Append(target);
                return assembly.ToString();
            }

        }
    }


    
    public static class Extensions
    {
        //This gives the option to do string.WordCount() and return the number of words in the string.
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        //This RETURNS the value of the passed in coordinates. To actually set it you need to do something like: transform.position = transform.position.With(z: 5);
        public static Vector3 With(this Vector3 original,float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x??original.x,y??original.y,z??original.z);
        }
        
        //This RETURNS the value of the vector with the coordinates passed, but instead of Setting the coordinated, it Adds them from the original. 
        public static Vector3 Add(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x + original.x ?? original.x, y + original.y ?? original.y, z + original.z ?? original.z);
        }
        //This is an Overloaded version of the above Function, incase I just want to add two vectors together.
        public static Vector3 Add(this Vector3 original, Vector3 vector3)
        {
            return original + vector3;
        }

        //Used to flatten the y value to 0;
        public static Vector3 Flat(this Vector3 original)
        {
            return new Vector3(original.x, 0, original.z);
        }

        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source);
        }

        //This allows you attach one object to another. Again you'll want to go with transform.position = transform.position.Attach(target).
        public static Vector3 Attach(this Vector3 vec, Transform _target, Vector3? _offset = null)
        {
           return ((_offset ?? Vector3.zero) + _target.position);
          
        }

        // Vector2 -> Vector3
        public static Vector3 ToVector3(this Vector2 original, float? z = null)
        {
            return new Vector3(original.x, original.y, z ?? 0);
        }

        // Vector3 -> Vector2
        public static Vector2 ToVector2(this Vector3 original)
        {
            return new Vector2(original.x, original.y);
        }
        
        //Turns a Bool into either a 1 or 0 for you. 
        public static int ToInt(this bool state)
        {
            if (state)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //Turns an int into either a true or false for you. 
        public static bool ToBool(this int oneOrZero)
        {
            if (oneOrZero==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        
    }



    public static class UserInfo
    {
        public static string UsersFolder()
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);            
            string tempname = docPath.ToString();
            string assembler = "";
            for (int i = 0; i < tempname.Length - 9; i++)
            {
                assembler += tempname[i + 9];
            }
            string name = assembler;
            return name;
        }

        public static float BatteryLife()
        {
            return SystemInfo.batteryLevel;
        }

    }



    //EXPERIMENTAL
    /*
    public class Printer
    {
        public List<string> sentencelist = new List<string>();
        public float typingSpeed;
        public float cooldown;

        private int index;
        private TextMeshProUGUI PROtext;
        private Text text;
        private float t;

        public void AddToPrinter(string _text)
        {
            sentencelist.Add(_text);
        }

        public void SelectText(TextMeshProUGUI textElement)
        {
            PROtext = textElement;
        }

        public void SelectText(Text textElement)
        {
            text = textElement;
        }
        public void Print()
        {
            if (PROtext == null)
            {
                
                foreach (char letter in sentencelist[index].ToCharArray())
                {
                    t = typingSpeed;
                    text.text += letter;
                    while (t<=0)
                    {
                        t -= Time.deltaTime;
                    }
                }
            }
            else
            {
                foreach (char letter in sentencelist[index].ToCharArray())
                {
                    t = typingSpeed;
                    PROtext.text += letter;
                    while (t <= 0)
                    {
                        t -= Time.deltaTime;
                    }
                }
            }
        }
        
    }
    */

}
