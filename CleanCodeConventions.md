# Clean Code Conventions

# Chapter 1: Meaningful Names
## Use Intention-Revealing Names
The name of a variable, function, or class, should answer all the big questions. It should tell you why it exists, what it does, and how it is used.

## Avoid Disinformation
Avoid leaving false clues that obscure the meaning of code.
   - Do not refer to a grouping of accounts as an `accountList` unless it’s actually a `List`.
The word **list** means something specific to programmers.

Beware of using names which vary in small ways.
   - How long does it take to spot the subtle difference between a `XYZControllerForEfficientHandlingOfStrings` in one module and, somewhere a little more distant, `XYZControllerForEfficientStorageOfStrings`?

## Make Meaningful Distinctions
If names must be different, then they should also mean something different.

   - If you have another called `ProductInfo` or `ProductData`, you have made the names different without making them mean anything different. `Info` and `Data` are indistinct noise words like `a`, `an`, and `the`.

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
Single-letter names can **ONLY** be used as local variables inside short methods.
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
You also don’t need to prefix member variables with m_ anymore. Your classes and functions should be small enough that you don’t need them.
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
For instance, it’s confusing to have `fetch`, `retrieve`, and `get` as equivalent methods of different classes. How do you remember which method name goes with which class

## Don’t Pun
Avoid using the same word for two purposes. Using the same term for two different ideas is essentially a pun.

## Use Solution Domain Names
Remember that the people who read your code will be programmers. So go ahead and use computer science (CS) terms, algorithm names, pattern names, math terms, and so forth.

## Add Meaningful Context
There are a few names which are meaningful in and of themselves—most are not. Instead, you need to place names in context for your reader by enclosing them in well-named classes, functions, or namespaces. When all else fails, then prefixing the name may be necessary as a last resort.

# Chapter 2: Functions

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
   - A function which there are concepts in there that are at a very high level of abstraction, such as `getHtml()`; others that are at an intermediate level of abstraction, such as: `String pagePathName = PathParser.render(pagePath);` and still others that are remarkably low level, such as: `.append("\n")`.

## Reading Code from Top to Bottom: The Stepdown Rule
We want every function to be followed by those at the next level of abstraction so that we can read the program, descending one level of abstraction at a time as we read down the list of functions.

## Switch Statements
Unfortunately we can’t always avoid switch statements, but we can make sure that each `switch` statement is buried in a low-level class and is never repeated. We do this, of course, with polymorphism.
```java
public Money calculatePay(Employee e)
   throws InvalidEmployeeType {
   switch (e.type) {
      case COMMISSIONED:
         return calculateCommissionedPay(e);
      case HOURLY:
         return calculateHourlyPay(e);
      case SALARIED:
         return calculateSalariedPay(e);
      default:
         throw new InvalidEmployeeType(e.type);
   }
}
```
There are several problems with this function:
- It’s large, and when new employee types are added, it will grow.
- It very clearly does more than one thing.
- It violates the [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle) because there is more than one reason for it to change.
-  It violates the [Open Closed Principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) because it must change whenever new types are added.

