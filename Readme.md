# Build with Unity
Version 2019.4.27f1

# Clean Code Books
[Link](https://github.com/gameoflord012/ANTs1/blob/main/CleanCodeConventions.md)

# Refactoring Techniques
[Link](https://github.com/gameoflord012/ANTs1/blob/main/RefactoringTechniques.md)

# Code Convetions
## Order of elements in class
1. Events
2. SerializeField (Should be alway `private`, and should not contain keyword `private`, if need public accessibility use `getter` and `setter`)
3. Enums (OBA ie. Order By Accessibility)
4. Statics (OBA)
5. Variables (OBA)
---
6. Getters and Setters
7. Constructors
8. Unity event functions (ie. `Update()`, `Start()`)
9. Class behaviours is the last and order base on decreasing level of abstraction rule.

### Notes
**Statics** and **Variables** must have accessibility keyword
