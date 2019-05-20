# CsSelenium
## Introduction
To build this simple frame I looked closely to the selenide project built in JAVA.

This project truely managed to capture Selenium in quite a simple frame.

As it stands now the framework is nothing like selenide for what concerns the architecture Selenide is miles ahead for what concerns maturity when it comes to that. This project just shares some ideas.

## Philosophy
### Simple interface
Whenever a letter of code is writting: KISS.

Whenever we need a complex item:
1. Build the item
2. Make it avaialbe in case someone really wants it
3. Make sure it is readily available behind the scenes

### Simple searches
Example: Threadbased driver + WebDriverFactory.
The framework will generate a driver when the thread requesting it is new (for parallel execution).
The logic to manage and call the driver is hidden in behind the scenes classes.


The user can have code like this:

```C#
namespace SomeTest
{
    [TestClass]
    public class SomeTests
    {
        [Initialize]
        public void Initialize()
        {
            open("http://www.somepagehere.someext");
        }
        
        [Cleanup]
        public void Cleanup()
        {
            QuiteAndDestroy();
        }
        
        [TestMethod]
        public void SomeTestOne()
        {
            f("some css.reference").GetText().Equals("the stuff to equal");
        }
        
        [TestMethod]
        public void SomeTestTwo()
        {
            ff("css.ref to many").Size() == 5;
        }
    }
}
```

Running these tests in Parrallel will create two drivers. One per thread for a test.
With 'open()' you call a driver (created when not there yet).

To find a element you just go f("identifier") for css. For xpath you go fx("identifier"). You can still use the selenium basics to insert finds with By objects like f(By.CssSelector("some selector")).

To find many objects you type ff("identifier") which will return a list of elements.

That's all there is right now:
- this framework removes boilerplating for finds
- the framework manages the webdriver on a thread basis

### Assertions on framework level
The user has the ability to create custom assertions with frameworks and assertion librarires of choice by retrieving the web element object from the CsSeElement object. This provides total freedom.

The assertions on framework level are built to make the process of asserting both fluent and easy.

The only Assertions framework implemented at this point in time are the assertion libraries from Microsoft.VisualStudio.TestTools.UnitTesting.

# Roadmap

- Assertions (to remove the boilerplate code there as well)
- Logging (to trace back the results) (!) --> First up
- Describe architecture
