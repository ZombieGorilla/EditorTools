// -----------------------------------------------------------------------
//  <copyright file="ColorHelperDrawer.cs">
//      Copyright (c) 2017 Scott M. Hill - MIT License
//  </copyright>
//  <contact>Scott Hill - https://github.com/ZombieGorilla/EditorTools</contact>
//	<description>Inspector Color Helper Drawer</description>
//	<version>2.0.1</version>
// -----------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ColorHelper))]
public class ColorHelperDrawer : PropertyDrawer
{
	ColorHelper color_helper { get { return ((ColorHelper) attribute); } }
    string[] options = new string[] {"Select", "Values", "Alpha", "RGBA", "Saturation"};
	
	// layout settings
	float popup_width = 70f;
	float base_height = 0f;
	float gap = 3f;
	float margin_bottom = 3f;
	float margin = 2f;
	float swatch_margin = 2f;
	float swatch_width = 12f;


	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) 
	{
		base_height = base.GetPropertyHeight(property, label);
		float h = (color_helper.display_option==3) ? base_height*5 : (color_helper.display_option==0) ? base_height : base_height*2;
		return h+margin_bottom+margin+margin;
	}


	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) 
	{
		Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.y+=margin;
			contentPosition.height=base_height;
			contentPosition.x+=margin;
			contentPosition.width -= (margin*2);
			
		// draw background
		Rect bkg = EditorGUI.PrefixLabel(position, label);
			bkg.height-=margin_bottom;

		GUI.Box(bkg, "", new GUIStyle("ShurikenEffectBg"));			
		
		// main block
		label = EditorGUI.BeginProperty(position, label, property);
		
		// color field
		Rect main_cont = contentPosition;
			main_cont.x+=popup_width; 
			main_cont.width-=(popup_width);
		
		EditorGUI.indentLevel = 0;
		EditorGUI.PropertyField(main_cont, property, GUIContent.none);
		
		
		// selection popup
		Rect popupPosition = contentPosition;
			popupPosition.width = popup_width-gap;
		
	    color_helper.display_option = EditorGUI.Popup(popupPosition,color_helper.display_option,options);
		
		// content
		contentPosition.y +=base_height;
		
		Rect swatchPosition = contentPosition;
			swatchPosition.x = contentPosition.width+contentPosition.x - swatch_width;
			swatchPosition.width = swatch_width;
		
		Color c = property.colorValue;

		switch (color_helper.display_option) 
		{
			case 1: // value string
			{
				EditorGUI.TextField(contentPosition, c.r+","+c.g+","+c.b+","+c.a );	
				break;
			}
			case 2:  // alpha slider
			{
				contentPosition.width -= (swatch_width+gap);
				c.a = ColorSlider(c, contentPosition, 3,false);
				property.colorValue = c;
				break;
			}
			case 3: // RGBA
			{
				contentPosition.width -= (swatch_width+gap);
				c.r = ColorSlider(c, contentPosition, 0);
				c.g = ColorSlider(c, contentPosition, 1);
				c.b = ColorSlider(c, contentPosition, 2);
				c.a = ColorSlider(c, contentPosition, 3);
				property.colorValue = c;
				break;
			}
			case 4: // Saturation
			{
				contentPosition.width -= (swatch_width+gap);
				property.colorValue = SaturationSlider(c, contentPosition);
				break;
			}
		}
		EditorGUI.EndProperty();
	}
	
	
	float ColorSlider(Color c, Rect base_pos, int pos) { return ColorSlider(c, base_pos, pos, true); }
	float ColorSlider(Color c, Rect base_pos, int index, bool use_pos)
	{
		if(use_pos) base_pos.y+=(index*base_height);
		float cv = DrawSlider(c[index],base_pos);
		if(index != 3)
		{
			c=Color.black;
			c[index] = cv;
		}
		DrawSwatch(c,base_pos);
		return cv;
	}
	
	
	Color SaturationSlider(Color c, Rect base_pos)
	{
		float s,v,h;
		Color.RGBToHSV(c, out h, out s, out v);
		s = DrawSlider(s,base_pos, 0.00001f);
		c =  Color.HSVToRGB(h,s,v);
		DrawSwatch(c,base_pos);
		return c;
	}
	
	
	float DrawSlider(float v, Rect base_pos) { return DrawSlider(v,base_pos,0f); }
	float DrawSlider(float v, Rect base_pos, float min)
	{
		base_pos.x+=2; // fix odd positioning for slider
		return EditorGUI.Slider(base_pos, v, min, 1f);
	}
	
	
	void DrawSwatch(Color c, Rect base_pos)
	{
		base_pos.x =  base_pos.x+base_pos.width+gap+swatch_margin-1;
		base_pos.y +=  swatch_margin-1;
		base_pos.width = swatch_width-(swatch_margin*2);
		base_pos.height -= (swatch_margin*2)+1;
		
		GUIStyle g = new GUIStyle("ProfilerTimelineBar");
			g.margin = new RectOffset(0,0,0,0);
			g.padding = new RectOffset(0,0,0,0);
		
		GUI.color = c;
		GUI.Box(base_pos, "",g);
		GUI.color = Color.white;
	}
}


