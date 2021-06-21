# Build with Unity
Version 2019.4.27f1

# Books
[Clean Code](https://github.com/gameoflord012/ANTs1/blob/main/CleanCodeConventions.md)

[Refactoring Techniques](https://github.com/gameoflord012/ANTs1/blob/main/RefactoringTechniques.md)

# Style Guidelines
## Order of elements in class
1. Events.
2. SerializeField (Should be alway `private`, and should not contain keyword `private`, if need public accessibility use `getter` and `setter`).
3. Enums (ODA ie. Order of Decreasing Accessibility).
4. Structs (ODA).
5. Statics (ODA).
6. Variables (ODA).
---
6. Getters and Setters.
7. Constructors.
8. Unity event functions (ie. `Update()`, `Start()`, `OnColliderEnter2d`).
9. Custome event functions (ie. `OnMaxHealthUpdate`, `OnBulletFire`)
10. Class behaviours is the last and order base on decreasing level of abstraction rule.

### Note
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
