# Clean Code Conventions

# Chapter 1: Meaningful Names
## Use Intention-Revealing Names
The name of a variable, function, or class, should answer all the big questions. It should tell you why it exists, what it does, and how it is used.

## Avoid Disinformation
1. Avoid leaving false clues that obscure the meaning of code.
   > Do not refer to a grouping of accounts as an `accountList` unless it’s actually a `List`.
The word **list** means something specific to programmers.

2. Beware of using names which vary in small ways.
   > How long does it take to spot the subtle difference between a `XYZControllerForEfficientHandlingOfStrings` in one module and, somewhere a little more distant, `XYZControllerForEfficientStorageOfStrings`?

## Make Meaningful Distinctions
1. If names must be different, then they should also mean something different.

   > If you have another called `ProductInfo` or `ProductData`, you have made the names different without making them mean anything different. `Info` and `Data` are indistinct noise words like `a`, `an`, and `the`.

## Use Pronounceable Names
```java
class DtaRcrd102 {
   private Date genymdhms;
   private Date modymdhms;
   private final String pszqint = "102";
   /* ... */
};
```
---
```java
class Customer {
   private Date generationTimestamp;
   private Date modificationTimestamp;;
   private final String recordId = "102";
   /* ... */
};
```

## Use Searchable Names
1. Single-letter names can **ONLY** be used as local variables inside short methods.
```java
for (int j=0; j<34; j++) {
   s += (t[j]*4)/5;
}
```
---
```java
int realDaysPerIdealDay = 4;
const int WORK_DAYS_PER_WEEK = 5;
int sum = 0;
for (int j=0; j < NUMBER_OF_TASKS; j++) {
   int realTaskDays = taskEstimate[j] * realDaysPerIdealDay;
   int realTaskWeeks = (realdays / WORK_DAYS_PER_WEEK);
   sum += realTaskWeeks;
}
```

## Avoid Encodings
### Member Prefixes
1. You also don’t need to prefix member variables with m_ anymore. Your classes and functions should be small enough that you don’t need them.
```java
public class Part {
   private String m_dsc; // The textual description
      void setName(String name) {
      m_dsc = name;
   }
}
```
---
```java
public class Part {
String description;
   void setDescription(String description) {
      this.description = description;
   }
}
```

## Class Names
Classes and objects should have noun or noun phrase names like `Customer`, `WikiPage`, `Account`, and `AddressParser`. Avoid words like `Manager`, `Processor`, `Data`, or `Info` in the name of a class. A class name should not be a verb.

## Method Names
Methods should have verb or verb phrase names like `postPayment`, `deletePage`, or `save`. **Accessors**, **mutators**, and **predicates** should be named for their value and prefixed with `get`, `set`, and `is`.

## Pick One Word per Concept
For instance, it’s confusing to have fetch, retrieve, and get as equivalent methods of different classes. How do you remember which method name goes with which class

## Don’t Pun
Avoid using the same word for two purposes. Using the same term for two different ideas is essentially a pun.

## Use Solution Domain Names
Remember that the people who read your code will be programmers. So go ahead and use computer science (CS) terms, algorithm names, pattern names, math terms, and so forth.

## Add Meaningful Context
There are a few names which are meaningful in and of themselves—most are not. Instead, you need to place names in context for your reader by enclosing them in well-named classes, functions, or namespaces. When all else fails, then prefixing the name may be necessary as a last resort.

# Chapter 3: Functions

## Small!
The first rule of functions is that they should be small. The second rule of functions is that _they should be smaller than that_.
   - The blocks within `if` statements, `else` statements, while statements, and so on should be one line long.

   - This also implies that functions should not be large enough to hold nested structures. The indent level of a function should not be greater than one or two.

## Do One Thing
*Function should do one thing. They should do it well. They should do it only.*
   - If a function does only those steps that are one level below the stated name of the function, then the function is doing one thing.

   - The reason we write functions is to decompose a larger concept (in other words, the name of the function) into a set of steps at the next level of abstraction

## One Level of Abstraction per Function
In order to make sure our functions are doing “one thing,” we need to make sure that the statements within our function are all at the same level of abstraction. Example of bad code function:
   > A function which there are concepts in there that are at a very high level of abstraction, such as `getHtml()`; others that are at an intermediate level of abstraction, such as: `String pagePathName = PathParser.render(pagePath);` and still others that are remarkably low level, such as: `.append("\n")`.