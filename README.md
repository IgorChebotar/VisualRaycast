# Visual raycast [Download](https://github.com/IgorChebotar/VisualRaycast/releases)
Raycast utilities and visualizer for Unity (all raycast types supported). Not required any additional components or interfaces on your objects. No conflicts with the standard unity raycast.
 

**Author:** Igor-Valerii Chebotar
**Email:**  igor.valerii.chebotar@gmail.com


## Requirements
* [Simple Man - Utilities](https://github.com/IgorChebotar/Utilities/releases)


## How to install plugin?
Open installer by the click on Tools -> Simple Man -> Master Installer -> [Plugins' name] -> Click 'Install' button. If you don't have one or more of the plugins this plugin depends on, you must install it first.

## Quick start
1. Add 'Raycast drawer' game object on your scene by right click inside the 'Hierarchy' window and select 'Raycast Drawer' option.
2. Open your C# class that calls '_Phisics.Raycast_' function (or create new C# MonoBehavior class) and add a _'using SimpleMan.VisualRaycast'_ line.
3. Replace standart raycast call on '_Phisics.Raycast_' line on _'this.Raycast'_. See example below:

```C#
using UnityEngine;
using SimpleMan.VisualRaycast;

public class SomeClass : MonoBehaviour
{
    private void Update()
    {
        //BEFORE:
        if(Physics.Raycast(transform.position, transform.forward))
        {
            //Do some action
        }
        
        //AFTER:
        if(this.Raycast(transform.position, transform.forward))
        {
            //Do some action
        }
    }
}
```
4. Done! 
All examples you also can find in 'Demo' package.

## Using box and sphere casts
1. Follow the above written steps
2. Use '_this.Spherecast_' or '_this.Boxcast_' instead of _'this.Raycast'_. 
3. Done! 

## Using box and sphere overlap functions
1. Follow the above written steps
2. Use '_this.SphereOverlap_' or '_this.BoxOverlap'_. 
3. Done! 

## Can I use layer masks and other standard raycast arguments?
Sure! Visual raycast has all arguments of classic raycast. Also it has '_ignoreSelf_' parameter. It switched on by default. Switch it off, if your caster game object needs to cast itself.


## Extension functions for the 'Component'
You can use this functions from each of your MonoBehavior classes. **Don't forget to write _'this.'_ keyword to use exension functions.** 

#### Methods
| Function name | Description                    |
| ------------- | ------------------------------ |
| Raycast |Make ray cast|
| Boxcast |Make box cast (sphere on end of line)|
| Spherecast |Make sphere cast (sphere on end of line)|
| BoxOverlap |Check the area by box|
| SphereOverlap |Check the area by sphere|

## Visual raycast drawer component
Handles visualization of raycast operations

#### Properties
| Property name | Description                    |
| ------------- | ------------------------------ |
| Instance |Static property with current class instance|
| FadeTime |Visualization lifetime|
| HitIndicatorScale |Scale of hit indicators (small spheres)|
| HitColor |Color of casts, that hit something|
| NoHitColor |Color of casts, that hit not anything|


