# Refactoring Techniques
## Format of the Refactorings
As I describe the refactorings in this and other chapters, I use a standard format. Each refactoring has five parts, as follows:

- I begin with a **name**. The name is important to building a vocabulary of refactorings. This is the name I use elsewhere in the book.

- I follow the name with a short **summary** of the situation in which you need the refactoring and a summary of what the refactoring does. This helps you find a refactoring more quickly.

- The **motivation** describes why the refactoring should be done and describes
circumstances in which it shouldn't be done.

- The **mechanics** are a concise, step-by-step description of how to carry out the
refactoring.

- The **examples** show a very simple use of the refactoring to illustrate how it works.

## Chapter 1. Composing Methods

### Extract Method

You have a code fragment that can be grouped together.

Turn the fragment into a method whose name explains the purpose of the method.
```java
void printOwing(double amount) {
    printBanner();
    //print details
    System.out.println ("name:" + _name);
    System.out.println ("amount" + amount);
}
```
---
```java
void printOwing(double amount) {
    printBanner();
    printDetails(amount);
}

void printDetails (double amount) {90
    System.out.println ("name:" + _name);
    System.out.println ("amount" + amount);
}
```

Motivation
- First, it increases the chances that other methods can use a method when the method is finely grained.

- Second, it allows the higher-level methods to read more like a series of comments.

- Small methods really work only when you have good names, so you need to pay attention to naming.

### Inline Method
A method's body is just as clear as its name.

Put the method's body into the body of its callers and remove the method.
```java
int getRating() {
    return (moreThanFiveLateDeliveries()) ? 2 : 1;
}

boolean moreThanFiveLateDeliveries() {
    return _numberOfLateDeliveries > 5;
}
```
```java
int getRating() {
    return (_numberOfLateDeliveries > 5) ? 2 : 1;
}
```

Motivation
- sometimes you do come across a method in which the body is as clear as the name. When this happens, you should then get rid of the method.

- Another time to use Inline Method is when you have a group of methods that seem badly factored. You can inline them all into one big method and then reextract the methods.

- I commonly use Inline Method when someone is using too much indirection and it seems that every method does simple delegation to another method, and I get lost in all the delegation.

### Inline Temp
You have a temp that is assigned to once with a simple expression, and the temp is getting in the way of other refactorings.

```java
double basePrice = anOrder.basePrice();
return (basePrice > 1000)
```
---
```java
return (anOrder.basePrice() > 1000)
```

Motivation
- Most of the time *Inline Temp* is used as part of **Replace Temp with Query**, so the real motivation is there.

- he only time *Inline Temp* is used on its own is when you find a temp that is assigned the value of a method call. Often this temp isn't doing any harm and you can safely leave it there.

- If the temp is getting in the way of other refactorings, such as **Extract Method**, it's time to inline it.

### Replace Temp with Query
You are using a temporary variable to hold the result of an expression.

Extract the expression into a method. Replace all references to the temp with the expression. The new method can then be used in other methods.

```java
double basePrice = _quantity * _itemPrice;
if (basePrice > 1000)
    return basePrice * 0.95;
else
    return basePrice * 0.98;98

```
---
```java
if (basePrice() > 1000)
    return basePrice() * 0.95;
else
    return basePrice() * 0.98;
...
double basePrice() {
    return _quantity * _itemPrice;
}
```

Motivation
- The problem with temps is that they are temporary and local, temps tend to encourage longer methods.

- By replacing the temp with a query method, any method in the class can get at the information. That helps a lot in coming up with cleaner code for the class.

- `Replace Temp with Query` often is a vital step before **Extract Method**. Local variables make it difficult to extract, so replace as many variables as you can with queries.

- Other cases are trickier but possible. You may need to use **Split Temporary Variable** or **Separate Query from Modifier** first to make things easier.
    > If the temp is used to collect a result (such as summing over a loop), you need to copy some logic into the query method

### Introduce Explaining Variable
You have a complicated expression

Put the result of the expression, or parts of the expression, in a temporary variable with a name that explains the purpose.