The solution to this problem (see Listing 3-5) is to bury the switch statement in the basement of an [ABSTRACT FACTORY](https://refactoring.guru/design-patterns/abstract-factory).
```java
public abstract class Employee {
   public abstract boolean isPayday();
   public abstract Money calculatePay();
   public abstract void deliverPay(Money pay);
}
-----------------
public interface EmployeeFactory {
   public Employee makeEmployee(EmployeeRecord r) throws InvalidEmployeeType;
}
-----------------
public class EmployeeFactoryImpl implements EmployeeFactory {
   public Employee makeEmployee(EmployeeRecord r) throws InvalidEmployeeType {
      switch (r.type) {
      case COMMISSIONED:
         return new CommissionedEmployee(r) ;
      case HOURLY:
         return new HourlyEmployee(r);
      case SALARIED:
         return new SalariedEmploye(r);
      default:
         throw new InvalidEmployeeType(r.type);
      }
   }
}
```

## Use Descriptive Names
- Don’t be afraid to spend time choosing a name.

- Choosing descriptive names will clarify the design of the module in your mind and help you to improve it.

- Be consistent in your names. Use the same phrases, nouns, and verbs in the function names you choose for your modules.

## Function Arguments
Three arguments should be avoided where possible. More than three requires very special justification—and then shouldn’t be used anyway.

### Common Monadic Forms (Single Argument Function)
You may be asking a question about that argument, as in `boolean fileExists(“MyFile”).`

Or you may be operating on that argument, transforming it into something else and returning it. For example, `InputStream fileOpen(“MyFile”)` transforms a file name String into an InputStream return value.
### Flag Arguments
Flag arguments are ugly. Passing a boolean into a function is a truly terrible practice, We should have split the function in two instead.
   > Still, the method call `render(true)` is just plain confusing to a poor reader. Mousing over the call and seeing `render(boolean isSuite)` helps a little, but not that much. We should have split the function into two: `renderForSuite()` and `renderForSingleTest()`.

### Dyadic Functions
There are times, of course, where two arguments are appropriate. For example,
Point `p = new Point(0,0);` is perfectly reasonable.

You should be aware that they come at a cost and should take advantage of what mechanims may be available to you to convert them into monads.
   > For example, you might make the `writeField` method a member of `outputStream` so that you can say `outputStream.writeField(name)`. Or you might make the `outputStream` a member variable of the current class so that you don’t have to pass it. Or you might extract a new class like `FieldWriter` that takes the `outputStream` in its constructor and has a write method.

### Argument Objects
When a function seems to need more than two or three arguments, it is likely that some of those arguments ought to be wrapped into a class of their own.
```java
Circle makeCircle(double x, double y, double radius);
Circle makeCircle(Point center, double radius);
```

### Argument Lists
Consider, for example, the `String.format` method:
```java
String.format("%s worked %.2f hours.", name, hours);
```
If the variable arguments are all treated identically, as they are in the example above, then they are equivalent to a single argument of type `List`.

### Verbs and Keywords
In the case of function with only argument, the function and argument should form a very nice verb/noun pair. For example, `write(name)` is very evocative.

Using this form we encode the names of the arguments into the function name. For example, `assertEquals` might be better written as `assertExpectedEqualsActual(expected, actual)`.

### Output Arguments
Arguments are most naturally interpreted as inputs to a function. For example:
```java
appendFooter(s);
```
Does this function append s as the footer to something? Or does it append some footer to s? Is s an input or an output? It doesn’t take long to look at the function signature and see:
```java
public void appendFooter(StringBuffer report)
```
In other words, it would be better for appendFooter to be invoked as
```java
report.appendFooter();
```

### Prefer Exceptions to Returning Error Codes
When you return an error code, you create the problem that the caller must deal with the error immediately.

```java
if (deletePage(page) == E_OK)
```
---
```java
if (deletePage(page) == E_OK) {
   if (registry.deleteReference(page.name) == E_OK) {
      if (configKeys.deleteKey(page.name.makeKey()) == E_OK){
         logger.log("page deleted");
      } else {
         logger.log("configKey not deleted");
      }
   } else {
      logger.log("deleteReference from registry failed");
   }
} else {
   logger.log("delete failed");
   return E_ERROR;
}
```
On the other hand, if you use exceptions instead of returned error codes, then the error processing code can be separated from the happy path code and can be simplified:
```java
try {
   deletePage(page);
   registry.deleteReference(page.name);
   configKeys.deleteKey(page.name.makeKey());
}
catch (Exception e) {
   logger.log(e.getMessage());
}
```

## Don’t Repeat Yourself
Duplication may be the root of all evil in software. Many principles and practices have been created for the purpose of controlling or eliminating it.

## Structured Programming
Dijkstra said that every function, and every block within a function, should have one entry and one exit. 
   > Following these rules means that there should only be one return statement in a function, no `break` or `continue` statements in a loop, and never, ever, any `goto` statements.

So if you keep your functions small, then the occasional multiple `return`, `break`, or `continue` statement does no harm and can sometimes even be more expressive than the single-entry, single-exit rule. On the other hand, `goto` only makes sense in large functions, so it should be avoided.

## How Do You Write Functions Like This?
When I write functions, they come out long and complicated. They have lots of indenting and nested loops. They have long argument lists. The names are arbitrary, and there is duplicated code. But I also have a suite of unit tests that cover every one of those clumsy lines of code.

So then I massage and refine that code, splitting out functions, changing names, eliminating duplication. I shrink the methods and reorder them. Sometimes I break out whole classes, all the while keeping the tests passing.

In the end, I wind up with functions that follow the rules I’ve laid down in this chapter. I don’t write them that way to start. I don’t think anyone could.

# Chapter 3: Formatting

## Vertical Formatting

### Vertical Openness Between Concepts
Each line represents an expression or a clause, and each group of lines represents a complete thought. Those thoughts should be separated from each other with blank lines.

```java
package fitnesse.wikitext.widgets;

import java.util.regex.*;

public class BoldWidget extends ParentWidget {
   public static final String REGEXP = "'''.+?'''";
   private static final Pattern pattern = Pattern.compile("'''(.+?)'''",
      Pattern.MULTILINE + Pattern.DOTALL
   );

   public BoldWidget(ParentWidget parent, String text) throws Exception {
      super(parent);
      Matcher match = pattern.matcher(text);
      match.find();
      addChildWidgets(match.group(1));
   }

   public String render() throws Exception {
      StringBuffer html = new StringBuffer("<b>");
      html.append(childHtml()).append("</b>");
      return html.toString();
   }
}
```
The difference between these two listings is a bit of vertical openness.
```java
package fitnesse.wikitext.widgets;
import java.util.regex.*;
public class BoldWidget extends ParentWidget {
   public static final String REGEXP = "'''.+?'''";
   private static final Pattern pattern = Pattern.compile("'''(.+?)'''",
      Pattern.MULTILINE + Pattern.DOTALL);
   public BoldWidget(ParentWidget parent, String text) throws Exception {
      super(parent);
      Matcher match = pattern.matcher(text);
      match.find();
      addChildWidgets(match.group(1));}
   public String render() throws Exception {
      StringBuffer html = new StringBuffer("<b>");
      html.append(childHtml()).append("</b>");
      return html.toString();
   }
}
```
### Vertical Distance
We want to avoid forcing our readers to hop around through our source
files and classes.

   - #### Variable Declarations
   Variables should be declared as close to their usage as possible. Because our functions are very short, local variables should appear a the top of    each function.

   - #### Instance variables
   on the other hand, should be declared at the top of the class.

   - #### Dependent Functions
   If one function calls another, they should be vertically close,
and the caller should be above the callee, if at all possible

   - #### Conceptual Affinity
Certain bits of code want to be near other bits. They have a certain conceptual affinity. Affinity might be caused because a group of functions perform a similar operation.

```java
public class Assert {
   static public void assertTrue(String message, boolean condition) {
      if (!condition)
         fail(message);
   }
   static public void assertTrue(boolean condition) {
      assertTrue(null, condition);
   }
   static public void assertFalse(String message, boolean condition) {
      assertTrue(message, !condition);
   }
   static public void assertFalse(boolean condition) {
      assertFalse(null, condition);
   }
...
```

# Chapter 4: Objects and Data Structures
## Data Abstraction
Both represent the data of a point on the Cartesian plane. And yet one exposes its implementation and the other completely hides it.

```java
// Abstract Point
public interface Point {
   double getX();
   double getY();
   void setCartesian(double x, double y);
   double getR();
   double getTheta();
   void setPolar(double r, double theta);
}
```

The methods enforce an access policy. You can read the individual coordinates independently, but you must set the coordinates together as an atomic operation.

```java
// Concrete Point
public class Point {
   public double x;
   public double y;
}
```

On the other hand, is very clearly implemented in rectangular coordinates, and it forces us to manipulate those coordinates independently. This exposes implementation.

Hiding implementation is not just a matter of putting a layer of functions between the variables. Hiding implementation is about abstractions!

## Data/Object Anti-Symmetry

>Procedural code (code using data structures) makes it easy to add new functions without changing the existing data structures. OO code, on the other hand, makes it easy to add new classes without changing existing functions.

The complement is also true:
> Procedural code makes it hard to add new data structures because all the functions must change. OO code makes it hard to add new functions because all the classes must change

In any complex system there are going to be times when we want to add new data types rather than new functions. For these cases objects and OO are most appropriate.

## The Law of Demeter
The Law of Demeter says that a method f of a class C should only call
the methods of these:
   - `C`
   - An object created by `f`
   - An object passed as an argument to `f`
   - An object held in an instance variable of `C`

The method should not invoke methods on objects that are returned by any of the allowed functions. In other words, talk to friends, not to strangers.
The following code appears to violate the Law of Demeter:
```java
final String outputDir = ctxt.getOptions().getScratchDir().getAbsolutePath();
```
### Train Wrecks
This kind of code is often called a train wreck because it look like a bunch of coupled train cars. Chains of calls like this are generally considered to be sloppy style and should be avoided.

It is usually best to split them up as follows:
```java
Options opts = ctxt.getOptions();
File scratchDir = opts.getScratchDir();
final String outputDir = scratchDir.getAbsolutePath();
```

If they are objects, then their internal structure should be hidden rather than exposed, and so knowledge of their innards is a clear violation of the Law of Demeter. On the other hand, if `ctxt`, `Options`, and `ScratchDir` are just data structures with no behavior, then they naturally expose their internal structure, and so Demeter does not apply.
```java
final String outputDir = ctxt.options.scratchDir.absolutePath;
```

### Hybrids
This confusion sometimes leads to unfortunate hybrid structures that are half object and half data structure.

Such hybrids make it hard to add new functions but also make it hard to add new data structures. They are the worst of both worlds. Avoid creating them.

### Hiding Structure
because objects are supposed to hide their internal structure, we should not be able to navigate through them. How then would we get the absolute path of the scratch directory
```java
ctxt.getAbsolutePathOfScratchDirectoryOption();
```
or
```java
ctx.getScratchDirectoryOption().getAbsolutePath()
```
The first option could lead to an explosion of methods in the ctxt object. The second presumes that `getScratchDirectoryOption()` returns a data structure, not an object.

Consider this code from (many lines farther down in) the same module:
```java
String outFile = outputDir + "/" + className.replace('.', '/') + ".class";
FileOutputStream fout = new FileOutputStream(outFile);
BufferedOutputStream bos = new BufferedOutputStream(fout);
```
We see that the intent of getting the absolute path of the scratch directory was to create a scratch file of a given name.

If ctxt is an object, we should be telling it to do *something*. So, what if we told the ctxt object to do this?
```java
BufferedOutputStream bos = ctxt.createScratchFileStream(classFileName);
```

# Chapter 5: Error Handling
## Don’t Return Null
```java
public void registerItem(Item item) {
   if (item != null) {
      ItemRegistry registry = peristentStore.getItemRegistry();
      if (registry != null) {
         Item existing = registry.getItem(item.getID());
         if (existing.getBillingPeriod().hasRetailOwner()) {
            existing.register(item);
         }
      }
   }
}
```
If you work in a code base with code like this, it might not look all that bad to you, but it is bad! When we return null, we are essentially creating work for ourselves and foisting problems upon our callers.

Imagine that you have code like this:
```java
List<Employee> employees = getEmployees();
if (employees != null) {
   for(Employee e : employees) {
      totalPay += e.getPay();
   }
}
```
Right now, getEmployees can return null, but does it have to? If we change `getEmployee` so that it returns an empty list, we can clean up the code:
```java
List<Employee> employees = getEmployees();
for(Employee e : employees) {
   totalPay += e.getPay();
}
```
If you code this way, you will minimize the chance of NullPointerExceptions and your code will be cleaner.

# Chapter 6: Class
## Class Organization
Following the standard Java convention, a class should begin with a list of variables.
   1. Public static constants, if any, should come first.
   2. Then private static variables.
   3. Followed by private instance variables.
   4. There is seldom a good reason to have a public variable.

## Classes Should Be Small!
The first rule of classes is that they should be small. The second rule of classes is that they should be smaller than that.

The name of a class should describe what responsibilities it fulfills. In fact, naming is probably the first way of helping determine class size.
   > For example, class names including weasel words like `Processor` or `Manager` or `Super` often hint at unfortunate aggregation of responsibilities.

## The Single Responsibility Principle
The [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle) states that a class or module should have one, and only one, reason to change.

This principle gives us both a definition of responsibility, and a guidelines for class size. Classes should have one responsibility—one reason to change.

We want our systems to be composed of many small classes, not a few large ones. Each small class encapsulates a single responsibility, has a single reason to change, and collaborates with a few others to achieve the desired system behaviors.

## Cohesion
Classes should have a small number of instance variables. Each of the methods of a class should manipulate one or more of those variables. In general the more variables a method manipulates the more cohesive that method is to its class.

The strategy of keeping functions small and keeping parameter lists short can sometimes lead to a proliferation of instance variables that are used by a subset of methods.

When this happens, it almost always means that there is at least one other class trying to get out of the larger class. You should try to separate the variables and methods into two or more classes such that the new classes are more cohesive.

# Chapter 7: Emergence
## Getting Clean via Emergent Design
What if there were four simple rules that you could follow that would help you create good designs as you worked? According to Kent, a design is “simple” if it follows these rules:
   - Runs all the tests
   - Contains no duplication
   - Expresses the intent of the programmer
   - Minimizes the number of classes and methods

The rules are given in order of importance

## Simple Design Rule 1: Runs All the Tests
## Simple Design Rules 2–4: Refactoring
During this refactoring step, we can apply anything from the entire body of knowledge about good software design. We can increase cohesion, decrease coupling, separate concerns, modularize system concerns, shrink our functions and classes, choose better names, and so on.

This is also where we apply the final three rules of simple design: Eliminate duplication, ensure expressiveness, and minimize the number of classes and methods.

## No Duplication
Duplication is the primary enemy of a well-designed system. It represents additional work, additional risk, and additional unnecessary complexity.
The [TEMPLATE METHOD](https://refactoring.guru/design-patterns/template-method) pattern is a common technique for removing higher-level
duplication.

## Expressive
You can express yourself by choosing good names. We want to be able to hear a class or function name and not be surprised when we discover its responsibilities.

You can also express yourself by keeping your functions and classes small. Small classes and functions are usually easy to name, easy to write, and easy to understand.

You can also express yourself by using standard nomenclature. Design patterns, for example, are largely about communication and expressiveness. By using the standard pattern names, such as COMMAND or VISITOR, in the names of the classes that implement those patterns, you can succinctly describe your design to other developers

## Minimal Classes and Methods
In an effort to make our classes and methods small, we might create too many tiny classes and methods. So this rule suggests that we also keep our function and class counts low.

# Chapter 8: Smells and Heuristic
## Functions
### F1: Too Many Arguments
Functions should have a small number of arguments. No argument is best, followed by one, two, and three. More than three is very questionable and should be avoided.

### F2: Output Arguments
[Output arguments](https://github.com/gameoflord012/ANTs1/blame/main/CleanCodeConventions.md#L213) are counterintuitive. Readers expect arguments to be inputs, not outputs. If your function must change the state of something, have it change the state of the object it is called on.

### F3: Flag Arguments
[Boolean arguments](https://github.com/gameoflord012/ANTs1/blame/main/CleanCodeConventions.md#L183) loudly declare that the function does more than one thing. They are confusing and should be eliminated.

### F4: Dead Function
Methods that are never called should be discarded. Keeping dead code around is wasteful. Don’t be afraid to delete the function. Remember, your source code control system still remembers it.

## General

### G2: Obvious Behavior Is Unimplemented
Following [The Principle of Least Surprise](http://en.wikipedia.org/wiki/
Principle_of_least_astonishment), any function or class should implement the behaviors that another programmer could reasonably expect.

### G3: Incorrect Behavior at the Boundaries
Every boundary condition, every corner case, every quirk and exception represents something that can confound an elegant and intuitive algorithm.

### G4: Overridden Safeties

### G5: Duplication
Every time you see duplication in the code, it represents a missed opportunity for abstraction. That duplication could probably become a subroutine or perhaps another class outright.

By folding the duplication into such an abstraction, you increase the vocabulary of the language of your design. Other programmers can use the abstract facilities you create. Coding becomes faster and less error prone because you have raised the abstraction level.

Still more subtle are the modules that have similar algorithms, but that don’t share
similar lines of code. This is still duplication and should be addressed by using the [TEMPLATE METHOD](https://refactoring.guru/design-patterns/template-method), or [STRATEGY](https://refactoring.guru/design-patterns/strategy/csharp/example#:~:text=Strategy%20is%20a%20behavioral%20design,delegates%20it%20executing%20the%20behavior) pattern.

### G6: Code at Wrong Level of Abstraction
It is important to create abstractions that separate higher level general concepts from lower level detailed concepts. Sometimes we do this by creating abstract classes to hold the higher level concepts and derivatives to hold the lower level concepts.

We want *all* the lower level concepts to be in the derivatives and *all* the higher level concepts to be in the base class.

For example, constants, variables, or utility functions that pertain only to the detailed implementation should not be present in the base class. The base class should know nothing about them.

Consider the following code:
```java
public interface Stack {
   Object pop() throws EmptyException;
   void push(Object o) throws FullException;
   double percentFull();
   class EmptyException extends Exception {}
   class FullException extends Exception {}
}
```
The percentFull function is at the wrong level of abstraction. So the function would be
better placed in a derivative interface such as BoundedStack.

### G7: Base Classes Depending on Their Derivatives
The most common reason for partitioning concepts into base and derivative classes is so that the higher level base class concepts can be independent of the lower level derivative class concepts.

In general, base classes should know nothing about their derivatives.

### G8: Too Much Information
Good software developers learn to limit what they expose at the interfaces of their classes and modules. The fewer methods a class has, the better. The fewer variables a function knows about, the better. The fewer instance variables a class has, the better.

Hide your data. Hide your utility functions. Hide your constants and your temporaries. Don’t create classes with lots of methods or lots of instance variables. Don’t create lots of protected variables and functions for your subclasses. Concentrate on keeping interfaces very tight and very small.

### G9: Dead Code
When you find dead code, do the right thing. Give it a decent burial. Delete it from the system.

### G10: Vertical Separation
Variables and function should be defined close to where they are used. Local variables
should be declared just above their first usage and should have a small vertical scope.

Private functions should be defined just below their first usage.

### G11: Inconsistency
If you do something a certain way, do all similar things in the same way. This goes back
to the [principle of least surprise](https://en.wikipedia.org/wiki/Principle_of_least_astonishment#:~:text=The%20principle%20of%20least%20astonishment,not%20astonish%20or%20surprise%20users.).

Be careful with the conventions you choose, and once chosen, be careful to continue to follow them.

### G12: Clutter

Of what use is a default constructor with no implementation? All it serves to do is clutter up the code with meaningless artifacts. Variables that aren’t used, functions that are never called, comments that add no information, and so forth. All these things are clutter and should be removed.

### G13: Artificial Coupling
Things that don’t depend upon each other should not be artificially coupled.

> For example, general enums should not be contained within more specific classes because this forces the whole application to know about these more specific classes.

In general an artificial coupling is a coupling between two modules that serves no direct purpose. It is a result of putting a variable, constant, or function in a temporarily convenient, though inappropriate, location. This is lazy and careless.

Take the time to figure out where functions, constants, and variables ought to be declared. Don’t just toss them in the most convenient place at hand and then leave them there.

### G14: Feature Envy

### G15: Selector Arguments
Selector arguments are just a lazy way to avoid splitting a large function into several smaller functions. Consider:
```java
public int calculateWeeklyPay(boolean overtime) {
   int tenthRate = getTenthRate();
   int tenthsWorked = getTenthsWorked();
   int straightTime = Math.min(400, tenthsWorked);
   int overTime = Math.max(0, tenthsWorked - straightTime);
   int straightPay = straightTime * tenthRate;
   double overtimeRate = overtime ? 1.5 : 1.0 * tenthRate;
   int overtimePay = (int)Math.round(overTime*overtimeRate);
   return straightPay + overtimePay;
}
```
You call this function with a true if overtime is paid as time and a half, and with a false if overtime is paid as `straight` time. It’s bad enough that you must remember what `calculateWeeklyPay(false)` means whenever you happen to stumble across it.
```java
public int straightPay() {
   return getTenthsWorked() * getTenthRate();
}

public int overTimePay() {
   int overTimeTenths = Math.max(0, getTenthsWorked() - 400);
   int overTimePay = overTimeBonus(overTimeTenths);
   return straightPay() + overTimePay;
}

private int overTimeBonus(int overTimeTenths) {
   double bonus = 0.5 * getTenthRate() * overTimeTenths;
   return (int) Math.round(bonus);
}
```

### G16: Obscured Intent
We want code to be as expressive as possible. Run-on expressions, Hungarian notation, and magic numbers all obscure the author’s intent. For example, here is the overTimePay function as it might have appeared:
```java
public int m_otCalc() {
   return iThsWkd * iThsRte +
      (int) Math.round(0.5 * iThsRte *
      Math.max(0, iThsWkd - 400)
   );
}
```
Small and dense as this might appear, it’s also virtually impenetrable. It is worth taking the time to make the intent of our code visible to our readers.

### G17: Misplaced Responsibility
One of the most important decisions a software developer can make is where to put code. For example, where should the `PI` constant go? Should it be in the `Math` class? Perhaps it belongs in the `Trigonometry` class? Or maybe in the `Circle` class?

The principle of least surprise comes into play here. Code should be placed where a reader would naturally expect it to be.

### G18: Inappropriate Static
Math.max(double a, double b) is a good static method. It does not operate on a single instance; indeed, it would be silly to have to say new Math().max(a,b) or even a.max(b).

In general you should prefer nonstatic methods to static methods. When in doubt, make the function nonstatic. If you really want a function to be static, make sure that there is no chance that you’ll want it to behave polymorphically.

### G19: Use Explanatory Variables
 One of the more powerful ways to make a program readable is to break the calculations up into intermediate values that are held in variables with meaning.

Consider this example from FitNesse:
```java
Matcher match = headerPattern.matcher(line);
if(match.find())
{
   String key = match.group(1);
   String value = match.group(2);
   headers.put(key.toLowerCase(), value);
}
```
The simple use of explanatory variables makes it clear that the first matched group is
the key, and the second matched group is the value.

### G20: Function Names Should Say What They Do
Look at this code:
```java
Date newDate = date.add(5);
```
Would you expect this to add five days to the date? Or is it weeks, or hours?
If the function adds five days to the date and changes the date, then it should be called `addDaysTo` or `increaseByDays`.

If you have to look at the implementation (or documentation) of the function to know what it does, then you should work to find a better name or rearrange the functionality so that it can be placed in functions with better names.

### G21: Understand the Algorithm
Before you consider yourself to be done with a function, make sure you understand how it works. It is not good enough that it passes all the tests. You must know10 that the solution is correct.

### G22: Make Logical Dependencies Physical
For example, imagine that you are writing a function that prints a plain text report of hours worked by employees. One class named `HourlyReporter` gathers all the data into a convenient form and then passes it to `HourlyReportFormatter` to print it.

```java
public class HourlyReporter {
   private HourlyReportFormatter formatter;
   private List<LineItem> page;
   private final int PAGE_SIZE = 55;

   public HourlyReporter(HourlyReportFormatter formatter) {
      this.formatter = formatter;
      page = new ArrayList<LineItem>();
   }

   public void generateReport(List<HourlyEmployee> employees) {
      for (HourlyEmployee e : employees) {
         addLineItemToPage(e);
         if (page.size() == PAGE_SIZE)
            printAndClearItemList();
      }
      if (page.size() > 0)
         printAndClearItemList();
   }

   private void printAndClearItemList() {
      formatter.format(page);
      page.clear();
   }

   private void addLineItemToPage(HourlyEmployee e) {
      LineItem item = new LineItem();
      item.name = e.getName();
      item.hours = e.getTenthsWorked() / 10;
      item.tenths = e.getTenthsWorked() % 10;
      page.add(item);
   }

   public class LineItem {
      public String name;
      public int hours;
      public int tenths;
   }
}
```
This code has a logical dependency that has not been physicalized. Can you spot it? It is the constant `PAGE_SIZE`. Why should the `HourlyReporter` know the size of the page? Page size should be the responsibility of the `HourlyReportFormatter`.

We can physicalize this dependency by creating a new method in `HourlyReportFormatter` named `getMaxPageSize()`. HourlyReporter will then call that function rather than using the `PAGE_SIZE` constant.

### G23: Prefer Polymorphism to If/Else or Switch/Case

### G24: Follow Standard Conventions
This coding standard should specify things like where to declare instance variables; how to name classes, methods, and variables; where to put braces; and so on.

The team should not need a document to describe these conventions because their code provides the examples.

### G25: Replace Magic Numbers with Named Constants
For example, the number 86,400 should be hidden behind the constant `SECONDS_PER_DAY`. If you are printing 55 lines per page, then the constant 55 should be hidden behind the constant `LINES_PER_PAGE`.

### G26: Be Precise

### G27: Structure over Convention
Naming conventions are good, but they are inferior to structures that force compliance.

For example, switch/cases with nicely named enumerations are inferior to base classes with abstract methods. No one is forced to implement the switch/case statement the same way each time; but the base classes do enforce that concrete classes have all abstract methods implemented.

### G28: Encapsulate Conditionals
Boolean logic is hard enough to understand without having to see it in the context of an if or while statement. Extract functions that explain the intent of the conditional.

For example:
```java
if (shouldBeDeleted(timer))
```
is preferable to
```java
if (timer.hasExpired() && !timer.isRecurrent())
```

### G29: Avoid Negative Conditionals
Negatives are just a bit harder to understand than positives. So, when possible conditionals should be expressed as positives. For example:
```java
if (buffer.shouldCompact())
```
is preferable to
```java
if (!buffer.shouldNotCompact())
```

### G30: Functions Should Do One Thing
It is often tempting to create functions that have multiple sections that perform a series of operations. Functions of this kind do more than one thing, and should be converted into many smaller functions, each of which does one thing.
For example:
```java
public void pay() {
   for (Employee e : employees) {
      if (e.isPayday()) {
         Money pay = e.calculatePay();
         e.deliverPay(pay);
      }
   }
}
```
---
```java
public void pay() {
for (Employee e : employees)
   payIfNecessary(e);
}

private void payIfNecessary(Employee e) {
   if (e.isPayday())
      calculateAndDeliverPay(e);
}

private void calculateAndDeliverPay(Employee e) {
   Money pay = e.calculatePay();
   e.deliverPay(pay);
}
```
Each of these functions does one thing.

### G31: Hidden Temporal Couplings
[Temporal couplings](https://blog.ploeh.dk/2011/05/24/DesignSmellTemporalCoupling/) are often necessary, but you should not hide the coupling. Structure the arguments of your functions such that the order in which they should be called is obvious.

Consider the following:

```java
public class MoogDiver {
Gradient gradient;
   List<Spline> splines;

   public void dive(String reason) {
      saturateGradient();
      reticulateSplines();
      diveForMoog(reason);
      }
   ...
}
```

The order of the three functions is important. You must saturate the gradient before you can reticulate the splines, and only then can you dive for the moog. Unfortunately, the code does not enforce this temporal coupling.

Another programmer could call `reticulateSplines` before `saturateGradient` was called, leading to an `UnsaturatedGradientException`. A better solution is:

```java
public class MoogDiver {
   Gradient gradient;
   List<Spline> splines;

   public void dive(String reason) {
      Gradient gradient = saturateGradient();
      List<Spline> splines = reticulateSplines(gradient);
      diveForMoog(splines, reason);
      }
   ...
}
```
This exposes the temporal coupling by creating a bucket brigade. Each function produces a result that the next function needs, so there is no reasonable way to call them out of order.

### G32: Don’t Be Arbitrary
Have a reason for the way you structure your code, and make sure that reason is communicated by the structure of the code. If a structure appears arbitrary, others will feel empowered to change it. If a structure appears consistently throughout the system, others will use it and preserve the convention.
```java
public class AliasLinkWidget extends ParentWidget
{
   public static class VariableExpandingWidgetRoot {
      ...

   ...
}
```
The problem with this was that `VariableExpandingWidgetRoot` had no need to be inside the scope of `AliasLinkWidget`. Moreover, other unrelated classes made use of `AliasLinkWidget`.`VariableExpandingWidgetRoot`. These classes had no need to know about `AliasLinkWidget`.

### G33: Encapsulate Boundary Conditions
Boundary conditions are hard to keep track of. Put the processing for them in one place. Don’t let them leak all over the code.
```java
if(level + 1 < tags.length)
{
   parts = new Parse(body, tags, level + 1, offset + endTag);
   body = null;
}
```
Notice that `level+1` appears twice. This is a boundary condition that should be encapsulated within a variable named something like `nextLevel`.
```java
int nextLevel = level + 1;
if(nextLevel < tags.length)
{
   parts = new Parse(body, tags, nextLevel, offset + endTag);
   body = null;
}
```

### G34: Functions Should Descend Only One Level of Abstraction
The statements within a function should all be written at the same level of abstraction, which should be one level below the operation described by the name of the function.

### G35: Keep Configurable Data at High Levels
If you have a constant such as a default or configuration value that is known and expected at a high level of abstraction, do not bury it in a low-level function. Expose it as an argument to that low-level function called from the high-level function. Consider the following code from FitNesse:
```java
public static void main(String[] args) throws Exception
{
   Arguments arguments = parseCommandLine(args);
   ...
}

public class Arguments
{
   public static final String DEFAULT_PATH = ".";
   public static final String DEFAULT_ROOT = "FitNesseRoot";
   public static final int DEFAULT_PORT = 80;
   public static final int DEFAULT_VERSION_DAYS = 14;
   ...
}
```

The command-line arguments are parsed in the very first executable line of FitNesse. The default values of those arguments are specified at the top of the Argument class. You don’t have to go looking in low levels of the system for statements like this one:
```java
if (arguments.port == 0) // use 80 by default
```

The configuration constants reside at a very high level and are easy to change. They get passed down to the rest of the application. The lower levels of the application do not own the values of these constants.

### G36: Avoid Transitive Navigation
If `A` collaborates with `B`, and `B` collaborates with `C`, we don’t want modules that use `A` to know about `C`.
   > For example, we don’t want `a.getB().getC().doSomething();`.

Rather we want our immediate collaborators to offer all the services we need. We should not have to roam through the object graph of the system, hunting for the method we
want to call.

 Rather we should simply be able to say:
```java
myCollaborator.doSomething()
```

## Names
### N1: Choose Descriptive Names
Don’t be too quick to choose a name. Make sure the name is descriptive. Remember that meanings tend to drift as software evolves, so frequently reevaluate the appropriateness of the names you choose.

This is not just a “feel-good” recommendation. Names in software are 90 percent of what make software readable. You need to take the time to choose them wisely and keep them relevant. Names are too important to treat carelessly.

### N2: Choose Names at the Appropriate Level of Abstraction
Don’t pick names that communicate implementation; choose names the reflect the level of abstraction of the class or function you are working in.

Consider the `Modem` interface below:
```java
public interface Modem {
   boolean dial(String phoneNumber);
   boolean disconnect();
   boolean send(char c);
   char recv();
   String getConnectedPhoneNumber();
}
```
But now consider an application in which some modems aren’t connected by `dialling`. Perhaps some are connected by sending a port number to a switch over a USB connection. Clearly the notion of phone numbers is at the wrong level of abstraction.

A better naming strategy for this scenario might be:
```java
public interface Modem {
   boolean connect(String connectionLocator);
   boolean disconnect();
   boolean send(char c);
   char recv();
   String getConnectedLocator();
}
```

### N3: Use Standard Nomenclature Where Possible
Names are easier to understand if they are based on existing convention or usage. For example, if you are using the `DECORATOR` pattern, you should use the word Decorator in the names of the decorating classes.

### N4: Unambiguous Names
Choose names that make the workings of a function or variable unambiguous.
```java
private String doRename() throws Exception
{
   if(refactorReferences)
      renameReferences();
   renamePage();

   pathToRename.removeNameFromEnd();
   pathToRename.addNameToEnd(newName);
   return PathParser.render(pathToRename);
}
```

The name of this function does not say what the function does except in broad and vague terms. This is emphasized by the fact that there is a function named `renamePage` inside the function named `doRename`! What do the names tell you about the difference between the two functions? Nothing.

A better name for that function is `renamePageAndOptionallyAllReferences`. This may seem long, and it is, but it’s only called from one place in the module, so it’s explanatory value outweighs the length.

### N5: Use Long Names for Long Scopes
The longer the scope of the name, the longer and more precise the name should be.

Variable names like `i` and `j` are just fine if their scope is five lines long. Consider this:
```java
private void rollMany(int n, int pins)
{
   for (int i=0; i<n; i++)
   g.roll(pins);
}
```

### N6: Avoid Encodings
Names should not be encoded with type or scope information. Prefixes such as `m_` or `f` are useless in today’s environments.

### N7: Names Should Describe Side-Effects
Names should describe everything that a function, variable, or class is or does. Don’t hide side effects with a name. Don’t use a simple verb to describe a function that does more than just that simple action.

For example:
```java
public ObjectOutputStream getOos() throws IOException {
   if (m_oos == null) {
      m_oos = new ObjectOutputStream(m_socket.getOutputStream());
   }
   return m_oos;
}
```
This function does a bit more than get an “oos”; it creates the “oos” if it hasn’t been created already. Thus, a better name might be `createOrReturnOos`.
