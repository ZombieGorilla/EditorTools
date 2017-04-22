// -----------------------------------------------------------------------
//  <copyright file="ColorHelperExample.cs">
//      Copyright (c) 2017 Scott M. Hill - MIT License
//  </copyright>
//  <contact>Scott Hill - https://github.com/ZombieGorilla/EditorTools</contact>
//	<description>Example script for Color Helper</description>
//	<version>1.0.0</version>
// -----------------------------------------------------------------------

using UnityEngine;

public class ColorHelperExample : MonoBehaviour {

	[Header("Color Helper Examples")]
	
	[ColorHelper()] // or [ColorHelper("Select")] or [ColorHelper(0)]  
	[SerializeField] public Color OptionSelect;
	
	[ColorHelper("Values")] // or [ColorHelper(1)] 
	[SerializeField] public Color OptionValues;
	
	[ColorHelper("Alpha")] // or [ColorHelper(2)] 
	[SerializeField] public Color OptionAlpha;
	
	[ColorHelper("RGBA")] // or [ColorHelper(3)] 
	[SerializeField] public Color OptionRGBA;
	
	[ColorHelper("Saturation")] // or [ColorHelper(4)] 
	[SerializeField] public Color OptionSaturation;
	
	void Start () { }
}
