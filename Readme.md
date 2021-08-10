# Build with Unity
Version 2019.4.27f1

# Books
- [Clean Code](https://github.com/gameoflord012/ANTs1/blob/main/CleanCodeConventions.md)
- [Refactoring Techniques](https://github.com/gameoflord012/ANTs1/blob/main/RefactoringTechniques.md)

# Useful resources
- https://catlikecoding.com/
- https://www.reddit.com/r/roguelikedev/wiki/faq_friday
- https://blog.unity.com/technology/unity-serialization
- https://learn.unity.com/tutorial/assets-resources-and-assetbundles/?tab=overview#5c7f8528edbc2a002053b5a6
- https://www.tutorialsteacher.com/ioc/dependency-injection
- https://docs.unity3d.com/Manual/ConfigurableEnterPlayMode.html
- https://www.youtube.com/watch?v=9tjYz6Ab0oc

## VFX
- https://realtimevfx.com/t/artistic-principles-of-vfx/4081
- https://www.artstation.com/
- https://www.youtube.com/playlist?list=PLQD_sA-R5qVKVYw3EVuRT7fSJsVukLEhD
- https://realtimevfx.com/t/getting-started-in-real-time-vfx-start-here/3415
- https://www.youtube.com/results?search_query=ImbueFX+

## Udemy Courses
- https://www.udemy.com/share/101Ak22@Pm5jVGJbSFAHdk5GBmJNfj4=/
- https://www.udemy.com/share/101XWK2@PW5gfWFbSFAHdk5GBktnfj5H/
- https://www.udemy.com/share/103t142@PW1jV1pYT1wLe0JGO0tNfQ==/
- https://www.udemy.com/share/101WYy2@Pm5jfWFgS1cLekNKBnZOVD1tYH0=/
- https://www.udemy.com/share/103CaS2@FG5jV0tbSFAHdk5GBktnVA==/
- https://www.udemy.com/share/104YJK2@FEdKV2FKWlQMdk9HCnZzVD1HYw==/
- https://www.udemy.com/share/102Abi2@PW5KfVpYT1wLe0JGOEhO/
- https://www.udemy.com/share/101Xwc2@PW1KVGJbSFAHdk5GBkhOfT5t/
- https://www.udemy.com/share/1020ki2@Pm1gVFpYT1wLe0JGO2Jnfg==/

# Style Guidelines
## Orders
### Elements in class
1. Events.
2. Enums (ODA ie. Order of Decreasing Accessibility).
3. SerializeField (Should be alway `private`, and should not contain keyword `private`, if need public accessibility use `getter` and `setter`).
4. Structs (ODA).
5. Static Variables (ODA).
6. Variables (ODA).
---
6. Getters and Setters.
7. Constructors.
8. Unity event functions (ie. `Update()`, `Start()`, `OnColliderEnter2d`).
9. Custome event functions (ie. `OnMaxHealthUpdate`, `OnBulletFire`)
10. Class behaviours is the last and order base on decreasing level of abstraction rule.

#### Note
- Example of [good script](https://ideone.com/3B85py).
- Every elements must have accessibility keyword (Except for _SerializeField_ is always private).
- All elements are attached to an _Attributes_ must be on newline, except for _SerializeField_ attribute.
  ```c#
  [SerializeField] float timeBetweenFire = 0.5f;
  
  [Range(0f, 1f)] 
  public float airborneDecelProportion;
  ```
- **Static** keyword should stay before _Accessibility_.

  ```c#
  static protected PlayerCharacter playerInstance;
  ```
### Namespace
1. System
2. Unity
3. Project dependencies
```c#
using System.Collections.Generic
using UnityEngine
using ANTs.Core
```
In visual studio, use shortcut _ctrl + R + G_ to remove unused dependencies.

## Instantiate
All _instantiate_ `GameObject` must be controlled by object pooling.

## Awake and Start
- Uncontrolled MonoBehaviour _methods_ shouldn't be called by `Awake`.
- Uncontrolled _variables_ shound't be initialized on `Start`.
- If variable must be initialized on `Start`, use **lazy initialization**.
- Awake is called immediately after `Instantiate`.

## Unity tips

Don't return _null_ instead return _empty_ or _throw UnityException_
```c#
public DogBreed CreateDog(string s)
{
  switch(s)
  {
    case "Chihuahua":
      return new Chihuahua();
      break;
    case "ShibaInu":
      return new ShibaInu();
      break;
    default:
      // Instead of return null throw exception
      return new UnityException("Invalid dog breed!");
  }
}
```

### Casting
It's recommended to use safely cast rather than return null cast

prefer this
```C#
ProjectileWeapon pWeapon = (ProjectileWeapon)weapon;
```
rather this
```C#
ProjectileWeapon pWeapon = weapon as ProjectileWeapon
```