```java
if ((platform.toUpperCase().indexOf("MAC") > -1) &&
     (browser.toUpperCase().indexOf("IE") > -1) &&
      wasInitialized() && resize > 0 )
{
// do something
}
```
---
```java
final boolean isMacOs = platform.toUpperCase().indexOf("MAC") > -1;
final boolean isIEBrowser = browser.toUpperCase().indexOf("IE") > -1;
final boolean wasResized = resize > 0;

if (isMacOs && isIEBrowser && wasInitialized() && wasResized) {
    // do something
}
```

Motivation
- Expressions can become very complex and hard to read. In such situations temporary variables can be helpful to break down the expression into something more manageable.

- *Introduce Explaining* Variable is particularly valuable with conditional logic in which it is useful to take each clause of a condition and explain what the condition means with a well-named temp.

- Another case is a long algorithm, in which each step in the computation can be explained with a temp.

- I almost always prefer to use **Extract Method** if I can. A temp is useful only within the context of one method. A method is useable throughout the object and to other objects.

- There are times, however, when local variables make it difficult to use **Extract Method**. That's when I use **Introduce Explaining Variable**.

### Split Temporary Variable
You have a temporary variable assigned to more than once, but is not a loop variable nor a collecting temporary variable.

Make a separate temporary variable for each assignment.
```java
double temp = 2 * (_height + _width);
System.out.println (temp);
temp = _height * _width;
System.out.println (temp);
```
---
```java
final double perimeter = 2 * (_height + _width);
System.out.println (perimeter);
final double area = _height * _width;
System.out.println (area);
```

Motivation
- Any variable with more than one responsibility should be replaced with a temp for each responsibility. Using a temp for two different things is very confusing for the reader.

### Remove Assignments to Parameters
The code assigns to a parameter.

Use a temporary variable instead
```java
int discount (int inputVal, int quantity, int yearToDate) {
if (inputVal > 50) inputVal -= 2;
```
---
```java
int discount (int inputVal, int quantity, int yearToDate) {
    int result = inputVal;
    if (inputVal > 50) result -= 2;
```

Motivation
- The reason I don't like this comes down to lack of clarity and to confusion
between pass by value and pass by reference.

- The other area of confusion is within the body of the code itself. It is much clearer if you use only the parameter to represent what has been passed in, because that is a consistent usage.

### Replace Method with Method Object
You have a long method that uses local variables in such a way that you cannot apply **Extract Method**.

Turn the method into its own object so that all the local variables become fields on that object. You can then decompose the method into other methods on the same object.
```java
class Order...
    double price() {
        double primaryBasePrice;
        double secondaryBasePrice;
        double tertiaryBasePrice;
        // long computation;
        ...
    }
```
---
![Image](https://i.imgur.com/kkcRgHm.png)

Motivation
- In this book I emphasize the beauty of small methods. By extracting pieces out of a large method, you make things much more comprehensible.

- The difficulty in decomposing a method lies in local variables. If they are rampant, decomposition can be difficult. Using **Replace Temp with Query** helps to reduce this burden.

- Applying **Replace Method with Method** Object turns all these local variables into fields on the method object. You can then use **Extract Method** on this new object to create additional methods that break down the original method.

### Substitute Algorithm
You want to replace an algorithm with one that is clearer.

Replace the body of the method with the new algorithm.

```java
String foundPerson(String[] people){
    for (int i = 0; i < people.length; i++) {
        if (people[i].equals ("Don")) {
            return "Don";
        }
        if (people[i].equals ("John")) {
            return "John";114
        }
        if (people[i].equals ("Kent")) {
            return "Kent";
        }
    }
    return "";
}
```
---
```java
String foundPerson(String[] people){
    List candidates = Arrays.asList(new String[] {"Don", "John", "Kent"});

    for (int i=0; i<people.length; i++)
        if (candidates.contains(people[i]))
            return people[i];

    return "";
}
```

Motivation
- Sometimes when you want to change the algorithm to do something slightly different, it is easier to subtitute the algorithm first into something easier for the change you need to make.

- Substituting a large, complex algorithm is very difficult; only by making it simple can you make the substitution tractable.

## Chapter 7. Moving Features Between Objects