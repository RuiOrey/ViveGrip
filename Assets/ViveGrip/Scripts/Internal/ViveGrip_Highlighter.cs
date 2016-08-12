using UnityEngine;
using System.Collections.Generic;

public class ViveGrip_Highlighter : MonoBehaviour {
  private Color tintColor = new Color(0.2f, 0.2f, 0.2f);
  private Queue<Color> oldColors = new Queue<Color>();

  void Start () {}

  public void Highlight(Color color) {
    RemoveHighlighting();
    foreach (Material material in GetComponent<Renderer>().materials) {
      Color currentColor = material.color;
      oldColors.Enqueue(currentColor);
      Color tintedColor = currentColor + color;
      material.color = tintedColor;
    }
  }

  public void RemoveHighlighting() {
    foreach (Material material in GetComponent<Renderer>().materials) {
      if (oldColors.Count == 0) { break; }
      material.color = oldColors.Dequeue();
    }
    oldColors.Clear();
  }

  void ViveGripHighlightStart() {
    Highlight(tintColor);
  }

  void ViveGripHighlightStop() {
    RemoveHighlighting();
  }

  void OnDestroy() {
    RemoveHighlighting();
  }
}