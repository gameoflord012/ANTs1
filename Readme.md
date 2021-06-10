# Table Of Contents
- [Build with Unity](#build-with-unity)
- [Editors](#editors)
- [Cleancode Code Conventions](#cleancode-code-conventions)
	- [Use Intention-Revealing Names](#use-intention-revealing-names)
	- [Avoid Disinformation](#avoid-disinformation)
	- [Make Meaningful Distinctions](#make-meaningful-distinctions)


# Build with Unity
Version 2019.4.26f1

# Editors
Use Vscode

# Cleancode Code Conventions

## Use Intention-Revealing Names
The name of a variable, function, or class, should answer all the big questions. It
should tell you why it exists, what it does, and how it is used. 

## Avoid Disinformation
1. Avoid leaving false clues that obscure the meaning of code.
   > Do not refer to a grouping of accounts as an `accountList` unless it’s actually a `List`.
The word **list** means something specific to programmers.

2. Beware of using names which vary in small ways.
   > How long does it take to spot the subtle difference between a `XYZControllerForEfficientHandlingOfStrings` in one module
and, somewhere a little more distant, `XYZControllerForEfficientStorageOfStrings`?

## Make Meaningful Distinctions
1. If names must be different, then they should also mean something different.

   > If you have another called `ProductInfo` or `ProductData`, you have made the names different without making them mean anything different. `Info` and `Data` are indistinct noise words like `a`, `an`, and `the`.

