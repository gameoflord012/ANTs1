# Build with Unity
Version 2019.4.27f1

# Books
[Clean Code](https://github.com/gameoflord012/ANTs1/blob/main/CleanCodeConventions.md)

[Refactoring Techniques](https://github.com/gameoflord012/ANTs1/blob/main/RefactoringTechniques.md)

# Code Convetions
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
8. Unity event functions (ie. `Update()`, `Start()`).
9. Class behaviours is the last and order base on decreasing level of abstraction rule.

### Note
- Every elements must have accessibility keyword (Except for **SerializeField** is always private).
- All elements are attached to an `attributes` must be on newline.
  ```c#
  [Range(0f, 1f)] 
  public float airborneDecelProportion;
  ```
- `Static` keyword should stay before `Accessibility`.

  ```c#
  static protected PlayerCharacter playerInstance;
  ```
