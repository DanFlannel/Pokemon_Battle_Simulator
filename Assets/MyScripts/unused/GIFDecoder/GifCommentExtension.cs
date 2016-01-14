using UnityEngine;
using System.Collections;

public class GifCommentExtension : MonoBehaviour {

	public struct CommentExtension
    {
        public const string Introductor = "21";
        public const string Label = "FE";
        public string Text { get; set; }

        public void Set(string stream)
        {
            string streamIntroductor = stream.Substring(0, 2);
            string streamLabel = stream.Substring(2, 2);

            if(Label != streamLabel)
            {
                Debug.LogError("NOT A COMMENT EXTENSION");
            }
        }
    }
}
