// -----------------------------------------------------------------------
//  <copyright file="ColorHelper.cs">
//      Copyright (c) 2017 Scott M. Hill - MIT License
//  </copyright>
//  <contact>Scott Hill - https://github.com/ZombieGorilla/EditorTools</contact>
//	<description>Inspector Color Helper Attribute</description>
//	<version>2.0.0</version>
// -----------------------------------------------------------------------

using UnityEngine;
using System;

public class ColorHelper : PropertyAttribute
{
	public int display_option = 0;
	
	public ColorHelper () { }
	
	public ColorHelper (int display_op) 
	{
		this.display_option = display_op;
	}
	
	public ColorHelper (string display_op) 
	{
		display_op = display_op.ToLower();
		switch (display_op) 
		{
			case "select": this.display_option = 0; break;
			case "values": this.display_option = 1; break;
			case "alpha": this.display_option = 2; break;
			case "rgba": this.display_option = 3; break;
			case "saturation": this.display_option = 4; break;
		}
	}
}




