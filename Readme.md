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

## Use Pronounceable Names

Compare
```c#
   class DtaRcrd102 {
      private Date genymdhms;
      private Date modymdhms;
      private final String pszqint = "102";
      /* ... */
};
```
to
```c#
   class Customer {
      private Date generationTimestamp;
      private Date modificationTimestamp;;
      private final String recordId = "102";
      /* ... */
   };
```
