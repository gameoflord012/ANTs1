# Build with Unity
Version 2019.4.27f1

# Books
- [Clean Code](https://github.com/gameoflord012/ANTs1/blob/main/CleanCodeConventions.md)
- [Refactoring Techniques](https://github.com/gameoflord012/ANTs1/blob/main/RefactoringTechniques.md)

# Useful resources
- https://forum.unity.com/threads/serialization-best-practices-megapost.155352/
- https://www.reddit.com/r/roguelikedev/comments/3jk3xm/faq_friday_20_saving/
- https://catlikecoding.com/
- https://www.reddit.com/r/roguelikedev/wiki/faq_friday

# Style Guidelines
## Orders
### Elements in class
1. Events.
2. Enums (ODA ie. Order of Decreasing Accessibility).
3. SerializeField (Should be alway `private`, and should not contain keyword `private`, if need public accessibility use `getter` and `setter`).
4. Structs (ODA).
5. Statics (ODA).
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
`GetComponent` should be call on `Awake` event
```c#
private void Awake()
{
  mover = GetComponent<Mover>();
}
```

If a class call another class, prefer `Start()`
```c#
protected virtual void Start()
{
  if (actionStartOnPlay)
    GetComponent<ActionScheduler>().Trigger(this);
}
```

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
